############################################################################
#
# Clean up files on ftp download folder
#
############################################################################

function Clean-ZipFiles()
{
    ############################################################
    #
    # Definitions
    #
    ############################################################
	$daysToKeep = 10;
    $compressDestination = "/var/ftp/";
	$files = Get-ChildItem -Path $compressDestination -File;

	Add-type -Assembly /opt/microsoft/powershell/6/MySql.Data.dll;

	foreach ($file in $files)
	{
		if ($file.CreationTimeUtc.AddDays($daysToKeep) < [System.DateTime]::UtcNow())
		{
			$pos = $file.Name.LastIndexOf('_');
			$taskName = $files.Name.Substring(0, $pos);
			$file | Remove-Item -Force;

			$con.Open();
			$cmd = New-Object Mysql.Data.MySqlClient.MySqlCommand;
			$cmd.Connection = $con;
			$cmd.CommandText = "UPDATE jpssflood.downloadtasks_v2 SET Status = 4 WHERE Name = $($taskName)";
			$cmd.ExecuteNonQuery();
			$con.Close();
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