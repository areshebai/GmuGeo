############################################################################
#
# Pick up download task and compress files in to one zip
#
############################################################################

function Compress-ImagesToZip ()
{
    pushd "/home/raw-geo-data/"

    ############################################################
    #
    # Definitions
    #
    ############################################################

    $inputDate = [System.DateTime]::get_UtcNow();

    $folderName = $inputDate.ToString("yyyyMMdd");
    $rootFolder = "/home/raw-geo-data/";
    $compressDestination = "/home/raw-geo-data/download/";
    
    $tifFileTypeName = "tif";
    $hdfFileTypeName = "hdf";
    $kmlFileTypeName = "kml";
    $pngFileTypeName = "png";
    $shapeFileTypeName = "shapefile";

    $fileTypes = @($tifFileTypeName, $hdfFileTypeName, $shapeFileTypeName, $pngFileTypeName, $kmlFileTypeName);

    # /home/raw-geo-data/20191004
    $currentWorkingFolder = (Resolve-Path ./$folderName).Path;

    # /home/raw-geo-data/20191004/tif
    $tifFolder = (Resolve-Path ./$folderName/$tifFileTypeName).Path;

    # /home/raw-geo-data/20191004/hdf
    $hdfFolder = (Resolve-Path ./$folderName/$hdfFileTypeName).Path;

    # /home/raw-geo-data/20191004/archive
    $archiveFolder = (Resolve-Path ./$folderName/$archiveFoldername).Path;

    ############################################################
    #
    # 1.Pickup download task form database
    #
    ############################################################

    Add-type -Assembly /opt/microsoft/powershell/6/MySql.Data.dll;
    $con = New-Object Mysql.Data.MySqlClient.MySqlConnection;
    $con.ConnectionString = "server=localhost;userid=root;database=jpssflood;";
    $con.Open();
    $cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
    $cmd.Connection = $con;
	
	$fileDate = $inputDate.ToString("yyyy-MM-dd");

    Log-Message "Insert database record for $fileBaseName ...";

    # Get Download tasks that has not been processed.
    $tasks = @();
    $mySqlCommond.CommandText = "SELECT * FROM jpssflood.downloadtasks_v2 WHERE Status = 1 LIMIT 5;";
    $mySqlCommond.Prepare();
    $reader = $mySqlCommond.ExecuteReader();
    while ($reader.Read() -eq $true)
    {
        $id = reader.GetInt32(0);
        $name = reader.GetString(2);
        $startTime = reader.GetMySqlDateTime(3);
        $endTime = reader.GetMySqlDateTime(4);
        $product = reader.GetInt32(6);
        $region = reader.GetInt32(7);
        $north = reader.GetInt32(8);
        $south = reader.GetInt32(9);
        $west = reader.GetInt32(10);
        $east = reader.GetInt32(11);
        $imageFormat = reader.GetString(4);

        $taskItem = 
        @{
            Id = $id;
            Name = $name;
            StartTime = $startTime;
            EndTime = $endTime;
            Product = $product;
            Region = $region;
            North = $north;
            South = $south;
            West = $west;
            East = $east;
            ImageFormat = $imageFormat;
        }

        $tasks += $taskItem;
    }

    ############################################################
    #
    # 2.Process task and compress files to zip
    #
    ############################################################s
    foreach($taskItem in $tasks)
    {
        $compressedFile = Join-Path $compressDestination ($taskItem["Name"]+".zip");

        $filesToDownload = @();

        $startDate = $taskItem["StartTime"];
        $endDate = $taskItem["EndTime"];
        $product = $taskItem["Product"];
        $region = $taskItem["Region"];

        $mySqlCommond.CommandText = "SELECT * FROM jpssflood.kmlmetadata WHERE Date >= '$startDate' AND Date <= '$endDate' AND ProductId = $product AND RegionId = $region AND DistrictId > 1 AND DistrictId < 136 ORDER BY DistrictID DESC";
        $mySqlCommond.Prepare();
        $reader = $mySqlCommond.ExecuteReader();
        while ($reader.Read() -eq $true)
        {
            $productId = reader.GetInt32(1);
            $regionId = reader.GetInt32(2);
            $districtId = reader.GetInt32(3);
            $mySqldate = reader.GetMySqlDateTime(4);
            $fileName = reader.GetString(5);

            $folderName = $mySqldate.ToString("yyyyMMdd");
            $folderName = Join-Path $rootFolder $folderName;
            $folderName = Join-Path $folderName (GetFolderNameByTypeId($product));
        }

        Compress-Archive -Path ($dataFile.FullName, $pngFileFullName) -DestinationPath $compressedFile -Force;
    }

	$con.Close();
	popd
}

