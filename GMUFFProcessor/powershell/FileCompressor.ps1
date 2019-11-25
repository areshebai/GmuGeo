############################################################################
#
# Pick up download task and compress files in to one zip
#
############################################################################

function Compress-ImagesToZip()
{
    ############################################################
    #
    # Definitions
    #
    ############################################################
	$rootFolder = "/home/raw-geo-data/";
    $compressDestination = "/var/ftp/";
	#$compressDestination = "/home/raw-geo-data/download/";

	Add-type -Assembly /opt/microsoft/powershell/6/MySql.Data.dll;
    
    ############################################################
    #
    # 1.Pickup download task form database
    #
    ############################################################
    $con = New-Object Mysql.Data.MySqlClient.MySqlConnection;
    $con.ConnectionString = "server=localhost;userid=root;database=jpssflood;";
    $con.Open();
    $cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
    $cmd.Connection = $con;

    # Get Download tasks that has not been processed.
    $tasks = @();
    $cmd.CommandText = "SELECT * FROM jpssflood.downloadtasks_v2 WHERE Status = 1 LIMIT 2;";
    $cmd.Prepare();
    $reader = $cmd.ExecuteReader();
    while ($reader.Read() -eq $true)
    {
        $id = $reader.GetInt32(0);
        $name = $reader.GetString(2);
        $startTime = $reader.GetMySqlDateTime(3);
        $endTime = $reader.GetMySqlDateTime(4);
        $product = $reader.GetInt32(6);
        $region = $reader.GetInt32(7);
        $north = $reader.GetInt32(8);
        $south = $reader.GetInt32(9);
        $west = $reader.GetInt32(10);
        $east = $reader.GetInt32(11);
        $imageFormat = $reader.GetString(14);

        $taskItem = @{Id = $id; Name = $name; StartTime = $startTime; EndTime = $endTime; Product = $product; Region = $region; North = $north; South = $south; West = $west; East = $east; ImageFormat = $imageFormat;};
        $tasks += $taskItem;
    }

	$con.Close();

    ############################################################
    #
    # 2.Process task and compress files to zip
    #
    ############################################################

    foreach($taskItem in $tasks)
    {
        $fileNames = @();
		
		$taskId = $taskItem["Id"];
        $startDate = $taskItem["StartTime"];
        $endDate = $taskItem["EndTime"];
        $product = $taskItem["Product"];
        $region = $taskItem["Region"];
		$imageFormat = $taskItem["ImageFormat"];

		$compressedFile = Join-Path $compressDestination ($taskItem["Name"]+"_"+$imageFormat+".zip");

		$con.Open();
		$cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
		$cmd.Connection = $con;
		$cmd.CommandText = "UPDATE jpssflood.downloadtasks_v2 SET Status = 2 WHERE Id = $($taskId)";
		$cmd.ExecuteNonQuery();
		$con.Close();

		$con.Open();
		$cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
		$cmd.Connection = $con;
		$startDateString = [System.String]::Format("{0:d4}-{1:d2}-{2:d2} 00:00:00", $startDate.Year, $startDate.Month, $startDate.Day);
		$endDateString = [System.String]::Format("{0:d4}-{1:d2}-{2:d2} 00:00:00", $endDate.Year, $endDate.Month, $endDate.Day);
        $cmd.CommandText = "SELECT * FROM jpssflood.kmlmetadata WHERE Date >= '$startDateString' AND Date <= '$endDateString' AND ProductId = $product AND RegionId = $region AND DistrictId > 1 AND DistrictId < 136 ORDER BY DistrictID DESC";
        $cmd.Prepare();
        $reader = $cmd.ExecuteReader();
        while ($reader.Read() -eq $true)
        {
            $productId = $reader.GetInt32(1);
            $regionId = $reader.GetInt32(2);
            $districtId = $reader.GetInt32(3);
            $mySqldate = $reader.GetMySqlDateTime(4);
            $fileName = $reader.GetString(5);

            $folderName = [System.String]::Format("{0:d4}{1:d2}{2:d2}", $mySqldate.Year, $mySqldate.Month, $mySqldate.Day);
            $folderName = Join-Path $rootFolder $folderName;
            $folderName = Join-Path $folderName (GetFolderNameByType($imageFormat));
			$fileName = Join-Path $folderName $fileName;
			$fileExtention = GetFileExtentionByType($imageFormat);
			$fileName = $fileName + $fileExtention;

			$fileNames += $fileName;
        }
		$con.Close();

        Compress-Archive -Path $fileNames -DestinationPath $compressedFile -Force;

		$con.Open();
		$cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
		$cmd.Connection = $con;
		$cmd.CommandText = "UPDATE jpssflood.downloadtasks_v2 SET Status = 3 WHERE Id = $($taskId)";
		$cmd.ExecuteNonQuery();
		$con.Close();
    }
}

function GetFileExtentionByType($fileType)
{
    if ($fileType -eq "GEOTiff") {return ".tif";}
    if ($fileType -eq "HDF4") {return ".hdf";}
    if ($fileType -eq "Kml") {return ".kml";}
    if ($fileType -eq "PNG") {return ".png";}
    if ($fileType -eq "ShapeFile") {return ".zip";}
}

function GetFolderNameByType($fileType)
{
    if ($fileType -eq "GEOTiff") {return "tif";}
    if ($fileType -eq "HDF4") {return "hdf";}
    if ($fileType -eq "Kml") {return "";}
    if ($fileType -eq "PNG") {return "";}
    if ($fileType -eq "ShapeFile") {return "shapefile";}     
}

function Log-Message([string]$message)
{
	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : $message";
}

function Log-Error([string]$message)
{
	$logTime = [System.DateTime]::get_UtcNow();
	Write-Error "$logTime : $message" -ErrorAction Continue;
}