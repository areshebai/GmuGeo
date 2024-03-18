
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
	pushd $path
    $tiffFiles = @(Get-ChildItem *.tif)
    if ($test) {
        Write-Host $tiffFiles.Count " tiff files found."
    }
    else {
	    $tiffFiles | Foreach-Object { Write-Host $_.Name; Compress-Archive $_.Name ($_.Name + ".zip") }
	    del *.tif
    }
	popd

	$path = $rootPath + $dateString + "/hdf/"
	Write-Host "Processing $path"
	pushd $path
    $hdfFiles = @(Get-ChildItem *.hdf)
    if ($test) {
        Write-Host $hdfFiles.Count " hdf files found."
    } else {
	    $hdfFiles | Foreach-Object { Write-Host $_.Name; Compress-Archive $_.Name ($_.Name + ".zip") }
	    del *.hdf
    }
	popd
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

$startDate = Get-Date -Year 2023 -Month 10 -Date 01
$rootPath = "/home/raw-geo-data/"
for ($i = 0; $i -le 30; $i++) {
    Compress-GEODataFiles $startDate $rootPath $false
    $startDate = $startDate.AddDays(1);
}

function Cleanup-InvalidDaysFile([DateTime]$inputDate, [string]$pattern)
{
    $dateString = $inputDate.ToString("yyyyMMdd")
    $ftpRootPath = "/var/ftp/pub/"
    $path = $ftpRootPath + $dateString

    Write-Host "Processing $path"
    pushd $path
    $invalidFiles = @(Get-ChildItem *_004day_*)
    Write-Host $invalidFiles.Count " 004day files found."
    $invalidFiles | Remove-Item -Force
    popd

    $rawRootPath = "/home/raw-geo-data/"
    $path = $rawRootPath + $dateString

    Write-Host "Processing $path"
    pushd $path
    $invalidFiles = @(Get-ChildItem *_004day_*)
    Write-Host $invalidFiles.Count " 004day files found."
    $invalidFiles | Remove-Item -Force
    popd
}