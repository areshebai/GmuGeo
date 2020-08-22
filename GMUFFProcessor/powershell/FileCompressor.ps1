############################################################################
#
# Pick up download task and copy files in to destination
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
	# Status: 1 - Scheduled, 2 - Processing, 3 - Completed
	# Do support pick up multiple tasks. Howerver, only pick 1 task currently for performance consideration
    $tasks = @();
    $cmd.CommandText = "SELECT * FROM jpssflood.downloadtasks_v2 WHERE Status = 1 LIMIT 1;";
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

		$west = $taskItem["West"];
		$east = $taskItem["East"];
		$south = $taskItem["South"];
		$north = $taskItem["North"];

		# Don't compress file for downloading now because of performance consideration'
		# Only copy files to DestinationPath
		$compressedFile = Join-Path $compressDestination ($taskItem["Name"]+"_"+$imageFormat+".zip");
		$folderForDownloading = Join-Path $compressDestination ($taskItem["Name"]+"_"+$imageFormat);

		$con.Open();
		$cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
		$cmd.Connection = $con;
		$cmd.CommandText = "UPDATE jpssflood.downloadtasks_v2 SET Status = 2 WHERE Id = $($taskId)";
		$cmd.ExecuteNonQuery();
		$con.Close();

		$con.Open();
		$cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
		$cmd.Connection = $con;
		$startDateConverted = [System.DateTime]::New($startDate.Year, $startDate.Month, $startDate.Day);
		$endDateConverted = [System.DateTime]::New($endDate.Year, $endDate.Month, $endDate.Day);
		$endDateConverted = $endDateConverted.AddDays(1);
		$startDateString = $startDateConverted.ToString("yyyy-MM-dd 00:00:00");
		$endDateString = $endDateConverted.ToString("yyyy-MM-dd 00:00:00");
        $cmd.CommandText = "SELECT * FROM jpssflood.kmlmetadata WHERE Date >= '$startDateString' AND Date < '$endDateString' AND ProductId = $product AND RegionId = $region AND DistrictId > 1 AND DistrictId < 136 ORDER BY DistrictID DESC";
        $cmd.Prepare();
        $reader = $cmd.ExecuteReader();
        while ($reader.Read() -eq $true)
        {
            $productId = $reader.GetInt32(1);
            $regionId = $reader.GetInt32(2);
            $districtId = $reader.GetInt32(3);
            $mySqldate = $reader.GetMySqlDateTime(4);
            $fileName = $reader.GetString(5);
			$districtBound = GetBoundaryFromDistrictId($districtId);

			$isInBoundary = IsInBoundary $west $east $south $north $districtBound.West $districtBound.East $districtBound.South $districtBound.North
			if($isInBoundary -eq $false)
			{
				continue;
			}

            $folderName = [System.String]::Format("{0:d4}{1:d2}{2:d2}", $mySqldate.Year, $mySqldate.Month, $mySqldate.Day);
            $folderName = Join-Path $rootFolder $folderName;
            $folderName = Join-Path $folderName (GetFolderNameByType($imageFormat));
			$fileName = Join-Path $folderName $fileName;
			$fileExtention = GetFileExtentionByType($imageFormat);
			$fileName = $fileName + $fileExtention;

			$fileNames += $fileName;
        }
		$con.Close();

        # Compress-Archive -Path $fileNames -DestinationPath $compressedFile -Force;
		mkdir $folderForDownloading
		$fileNames | Copy-Item -Destination $folderForDownloading -Force

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

function IsInBoundary($stdWest, $stdEast, $stdSouth, $stdNorth, $varWest, $varEast, $varSouth, $varNorth)
{
    $isInBoundary = $true;
	if(($varWest -gt $stdEast) -or ($varEast -lt $stdWest) -or ($varSouth -gt $stdNorth) -or ($varNorth -lt $stdSouth))
	{
		$isInBoundary = $false;
	}
	return $isInBoundary;
}

