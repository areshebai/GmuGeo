############################################################################
#
# Organize uploaded file into folder by date
#
############################################################################

$now = [System.DateTime]::get_UtcNow();
$now = $now.AddDays(-1);

function MoveFtpFile ([DateTime]$inputDate)
{
    pushd "/home/raw-geo-data/"

    ############################################################
    #
    # Definitions
    #
    ############################################################

    $folderName = $inputDate.ToString("yyyyMMdd");
    $compressDestination = "/home/www-html/kmls/";

    $viirs1DayFileNameFormat = 'WATER_COM_VIIRS_Prj_SVI_*_*_*_*_*_001day_{0}';
    $viirs5DayFileNameFormat = 'WATER_COM_VIIRS_Prj_SVI_*_*_*_*_*_005day_{0}';
    $ahiFileNameFormat = 'COM_H08_AHI_WATER_*_*_*_*_*_*_*_*_*_{0}';
    $abiFileNameFormat = 'COM_*_ABI_WATER_*_*_*_*_*_*_*_*_*_{0}';
    $viirsAhiFileNameFormat = 'Joint_VIIRS_AHI_WATER_Prj_SVI_*_*_*_*_{0}';
    $viirsAbiFileNameFormat = 'Joint_VIIRS_ABI_WATER_Prj_SVI_*_*_*_*_{0}';

    $archiveFoldername = "archive";

    $tifFileTypeName = "tif";
    $hdfFileTypeName = "hdf";
    $kmlFileTypeName = "kml";
    $pngFileTypeName = "png";
    $shapeFileTypeName = "shapefile";

    $fileTypes = @($tifFileTypeName, $hdfFileTypeName, $shapeFileTypeName, $pngFileTypeName, $kmlFileTypeName);

    ############################################################
    #
    # 1. Create root folder and subfolders
    #
    ############################################################

    if(-not(Test-Path $folderName))
    {
		# Create folder /home/raw-geo-data/{date}
		mkdir $folderName
        Log-Message "Directory is created. $folderName"
    }

    if(-not(Test-Path $folderName/$tifFileTypeName))
    {
        # Create folder /home/raw-geo-data/{date}/tif
        mkdir $folderName/$tifFileTypeName
        Log-Message "Directory is created. $folderName/$tifFileTypeNamef"
    }

    if(-not(Test-Path $folderName/$hdfFileTypeName))
    {
        # Create folder /home/raw-geo-data/{date}/hdf
		mkdir $folderName/$hdfFileTypeName
        Log-Message "Directory is created. $folderName/$hdfFileTypeName"
    }

    if(-not(Test-Path $folderName/$shapeFileTypeName))
    {
        # Create folder /home/raw-geo-data/{date}/shapefile
		mkdir $folderName/$shapeFileTypeName
        Log-Message "Directory is created. $folderName/$shapeFileTypeName"
    }

    if(-not(Test-Path $folderName/$archiveFoldername))
    {
        # Create folder /home/raw-geo-data/{date}/archive
		mkdir $folderName/$archiveFoldername
        Log-Message "Directory is created. $folderName/$archiveFoldername"
    }

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
    # 2. Move files by type
    #
    ############################################################
    $fileTypes | ForEach-Object { Move-FilesByType $folderName $_;};

    ############################################################
    #
    # 3. Compress and move files to https folder
    #
    ############################################################
    MoveAndCompressSourceFiles $fileTypes $currentWorkingFolder $viirs1DayFileNameFormat 136 $archiveFolder $compressDestination;
    MoveAndCompressSourceFiles $fileTypes $currentWorkingFolder $viirs5DayFileNameFormat 136 $archiveFolder $compressDestination;
    MoveAndCompressSourceFiles $fileTypes $currentWorkingFolder $ahiFileNameFormat 8 $archiveFolder $compressDestination;
    MoveAndCompressSourceFiles $fileTypes $currentWorkingFolder $abiFileNameFormat 8 $archiveFolder $compressDestination;
    MoveAndCompressSourceFiles $fileTypes $currentWorkingFolder $viirsAhiFileNameFormat 136 $archiveFolder $compressDestination;
    MoveAndCompressSourceFiles $fileTypes $currentWorkingFolder $viirsAbiFileNameFormat 136 $archiveFolder $compressDestination;

    ############################################################
    #
    # 4. Adjust image resolution for viirs images
    #
    ############################################################
    GenerateViirsDisplayImages $viirs1DayFileNameFormat $currentWorkingFolder $compressDestination;
    GenerateViirsDisplayImages $viirs5DayFileNameFormat $currentWorkingFolder $compressDestination;
    CopyDisplayImages $ahiFileNameFormat $currentWorkingFolder $compressDestination 8;
    CopyDisplayImages $abiFileNameFormat $currentWorkingFolder $compressDestination 8;
    CopyDisplayImages $viirsAhiFileNameFormat $currentWorkingFolder $compressDestination 136;
    CopyDisplayImages $viirsAbiFileNameFormat $currentWorkingFolder $compressDestination 136;

    ############################################################
    #
    # 5.Insert database records for new images
    #
    ############################################################

    Add-type -Assembly /opt/microsoft/powershell/6/MySql.Data.dll;
    $con = New-Object Mysql.Data.MySqlClient.MySqlConnection;
    $con.ConnectionString = "server=localhost;userid=root;database=jpssflood;";
    $con.Open();
    $cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
    $cmd.Connection = $con;
	
	$fileDate = $inputDate.ToString("yyyy-MM-dd");
    InsertRecordToDatabaseForImages $cmd $viirs1DayFileNameFormat 1 136 $fileDate $currentWorkingFolder;
    InsertRecordToDatabaseForImages $cmd $viirs5DayFileNameFormat 6 136 $fileDate $currentWorkingFolder;
    InsertRecordToDatabaseForImages $cmd $ahiFileNameFormat 3 8 $fileDate $currentWorkingFolder;
    InsertRecordToDatabaseForImages $cmd $abiFileNameFormat 2 8 $fileDate $currentWorkingFolder;
    InsertRecordToDatabaseForImages $cmd $viirsAhiFileNameFormat 5 136 $fileDate $currentWorkingFolder;
    InsertRecordToDatabaseForImages $cmd $viirsAbiFileNameFormat 4 136 $fileDate $currentWorkingFolder;

	$con.Close();
	popd
}

