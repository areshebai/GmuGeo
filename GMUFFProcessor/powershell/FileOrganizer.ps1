
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

	# Create folder /home/raw-geo-data/{date
	if(-not(Test-Path $folderName))
	{
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

	############################################################
	#
	# Move tif files
	#
	############################################################
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