function GetFileExtentionByType([string]$fileType)
{
    if ($fileType -eq "GEOTiff") {return ".tif";}
    if ($fileType -eq "HDF4") {return ".hdf";}
    if ($fileType -eq "Kml") {return ".kml";}
    if ($fileType -eq "PNG") {return ".png";}
    if ($fileType -eq "ShapeFile") {return ".zip";}
}

function GetFileExtentionByTypeId([int]$fileTypeId)
{
    if ($fileType -eq "GEOTiff") {return ".tif";}
    if ($fileType -eq "HDF4") {return ".hdf";}
    if ($fileType -eq "Kml") {return ".kml";}
    if ($fileType -eq "PNG") {return ".png";}
    if ($fileType -eq "ShapeFile") {return ".zip";}     
}

function GetFolderNameByProductId([int]$fileTypeId)
{
    if ($fileType -eq "GEOTiff") {return "tif";}
    if ($fileType -eq "HDF4") {return "hdf";}
    if ($fileType -eq "Kml") {return "";}
    if ($fileType -eq "PNG") {return "";}
    if ($fileType -eq "ShapeFile") {return "zip";}     
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

<#
function QueryDatabaseCommon()
{
    Add-type -Assembly /opt/microsoft/powershell/6/MySql.Data.dll;
    $con = New-Object Mysql.Data.MySqlClient.MySqlConnection;
    $con.ConnectionString = "server=localhost;userid=root;database=jpssflood;";
    $con.Open();
    $cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
    $cmd.Connection = $con; 

    $viirs1DayKmlFileNameFormat = $viirs1DayKmlFileNameFormat + ".kml";
    for($i = 1; $i -le 136; $i++)
    {

		$pathPattern = $viirs1DayKmlFileNameFormat -f $i.ToString("d3");
        # Must return only 1 file after dedup
		$kmlFile = Get-Item -Path (Join-Path $currentWorkingFolder $pathPattern);
        $fileBaseName = $kmlFile.BaseName;
        $cmd.CommandText = "INSERT INTO k (FileName) Values($fileBaseName) WHERE NOT EXISTS (SELECT 1 FROM jpssflood.kmlmetadata WHERE FileName = $fileBaseName)";
        $cmd.Prepare();
        $cmd.Execute();
    }
	$cmd.CommandText = "INSERT INTO k (FileName) Values($fileBaseName) WHERE NOT EXISTS (SELECT 1 FROM jpssflood.kmlmetadata WHERE FileName = $fileBaseName)";
    $cmd.Prepare();
	$data = $cmd.ExecuteReader();

    # $data.GetString(5)
	$dataList = @();
    while ($data.Read() -eq $true)
    {
		$tempObject = New-Object -TypeName PSObject;
		for ($i = 0; $i -lt $data.FieldCount; $i++)
        {
			$fieldType = $data.GetFieldType($i);
            Switch ($fieldType.Name)
            {
				"Int32" {$cellData = $data.GetInt32($i);break}
                "String" {$cellData = $data.GetString($i);break}
                default {$filedType.Name}
			}
			Add-Member -InputObject $tempObject -MemberType "NoteProperty" -Name $data.GetName($i) -Value $cellData;
        }

         $dataList += $temObject
    }
	$con.Close();
}
#>