function Move-FilesByType([string]$folderName, [string]$fileType)
{
	$fileExtension = $fileType;
	if ($fileType -eq "shapefile")
	{
		$fileExtension = "zip";
	}

    $moveDestination = Join-Path ./$folderName $fileType;
	if(($fileType -eq "png") -or ($fileType -eq "kml"))
	{
        $moveDestination = "./$folderName";
    }

	Log-Message "Move $fileType files to $moveDestination start.";

	# Can not handle files mixed more than 5 days
    Get-Item -Path ./*$folderName*.$fileExtension | Move-Item -Destination $moveDestination

    Log-Message "Move $fileType files to $moveDestination finish.";
}

function MoveAndCompressSourceFiles([string[]]$fileTypes, [string]$directory, [string]$searchFormat, [Int32]$maxIndex, [string]$archiveFolder, [string]$compressDestination)
{
    for($n = 0; $n -lt $fileTypes.Count; $n++)
    {
        $fileType = $fileTypes[$n];
        $fileExtension = GetFileExtentionByType $fileType;

        $searchDirectory = Join-Path $directory $fileType
        if($fileType -eq "kml")
        {
            $searchDirectory = $directory;
        }

        if($fileType -eq "png")
        {
            # Already handled by kml file
            continue;
        }

        $searchFormatWithFileExtension = $searchFormat + $fileExtension;

        for($i = 1; $i -le $maxIndex; $i++)
        {
		    $pathPattern = $searchFormatWithFileExtension -f $i.ToString("d3");
		    $dataFiles = Get-Item -Path (Join-Path $searchDirectory $pathPattern);

		    if ($dataFiles.Count -gt 0)
		    {
			    # No dup for re-entry
			    MoveDupFilesToArchive $dataFiles $archiveFolder;
			
			    $dataFile = $dataFiles[$dataFiles.Count-1];
			    $compressedFile = Join-Path $compressDestination $dataFile.Name;

                if ($fileType -ne "shapefile")
                {
			        $compressedFile = $compressedFile + ".zip";
                }

			    if(Test-Path $compressedFile)
			    {
				    continue;
			    }

	            Log-Message "Compress file to $compressedFile";
                
                if($fileType -eq "kml")
                {
                    $pngFileFullName = $dataFile.FullName.Replace(".kml", ".png");
                    if(Test-Path $pngFileFullName)
                    {
                        Compress-Archive -Path ($dataFile.FullName, $pngFileFullName) -DestinationPath $compressedFile -Force;
                    }
                }
                else
                {
                    Compress-Archive -Path $dataFile.FullName -DestinationPath $compressedFile -Force;
                }
		    }
        }
    }
}

function GetFileExtentionByType([string]$fileType)
{
    if ($fileType -eq "tif") {return ".tif";}
    if ($fileType -eq "hdf") {return ".hdf";}
    if ($fileType -eq "kml") {return ".kml";}
    if ($fileType -eq "png") {return ".png";}
    if ($fileType -eq "shapefile") {return ".zip";}
        
}

function InsertRecordToDatabaseForImages([Mysql.Data.MySqlClient.MySqlCommand]$mySqlCommond, [string]$searchFormat, [int]$productId, [int]$maxIndex, [string]$date, [string]$currentWorkingFolder)
{
    $searchFormat = $searchFormat + ".kml";

    for($i = 1; $i -le $maxIndex; $i++)
    {
		$pathPattern = $searchFormat -f $i.ToString("d3");

        # $currentWorkingFolder is /home/raw-data-geo/20191004
        # Must return only 1 file after dedup, possible be 0 if not exists
        $kmlFile = Get-Item -Path (Join-Path $currentWorkingFolder $pathPattern);

        if($kmlFile -ne $null)
        {
            $fileBaseName = $kmlFile.BaseName;

			Log-Message "Insert database record for $fileBaseName ...";

            $fileDate = "$date 01:00:00";
            $mySqlCommond.CommandText = "INSERT INTO jpssflood.kmlmetadata (ProductId, RegionId, DistrictId, Date, FileName, Version) SELECT $productId, 1, $i, '$fileDate', '$fileBaseName', 3 FROM jpssflood.kmlmetadata WHERE NOT EXISTS (SELECT 1 FROM jpssflood.kmlmetadata WHERE FileName = '$fileBaseName') LIMIT 1";
            $mySqlCommond.Prepare();
            $mySqlCommond.ExecuteNonQuery();
        }
    }
}

function CopyDisplayImages([string]$fileBaseNameFormat, [string]$currentWorkingFolder, [string]$compressDestination, [int]$maxIndex)
{
    $fileBaseNameFormat = $fileBaseNameFormat + ".kml";
        
    for($i = 1; $i -le $maxIndex; $i++)
	{
		$pathPattern = $fileBaseNameFormat -f $i.ToString("d3");

        # Must return only 1 file after dedup
        $kmlFile = Get-Item -Path (Join-Path $currentWorkingFolder $pathPattern);
		if($kmlFile -ne $null)
		{
			$srcKmlFilePath = $kmlFile.FullName;
            $fileBaseName = $kmlFile.BaseName;

            $destinationFilePath = Join-Path $compressDestination ($fileBaseName + ".kml");
            Log-Message "Copy $srcKmlFilePath to $destinationFilePath"
			Copy-Item -Path $srcKmlFilePath -Destination $destinationFilePath -Force

			$srcPngFilePath = $kmlFile.FullName.Replace(".kml", ".png");
            if(Test-Path $srcPngFilePath)
            {
                $destinationFilePath = Join-Path $compressDestination ($fileBaseName + ".png");
                Log-Message "Copy $srcPngFilePath to $destinationFilePath"
			    Copy-Item -Path $srcPngFilePath -Destination $destinationFilePath -Force;
            }
		}
        else
        {
            Log-Message "No file found when search $pathPattern. Skip copy.";
        }
    }
}

function GenerateViirsDisplayImages([string]$fileBaseNameFormat, [string]$currentWorkingFolder, [string]$compressDestination)
{
    $fileBaseNameFormat = $fileBaseNameFormat + ".kml";

    for($i = 1; $i -le 136; $i++)
	{
		$pathPattern = $fileBaseNameFormat -f $i.ToString("d3");

        # Must return only 1 file after dedup
        $kmlFile = Get-Item -Path (Join-Path $currentWorkingFolder $pathPattern);
		if($kmlFile -ne $null)
		{
			$srcKmlFilePath = $kmlFile.FullName;
			$srcPngFilePath = $kmlFile.FullName.Replace(".kml", ".png");
			$fileBaseName = $kmlFile.BaseName;

			try
			{
				Log-Message "Processing $srcPngFilePath ...";
				$destinationFilePath = Join-Path $compressDestination ($fileBaseName + ".png");

				if(Test-Path $destinationFilePath)
				{
					continue;
				}

				ReduceImageQuality $srcPngFilePath $destinationFilePath 556 556;
				$destinationFilePath = Join-Path $compressDestination ($fileBaseName + ".kml");
				Copy-Item -Path $srcKmlFilePath -Destination $destinationFilePath -Force

				$destinationFilePath = Join-Path $compressDestination ($fileBaseName +"_L1.png");
				ReduceImageQuality $srcPngFilePath $destinationFilePath 1112 1112
				$destinationFilePath = Join-Path $compressDestination ($fileBaseName + "_L1.kml");
				Copy-Item -Path $srcKmlFilePath -Destination $destinationFilePath -Force

				$destinationFilePath = Join-Path $compressDestination ($fileBaseName + "_L2.png");
				Copy-Item -Path $srcPngFilePath -Destination $destinationFilePath -Force
				$destinationFilePath = Join-Path $compressDestination ($fileBaseName + "_L2.kml");
				Copy-Item -Path $srcKmlFilePath -Destination $destinationFilePath -Force

				$content = Get-Content -Path $destinationFilePath;
				for($l = 0; $l -lt $content.Count; $l++)
				{
					if($content[$l].Contains($fileBaseName + ".png"))
					{
						$content[$l] = $content[$l].Replace($fileBaseName, $fileBaseName + "_L2");
					}
				}
				Set-Content -Path $destinationFilePath -Value $content

				Log-Message "Processing $srcPngFilePath finished";
			}

			catch
			{
				#Write-Error "Process $srcPngFilePath failed!" -ErrorAction Continue;
				continue;
			}
		}
    }
}

function MoveDupFilesToArchive([System.IO.FileInfo[]]$files, [String]$archiveFolder)
{
	if($files.Count -gt 1)
    {
        for ($i = 0; $i -lt $files.Count - 1; $i++)
        {
			$fileFullName = $files[$i].FullName;
            Log-Message "Move $fileFullName archive folder $archiveFolder";
			Move-Item -Path $fileFullName -Destination $archiveFolder -Force;
        }
    }
}

function ReduceImageQuality([String]$pngFile, [string]$destinationFile, [Int32]$height = 1112, [Int32]$width = 1112)
{
	if(-not(Test-Path $pngFile))
    {
        Write-Host "Can not find image file: $pngFile";
    }

    $pngFileFullPath = Resolve-Path $pngFile;
    [System.Drawing.Image]$rawImage = [System.Drawing.Image]::FromFile($pngFileFullPath.Path);
    $newBitmap = New-Object -TypeName System.Drawing.Bitmap -ArgumentList $rawImage, $width, $height;
    [System.Drawing.Graphics]$newGraphic = [System.Drawing.Graphics]::FromImage($newBitmap);

	<#Use default setting to save small image file, do not use high quality configurations#>
    #$newGraphic.CompositingQuality = [System.Drawing.Drawing2D.CompositingQuality]::HighQuality;
    #$newGraphic.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality;
    #$newGraphic.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic;

    $newRectangle = New-Object -TypeName System.Drawing.Rectangle -ArgumentList 0, 0, $width, $height;
    $newGraphic.DrawImage($rawImage, $newRectangle);
    $newBitmap.Save($destinationFile);

	$newGraphic.Dispose();
	$newBitmap.Dispose();
	$rawImage.Dispose();
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