function Get-JpssFloodConfiguration {
    param (
        [string] $configFilePath =  ".\JpssFlood.config"
    )
    
    $config = Get-Content $configFilePath | ConvertFrom-Json;

    return $config;
}

function Compress-GEODataFiles {
    Param (
        [DateTime]$inputDate,
        [string]$rootPath,
        [bool]$test = $true
    )

	$dateString = $inputDate.ToString("yyyyMMdd")
	$path = $rootPath + $dateString + "/tif/"
	Write-Host "Processing $path"
	Push-Location $path
    $tiffFiles = @(Get-ChildItem *.tif)
    if ($test) {
        Write-Host $tiffFiles.Count " tiff files found."
    }
    else {
	    $tiffFiles | Foreach-Object { Write-Host $_.Name; Compress-Archive $_.Name ($_.Name + ".zip") }
	    Remove-Item*.tif
    }
	Pop-Location

	$path = $rootPath + $dateString + "/hdf/"
	Write-Host "Processing $path"
	Push-Location $path
    $hdfFiles = @(Get-ChildItem *.hdf)
    if ($test) {
        Write-Host $hdfFiles.Count " hdf files found."
    } else {
	    $hdfFiles | Foreach-Object { Write-Host $_.Name; Compress-Archive $_.Name ($_.Name + ".zip") }
	    Remove-Item *.hdf
    }
	Pop-Location
}

function Compress-RawDataFile ([DateTime]$inputDate)
{
    $rootPath = "/home/raw-geo-data/"
    Compress-GEODataFiles $inputDate $rootPath $false
}

function Compress-FTPDataFile ([DateTime]$inputDate)
{
    $rootPath = "/var/ftp/pub/"
    Compress-GEODataFiles $inputDate $rootPath $false
}

$startDate = Get-Date -Year 2023 -Month 10 -Day 01
$rootPath = "/home/raw-geo-data/"
for ($i = 0; $i -le 30; $i++) {
    Compress-GEODataFiles $startDate $rootPath $false
    $startDate = $startDate.AddDays(1);
}

function Cleanup-InvalidDaysFile([DateTime]$inputDate)
{
    $dateString = $inputDate.ToString("yyyyMMdd")
    $ftpRootPath = "/var/ftp/pub/"
    $path = $ftpRootPath + $dateString

    Write-Host "Processing $path"
    Push-Location $path
    $invalidFiles = @(Get-ChildItem *_002day_* -Recurse)
    Write-Host $invalidFiles.Count " 002day files found."
    $invalidFiles | Remove-Item -Force

    $invalidFiles = @(Get-ChildItem *_003day_* -Recurse)
    Write-Host $invalidFiles.Count " 003day files found."
    $invalidFiles | Remove-Item -Force

    $invalidFiles = @(Get-ChildItem *_004day_* -Recurse)
    Write-Host $invalidFiles.Count " 004day files found."
    $invalidFiles | Remove-Item -Force
    Pop-Location

    $rawRootPath = "/home/raw-geo-data/"
    $path = $rawRootPath + $dateString

    Write-Host "Processing $path"
    Push-Location $path
    $invalidFiles = @(Get-ChildItem *_002day_* -Recurse)
    Write-Host $invalidFiles.Count " 002day files found."
    $invalidFiles | Remove-Item -Force

    $invalidFiles = @(Get-ChildItem *_003day_* -Recurse)
    Write-Host $invalidFiles.Count " 003day files found."
    $invalidFiles | Remove-Item -Force

    $invalidFiles = @(Get-ChildItem *_004day_* -Recurse)
    Write-Host $invalidFiles.Count " 004day files found."
    $invalidFiles | Remove-Item -Force
    Pop-Location
}

######################## Test Runs #########################
<#

$startDate = Get-Date -Year 2023 -Month 07 -Day 01
$rootPath = "/home/raw-geo-data/"
for ($i = 0; $i -le 30; $i++) {
    Compress-GEODataFiles $startDate $rootPath $false
    $startDate = $startDate.AddDays(1);
}

for ($i = 0; $i -le 3; $i++) {
    Cleanup-InvalidDaysFile $startDate
    $startDate = $startDate.AddDays(1);
}

cp -r /home/raw-geo-data/20230620 /var/ftp/pub
cp -r /home/raw-geo-data/20230621 /var/ftp/pub
cp -r /home/raw-geo-data/20230622 /var/ftp/pub
cp -r /home/raw-geo-data/20230623 /var/ftp/pub
cp -r /home/raw-geo-data/20230624 /var/ftp/pub
cp -r /home/raw-geo-data/20230625 /var/ftp/pub
cp -r /home/raw-geo-data/20230626 /var/ftp/pub
cp -r /home/raw-geo-data/20230707 /var/ftp/pub
cp -r /home/raw-geo-data/20230708 /var/ftp/pub
cp -r /home/raw-geo-data/20230709 /var/ftp/pub
cp -r /home/raw-geo-data/20230710 /var/ftp/pub
cp -r /home/raw-geo-data/20230711 /var/ftp/pub
cp -r /home/raw-geo-data/20230712 /var/ftp/pub
cp -r /home/raw-geo-data/20230713 /var/ftp/pub
cp -r /home/raw-geo-data/20230714 /var/ftp/pub
cp -r /home/raw-geo-data/20230715 /var/ftp/pub
cp -r /home/raw-geo-data/20230716 /var/ftp/pub
cp -r /home/raw-geo-data/20230717 /var/ftp/pub


rm -r /var/ftp/pub/20230620
rm -r /var/ftp/pub/20230621
rm -r /var/ftp/pub/20230622
rm -r /var/ftp/pub/20230623
rm -r /var/ftp/pub/20230624
rm -r /var/ftp/pub/20230625
rm -r /var/ftp/pub/20230626
rm -r /var/ftp/pub/20230707
rm -r /var/ftp/pub/20230708
rm -r /var/ftp/pub/20230709
rm -r /var/ftp/pub/20230710
rm -r /var/ftp/pub/20230711
rm -r /var/ftp/pub/20230712
rm -r /var/ftp/pub/20230713
rm -r /var/ftp/pub/20230714
rm -r /var/ftp/pub/20230715
rm -r /var/ftp/pub/20230716
rm -r /var/ftp/pub/20230717
#>