function GetBoundaryFromDistrictId($districtId)
{
	switch ( $districtId )
    {
        1 { $result = @{West=-180; East=-165; South=60; North=75}}
        2 { $result = @{West=-165; East=-150; South=60; North=75}}
        3 { $result = @{West=-150; East=-135; South=60; North=75}}
        4 { $result = @{West=-135; East=-120; South=60; North=75}}
        5 { $result = @{West=-120; East=-105; South=60; North=75}}
        6 { $result = @{West=-105; East=-90; South=60; North=75}}
        7 { $result = @{West=-90; East=-75; South=60; North=75}}
		8 { $result = @{West=-75; East=-60; South=60; North=75}}
		9 { $result = @{West=-180; East=-165; South=45; North=60}}
		10 { $result = @{West=-165; East=-150; South=45; North=60}}
		11 { $result = @{West=-135; East=-120; South=45; North=60}}
		12 { $result = @{West=-120; East=-105; South=45; North=60}}
		13 { $result = @{West=-105; East=-90; South=45; North=60}}
		14 { $result = @{West=-90; East=-75; South=45; North=60}}
		15 { $result = @{West=-75; East=-60; South=45; North=60}}
		16 { $result = @{West=-60; East=-45; South=45; North=60}}
		17 { $result = @{West=-135; East=-120; South=30; North=45}}
		18 { $result = @{West=-120; East=-105; South=30; North=45}}
		19 { $result = @{West=-105; East=-90; South=30; North=45}}
		20 { $result = @{West=-90; East=-75; South=30; North=45}}
		21 { $result = @{West=-75; East=-60; South=30; North=45}}
		22 { $result = @{West=-120; East=-105; South=15; North=30}}
		23 { $result = @{West=-105; East=-90; South=15; North=30}}
		24 { $result = @{West=-90; East=-75; South=15; North=30}}
		25 { $result = @{West=-75; East=-60; South=15; North=30}}
		26 { $result = @{West=-105; East=-90; South=0; North=15}}
		27 { $result = @{West=-90; East=-75; South=0; North=15}}
		28 { $result = @{West=-75; East=-60; South=0; North=15}}
		29 { $result = @{West=-60; East=-45; South=0; North=15}}
		30 { $result = @{West=-90; East=-75; South=-15; North=0}}
		31 { $result = @{West=-75; East=-60; South=-15; North=0}}
		32 { $result = @{West=-60; East=-45; South=-15; North=0}}
		33 { $result = @{West=-45; East=-30; South=-15; North=0}}
		34 { $result = @{West=-75; East=-60; South=-30; North=-15}}
		35 { $result = @{West=-60; East=-45; South=-30; North=-15}}
		36 { $result = @{West=-45; East=-30; South=-30; North=-15}}
		37 { $result = @{West=-75; East=-60; South=-45; North=-30}}
		38 { $result = @{West=-60; East=-45; South=-45; North=-30}}
		39 { $result = @{West=-90; East=-75; South=-60; North=-45}}
		40 { $result = @{West=-75; East=-60; South=-60; North=-45}}
		41 { $result = @{West=-60; East=-45; South=-60; North=-45}}
		42 { $result = @{West=-15; East=0; South=60; North=75}}
		43 { $result = @{West=0; East=15; South=60; North=75}}
		44 { $result = @{West=15; East=30; South=60; North=75}}
		45 { $result = @{West=30; East=45; South=60; North=75}}
		46 { $result = @{West=45; East=60; South=60; North=75}}
		47 { $result = @{West=60; East=75; South=60; North=75}}
		48 { $result = @{West=75; East=90; South=60; North=75}}
		49 { $result = @{West=90; East=105; South=60; North=75}}
		50 { $result = @{West=90; East=105; South=75; North=90}}
		51 { $result = @{West=105; East=120; South=75; North=90}}
		52 { $result = @{West=105; East=120; South=60; North=75}}
		53 { $result = @{West=120; East=135; South=60; North=75}}
		54 { $result = @{West=135; East=150; South=60; North=75}}
		55 { $result = @{West=150; East=165; South=60; North=75}}
		56 { $result = @{West=165; East=180; South=60; North=75}}
		57 { $result = @{West=-15; East=0; South=45; North=60}}
		58 { $result = @{West=0; East=15; South=45; North=60}}
		59 { $result = @{West=15; East=30; South=45; North=60}}
		60 { $result = @{West=30; East=45; South=45; North=60}}
		61 { $result = @{West=45; East=60; South=45; North=60}}
		62 { $result = @{West=60; East=75; South=45; North=60}}
		63 { $result = @{West=75; East=90; South=45; North=60}}
		64 { $result = @{West=90; East=105; South=45; North=60}}
		65 { $result = @{West=105; East=120; South=45; North=60}}
		66 { $result = @{West=120; East=135; South=45; North=60}}
		67 { $result = @{West=135; East=150; South=45; North=60}}
		68 { $result = @{West=150; East=165; South=45; North=60}}
		69 { $result = @{West=165; East=180; South=45; North=60}}
		70 { $result = @{West=-15; East=0; South=30; North=45}}
		71 { $result = @{West=0; East=15; South=30; North=45}}
		72 { $result = @{West=15; East=30; South=30; North=45}}
		73 { $result = @{West=30; East=45; South=30; North=45}}
		74 { $result = @{West=45; East=60; South=30; North=45}}
		75 { $result = @{West=60; East=75; South=30; North=45}}
		76 { $result = @{West=75; East=90; South=30; North=45}}
		77 { $result = @{West=90; East=105; South=30; North=45}}
		78 { $result = @{West=105; East=120; South=30; North=45}}
		79 { $result = @{West=120; East=135; South=30; North=45}}
		80 { $result = @{West=135; East=150; South=30; North=45}}
		81 { $result = @{West=150; East=165; South=30; North=45}}
		82 { $result = @{West=-30; East=-15; South=15; North=30}}
		83 { $result = @{West=-15; East=0; South=15; North=30}}
		84 { $result = @{West=0; East=15; South=15; North=30}}
		85 { $result = @{West=15; East=30; South=15; North=30}}
		86 { $result = @{West=30; East=45; South=15; North=30}}
		87 { $result = @{West=45; East=60; South=15; North=30}}
		88 { $result = @{West=60; East=75; South=15; North=30}}
		89 { $result = @{West=75; East=90; South=15; North=30}}
		90 { $result = @{West=90; East=105; South=15; North=30}}
		91 { $result = @{West=105; East=120; South=15; North=30}}
		92 { $result = @{West=120; East=135; South=15; North=30}}
		93 { $result = @{West=-30; East=-15; South=0; North=15}}
		94 { $result = @{West=-15; East=0; South=0; North=15}}
		95 { $result = @{West=0; East=15; South=0; North=15}}
		96 { $result = @{West=15; East=30; South=0; North=15}}
		97 { $result = @{West=30; East=45; South=0; North=15}}
		98 { $result = @{West=45; East=60; South=0; North=15}}
		99 { $result = @{West=60; East=75; South=0; North=15}}
		100 { $result = @{West=75; East=90; South=0; North=15}}
		101 { $result = @{West=90; East=105; South=0; North=15}}
		102 { $result = @{West=105; East=120; South=0; North=15}}
		103 { $result = @{West=120; East=135; South=0; North=15}}
		104 { $result = @{West=0; East=15; South=-15; North=0}}
		105 { $result = @{West=15; East=30; South=-15; North=0}}
		106 { $result = @{West=30; East=45; South=-15; North=0}}
		107 { $result = @{West=45; East=60; South=-15; North=0}}
		108 { $result = @{West=75; East=90; South=-15; North=0}}
		109 { $result = @{West=90; East=105; South=-15; North=0}}
		110 { $result = @{West=105; East=120; South=-15; North=0}}
		111 { $result = @{West=120; East=135; South=-15; North=0}}
		112 { $result = @{West=135; East=150; South=-15; North=0}}
		113 { $result = @{West=0; East=15; South=-30; North=-15}}
		114 { $result = @{West=15; East=30; South=-30; North=-15}}
		115 { $result = @{West=30; East=45; South=-30; North=-15}}
		116 { $result = @{West=45; East=60; South=-30; North=-15}}
		117 { $result = @{West=105; East=120; South=-30; North=-15}}
		118 { $result = @{West=120; East=135; South=-30; North=-15}}
		119 { $result = @{West=135; East=150; South=-30; North=-15}}
		120 { $result = @{West=150; East=165; South=-30; North=-15}}
		121 { $result = @{West=15; East=30; South=-45; North=-30}}
		122 { $result = @{West=30; East=45; South=-45; North=-30}}
		123 { $result = @{West=105; East=120; South=-45; North=-30}}
		124 { $result = @{West=120; East=135; South=-45; North=-30}}
		125 { $result = @{West=135; East=150; South=-45; North=-30}}
		126 { $result = @{West=150; East=165; South=-45; North=-30}}
		127 { $result = @{West=165; East=180; South=-45; North=-30}}
		128 { $result = @{West=165; East=180; South=-60; North=-45}}
		129 { $result = @{West=-150; East=-135; South=45; North=60}}
		130 { $result = @{West=-60; East=-45; South=60; North=75}}
		131 { $result = @{West=-45; East=-30; South=60; North=75}}
		132 { $result = @{West=-30; East=-15; South=60; North=75}}
		133 { $result = @{West=-160; East=-150; South=15; North=25}}
		134 { $result = @{West=150; East=165; South=-15; North=0}}
		135 { $result = @{West=165; East=180; South=-30; North=-15}}
		136 { $result = @{West=-180; East=-165; South=-20; North=-5}}
		default {$result = @{West=-180; East=180; South=-90; North=90}}
    }

	return $result;
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