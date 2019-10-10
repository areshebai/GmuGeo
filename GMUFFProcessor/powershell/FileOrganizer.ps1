
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
	if(-not(Test-Path $folderName))
	{
		mkdir $folderName
	}
	if(-not(Test-Path $folderName/tif))
	{
		mkdir $folderName/tif
	}
	if(-not(Test-Path $folderName/hdf))
	{
		mkdir $folderName/hdf
	}
	if(-not(Test-Path $folderName/shapefile))
	{
		mkdir $folderName/shapefile
	}

	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : Move tif files to $folderName start.";
	Get-Item -Path .\*$folderName*.tif | Move-Item -Destination .\$folderName/tif
	# Get-Item -Path .\$folderName/*.tif | Move-Item -Destination .\$folderName/tif
	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : Move tif files to $folderName finish.";

	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : Move hdf files to $folderName start.";
	Get-Item -Path .\*$folderName*.hdf | Move-Item -Destination .\$folderName/hdf
	# Get-Item -Path .\$folderName/*.hdf | Move-Item -Destination .\$folderName/hdf
	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : Move hdf files to $folderName finish.";

	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : Move shapefile files to $folderName start.";
	Get-Item -Path .\*$folderName*.zip | Move-Item -Destination .\$folderName/shapefile
	# Get-Item -Path .\$folderName/*.zip | Move-Item -Destination .\$folderName/shapefile
	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : Move shapefile files to $folderName finish.";

	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : Move kml files to $folderName start.";
	Get-Item -Path .\*$folderName*.kml | Move-Item -Destination .\$folderName
	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : Move kml files to $folderName finish.";

	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : Move png files to $folderName start.";
	Get-Item -Path .\*$folderName*.png | Move-Item -Destination .\$folderName
	$logTime = [System.DateTime]::get_UtcNow();
	Write-Host "$logTime : Move png files to $folderName finish.";

	popd
}