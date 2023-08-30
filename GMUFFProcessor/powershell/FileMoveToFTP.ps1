function Move-ToFTP([DateTime]$inputDate)
{
    ############################################################
    #
    # Definitions
    #
    ############################################################
    $ftpDestination= "/var/ftp/pub";
    $dateString = $inputDate.ToString("yyyyMMdd")
    $ftpDestinationDate = "/var/ftp/pub/" + $dateString;
    if (-not(Test-Path $ftpDestinationDate))
    {
        $sourcePath = "/home/raw-geo-data/" + $dateString;
        Copy-Item -Path $sourcePath -Destination $ftpDestination -Recurse
    }
    else 
    { 
        $ftpDestination = $ftpDestinationDate;
        if (-not(Test-Path ($ftpDestinationDate + "/netCDF")))
        {
            $sourcePath = "/home/raw-geo-data/" + $dateString + "/netCDF";
            Copy-Item -Path $sourcePath -Destination $ftpDestination -Recurse
        }
    }
	
}

$moveDate = Get-Date -Year 2019 -Month 9 -Day 20