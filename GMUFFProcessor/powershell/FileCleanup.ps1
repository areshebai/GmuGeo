############################################################################
#
# Clean up files on ftp download folder
#
############################################################################

function Clean-ExpiredDownloadFiles()
{
    ############################################################
    #
    # Definitions
    #
    ############################################################
	$daysToKeep = 30;
    $compressDestination = "/var/ftp/";
	$directories = Get-ChildItem -Path $compressDestination -Directory;

	Add-type -Assembly /opt/microsoft/powershell/6/MySql.Data.dll;

	foreach ($directory in $directories)
	{
		if ($directory.CreationTimeUtc.AddDays($daysToKeep) -lt [System.DateTime]::UtcNow)
		{
			# $pos = $directory.Name.LastIndexOf('_');
			# $taskName = $directory.Name.Substring(0, $pos);
			# $con.Open();
			# $cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
			# $cmd.Connection = $con;
			# $cmd.CommandText = "UPDATE jpssflood.downloadtasks_v2 SET Status = 4 WHERE Name = $($taskName)";
			# $cmd.ExecuteNonQuery();
			# $con.Close();
			Write-Host "Deleting expired download files in $($directory.FullName)"
			$directory.FullName | Out-File /home/raw-geo-data/job/cleanupdownloadfiles.log -Append
			$directory | Remove-Item -Force -Recurse
		}
	}
}

function Clean-DownloadedFiles()
{
	$ftp = [System.Net.FtpWebRequest]::Create("ftp://jpssflood.gmu.edu/")
	$ftp = [System.Net.FtpWebRequest]$ftp
	$ftp.Method = [System.Net.WebRequestMethods+Ftp]::ListDirectory
	$ftp.Credentials = new-object System.Net.NetworkCredential("anonymous","anonymous@jpssflood.gmu.edu")
	$ftp.UseBinary = $true
	$ftp.UsePassive = $true
	$ftpResponse = $ftp.GetResponse()
	$responseStream = $ftpResponse.GetResponseStream()
	$streamReader = New-Object System.IO.StreamReader $ResponseStream  
	

	$filesOrDirectories = New-Object System.Collections.ArrayList
	While ($fileOrDirectory = $streamReader.ReadLine())
	{
	[void] $filesOrDirectories.add("$fileOrDirectory")      
	}
	$filesOrDirectories | Out-File "d:\logs\FtpConnectLogs.txt"
}

function Compress-RawDataFiles ([DateTime]$inputDate)
{
	$dateString = $inputDate.ToString("yyyyMMdd")
	$path = "/home/raw-geo-data/" + $dateString + "/tif/"
	Write-Host $path
	pushd $path
	Get-Item *.tif | Foreach-Object { Write-Host $_.Name; Compress-Archive $_.Name ($_.Name + ".zip") }
	del *.tif
	popd
	$path = "/home/raw-geo-data/" + $dateString + "/hdf/"
	Write-Host $path
	pushd $path
	Get-Item *.hdf | Foreach-Object { Write-Host $_.Name; Compress-Archive $_.Name ($_.Name + ".zip") }
	del *.hdf
	popd
}

$now = [System.DateTime]::get_UtcNow();
$now = $now.AddDays(-2);