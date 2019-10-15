
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
	$folderName = $inputDate.ToString("yyyyMMdd");
	$compressDestination = "/home/www-html/kmls/"

	$viirs1DayFileNameFormat = 'WATER_COM_VIIRS_Prj_SVI_*_*_*_*_*_001day_{0}.tif';
	$viirs5DayFileNameFormat = 'WATER_COM_VIIRS_Prj_SVI_*_*_*_*_*_005day_{0}.tif';
	$ahiFileNameFormat = 'COM_H08_AHI_WATER_*_*_*_*_*_*_*_*_*_{0}.tif';
	$abiFileNameFormat = 'COM_*_ABI_WATER_*_*_*_*_*_*_*_*_*_{0}.tif';
	$viirsAhiFileNameFormat = 'Joint_VIIRS_AHI_WATER_Prj_SVI_*_*_*_*_{0}.tif';
	$viirsAbiFileNameFormat = 'Joint_VIIRS_ABI_WATER_Prj_SVI_*_*_*_*_{0}.tif';

    ############################################################
	#
	# 1. Create root folder and subfolders
	#
	############################################################
	if(-not(Test-Path $folderName))
	{
		# Create folder /home/raw-geo-data/{date}
		mkdir $folderName
	}
	if(-not(Test-Path $folderName/tif))
	{
		# Create folder /home/raw-geo-data/{date}/tif
		mkdir $folderName/tif
	}
	if(-not(Test-Path $folderName/hdf))
	{
		# Create folder /home/raw-geo-data/{date}/hdf
		mkdir $folderName/hdf
	}
	if(-not(Test-Path $folderName/shapefile))
	{
		# Create folder /home/raw-geo-data/{date}/shapefile
		mkdir $folderName/shapefile
	}
	if(-not(Test-Path $folderName/archive))
	{
		# Create folder /home/raw-geo-data/{date}/shapefile
		mkdir $folderName/archive
	}

	$tifFolder = (Resolve-Path ./$folderName/tif).Path;
	$hdfFolder = (Resolve-Path ./$folderName/hdf).Path;
	$archiveFolder = (Resolve-Path ./$folderName/archive).Path;

	############################################################
	#
	# 2. Move files by type
	#
	############################################################
	$fileTypes = @("tif", "hdf", "shapefile", "png", "kml");
	for($i = 0; $i -lt fileTypes.Count; $i++)
	{
		$fileType = fileTypes[$i];

		$fileExtension = $fileType == "shapefile" ? "zip" : $fileType;

		$logTime = [System.DateTime]::get_UtcNow();
		Write-Host "$logTime : Move $fileType files to $folderName start.";
		Get-Item -Path ./*$folderName*.$fileExtension | Move-Item -Destination ./$folderName/$fileType
		# Get-Item -Path ./$folderName/*.$fileExtension | Move-Item -Destination ./$folderName/$fileType
		$logTime = [System.DateTime]::get_UtcNow();
		Write-Host "$logTime : Move $fileType files to $folderName finish.";
	}

    ############################################################
	#
	# 3. Compress and move files to https folder
	#
	############################################################

	MoveAndCompressSourceFiles $tifFolder $viirs1DayFileNameFormat 136;
	MoveAndCompressSourceFiles $tifFolder $viirs5DayFileNameFormat 136;
	MoveAndCompressSourceFiles $tifFolder $ahiFileNameFormat 8;
	MoveAndCompressSourceFiles $tifFolder $$abiFileNameFormat 8;
	MoveAndCompressSourceFiles $tifFolder $viirsAhiFileNameFormat 136;
	MoveAndCompressSourceFiles $tifFolder $viirsAbiFileNameFormat 136;

	$viirs1DayFileNameFormat = "WATER_COM_VIIRS_Prj_SVI_*_*_*_*_*_001day_{0}.tif";
	for($i = 1; $i -le 136; $i++)
	{
		$pathPattern = $viirs1DayFileNameFormat -f $i.ToString("d3");
		$tifFiles = Get-Item -Path (Join-Path $tifFolder $pathPattern);

		# tif, hdf, kml, png and shapefile handled together in the function
		MoveDupFilesToArchive $tifFiles $archiveFolder;

		# tif, hdf, kml, png and shapefile handled together in the function
		CompressFileToHttpFolder $tifFiles[$tifFiles.Count-1] $compressDestination
	}

    ############################################################
	#
	# Create database records
	#
	############################################################
	Add-type -Assembly /opt/microsoft/powershell/6/MySql.Data.dll;
	$con = New-Object Mysql.Data.MySqlClient.MySqlConnection;
	$con.ConnectionString = "server=localhost;userid=root;database=jpssflood;";
	$con.Open();
	$cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
	$cmd.CommandText = "SELECT * FROM jpssflood.kmlmetadata WHERE ProductId = 100";
	$cmd.Connection = $con;
    $cmd.Prepare();
	$data = $cmd.ExecuteReader(); 
	# $data.GetString(5)
	$dataList = @();
	
	while ($data.Read() eq $true)
	{
		$tempObject = New-Object -TypeName PSObject;
		for ($i = 0; $i -lt $data.FieldCount; $i++)
		{
			$fieldType = $data.GetFieldType($i);
			Switch ($fieldType.Name)
			{
				"Int32" {$cellData = $data.GetInt32($i);break}
				"String" {$cellData $data.GetString($i);break}
				default {$filedType.Name}
			}

			Add-Member -InputObject $tempObject -MemberType "NoteProperty" -Name $data.GetName($i) -Value $cellData;
		}
		$dataList += $temObject
	}

	$con.Close();

	popd
}

function MoveAndCompressSourceFiles([string]$directory, [string]$searchFormat, [Int32]$maxIndex)
{
	for($i = 1; $i -le $maxIndex; $i++)
	{
		$pathPattern = $searchFormat-f $i.ToString("d3");
		$tifFiles = Get-Item -Path (Join-Path $directory $pathPattern);

		if (tifFiles.Count -gt 0)
		{
			# tif, hdf, kml, png and shapefile handled together in the function
			MoveDupFilesToArchive $tifFiles $archiveFolder;

			# tif, hdf, kml, png and shapefile handled together in the function
			CompressFileToHttpFolder $tifFiles[$tifFiles.Count-1] $compressDestination
		}
	}
}

function CompressFileToHttpFolder([System.IO.FileInfo]$file, [string]$compressDestination)
{
	$compressedFileName = (Join-Path $compressDestination $file.Name) + ".zip";
	Compress-Archive -Path $file.FullName -DestinationPath $compressedFileName -Force;

	$compressedFileName = (Join-Path $compressDestination $file.Name.Replace(".tif", ".hdf")) + ".zip";
	Compress-Archive -Path $file.FullName.Replace(".tif", ".hdf") -DestinationPath $compressedFileName -Force;

	$compressedFileName = (Join-Path $compressDestination $file.Name.Replace(".tif", ".kml")) + ".zip";
	Compress-Archive -Path ($file.FullName.Replace(".tif", ".kml"), $file.FullName.Replace(".tif", ".png")) -DestinationPath $compressedFileName -Force;

	$compressedFileName = (Join-Path $compressDestination $file.Name.Replace(".tif", ".zip"));
	$copy-Item -Path $file.FullName.Replace(".tif", ".zip") -Destination $compressedFileName;

}

function MoveDupFilesToArchive([System.IO.FileInfo[]]$files, [String]$archiveFolder)
{
	if($files.Count -gt 1)
    {
        for ($i = 0; $i -lt $files.Count - 1; $i++)
        {
			$fileFullName = $files[$i].FullName;

            Move-Item -Path $fileFullName -Destination $archiveFolder -Force;
			Move-Item -Path $fileFullName.Replace(".tif", ".hdf") -Destination $archiveFolder -Force;
			Move-Item -Path $fileFullName.Replace(".tif", ".kml") -Destination $archiveFolder -Force;
			Move-Item -Path $fileFullName.Replace(".tif", ".png") -Destination $archiveFolder -Force;
			Move-Item -Path $fileFullName.Replace(".tif", ".zip") -Destination $archiveFolder -Force;
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

    #$newGraphic.CompositingQuality = [System.Drawing.Drawing2D.CompositingQuality]::HighQuality;
    #$newGraphic.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality;
    #$newGraphic.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic;

    $newRectangle = New-Object -TypeName System.Drawing.Rectangle -ArgumentList 0, 0, $width, $height;

    $newGraphic.DrawImage($rawImage, $newRectangle);

    $newBitmap.Save($destinationFile);
}