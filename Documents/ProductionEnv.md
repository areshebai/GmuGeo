
# Environment Reference

## Website Url  

<https://jpssflood.gmu.edu/>  

## Kml URL

<https://jpssflood.gmu.edu/kmls/cspp-viirs-flood-globally_20180815_010000_53.kml>  

## MySQL database connection

Using ‘mysql workbench’ and configure connection as ‘Standard TCP/IP over SSH’  

**SSH hostname:** jpssflood.gmu.edu
**Username:** ywang95  
**Password:** 9c3f-6ee2b2a492f3  
**MySQL Hostname:** localhost  

### Web application location

> For localhost:5000, it is a web application started by dotnet runtime.

**Web App Configuration:** /etc/httpd/conf.d/gmuffapp.conf  
**Web App monitor service:** /etc/systemd/system/kestrel-gmuffapp.service  
**Web App deplooyment path:** /var/www/gmuffapp  
**Kml location for Google Map API:** /home/www-html/kmls  
**FTP file server for download:** /var/ftp/

### CronJob Configuration

```powershell
New-CronJob -Command {pwsh -c '. /home/raw-geo-data/job/FileOrganizer.ps1; $now | Out-File /home/raw-geo-data/job/move.log -Append; MoveFtpFile $now'} -Minute '*/30' | Out-Host

New-CronJob -Command {pwsh -c '$now = [System.DateTime]::get_UtcNow(); $now | Out-File /var/ftp/test.log -Append;'} -Minute '*/1' | Out-Host

New-CronJob -Command {pwsh -c '. /home/raw-geo-data/job/FileCompressor.ps1; Compress-ImagesToZip'} -Minute '*/5' | Out-Host
```

# Google Earth Engine Guide

## Account Information - 07Pine@pples18 - 07PineApples+2208 - 07PineApples+2211 - 07PineApples+2212
AIzaSyAjAUrw3XvRJjEUiN5FMnzpM9MNVD4T-jQ
Before-Solemn-Waste-Ahead-9

yan.wang.gmu@gmail.com (expired)  
ywang95.gmu.edu@gmail.com (expire on 09/20)  
yanwang.2020.gmu.edu@gmail.com (expired)
yanwang.20211.gmu.edu@gmail.com (expire on 03/02)
yanwang.202102.gmu.edu@gmail.com (expire on 05/24)
yanwang.202105.gmu.edu@gmail.com (expire on 08/20)
yanwang.202108.gmu.edu@gmail.com (expire on 11/20)
yanwang.202111.gmu.edu@gmail.com (expire on 02/20)
yanwang.202202.gmu.edu@gmail.com (expire on 05/18)
yanwang.202205.gmu.edu@gmail.com (expire on 08/18)
yanwang.202208.gmu.edu@gmail.com (expire on 11/15)
yanwang.202211.gmu.edu@gmail.com (expire on 02/14)
yanwang.202302.gmu.edu@gmail.com (expire on 05/24)
yanwang.202305.gmu.edu@gmail.com (expire on 08/21) - drive current
yanwang.202308.gmu.edu@gmail.com (expire on 11/20) - drive
yanwang.202311.gmu.edu@gmail.com (expire on 02/19)
yanwang.202402.gmu.edu@gmail.com (expire on 05/18)

## Google Cloud Platform

1. Login to <https://console.cloud.google.com/>, activate the free trial on top navigation bar.
Need home address and payment information.

2. Create new project <https://developers.google.com/maps/documentation/javascript/cloud-setup>
Navigate to "APIs & Services" -> "Credentials", create project in top dropdown list.

3. Enable Billing
Navigate to "Billing". Actually, it is already enabled when activate the free trail in step 1.

4. Enable APIs
Navigate to "APIs & Services" -> "Library" -> "Maps JavaScript API" -> Enable

5. Create API Key <https://developers.google.com/maps/documentation/javascript/get-api-key>
Navigate to "APIs & Services" -> "Credentials", click "Create credentials" -> API Key

## API Key

AIzaSyBkOzsMYQoMMvXYQHl5EtfLJGTHY8iRf3Q (created on 11/19/2021, for 90 days, expired)
AIzaSyCJvYmAwEYrsPxxj3QZjZyqDy279ctRa2g (created on 02/18/2022, for 90 days, expired)
AIzaSyB1G_EskQIOF4_iN8m1oHfRdX9f6Ns6Y84 (created on 05/18/2022, for 90 days, expired)
AIzaSyB2Ek2b2pRf55xM2sLnLoRuyft3TYj2Hlk (created on 08/17/2022, for 91 days, expired)
AIzaSyAXc0sgEvIkyQL5iQYfKKLQ-UtNrtSSZcg (created on 11/14/2022, for 91 days, expired)
AIzaSyBe7RvyOwm5QTPGajOrCphA7nG1mxWMGtQ (created on 02/24/2023, for 91 days, expired)
AIzaSyC16V2Qu3hoIYoov4Lo45aJNGKUhkKmOTM (created on 05/21/2023, for 91 days, expired)
AIzaSyBukgDQw-0XLytS9gp3mPoDNT2jC9MOkfk (created on 08/20/2023, for 91 days, expired)
AIzaSyDpjZabQvCct7BsXmOZq8x1hR7We9LDJRw (created on 11/19/2023, for 91 days, expired)

## Website

<https://gmugeo-floodforecast.appspot.com/> (retired)  

# MySQL Database

## Install MySQL

```powershell
    sudo apt-get update  
    sudo apt-get install mysql-server  
    mysql -V  
```

## Reset root password

```powershell
    sudo cat /etc/mysql/debian.cnf
    mysql -u debian-sys-maint -p
    USE mysql
    SELECT User, Host, plugin FROM mysql.user;
    UPDATE user SET authentication_string=PASSWORD('07Apples') WHERE user='root';
    FLUSH PRIVILEGES;
    exit
```

## MySQL command trial

```powershell
    mysql -u root -p
    show databases
```

# Apache web server

```powershell
    sudo apt-get update  
    sudo apt-get install apache2 -y  
    apache2 - V  
    sudo service apache2 restart  
```

# ASP.net core and SDK

Refer to this document: <https://odan.github.io/2018/07/17/aspnet-core-2-ubuntu-setup.html>

Before installing .NET, you’ll need to register the Microsoft key, register the product repository, and install required dependencies. This only needs to be done once per machine.

Open a command prompt and run the following commands:  
wget -qO- <https://packages.microsoft.com/keys/microsoft.asc> | gpg --dearmor > microsoft.asc.gpg  
sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/  
wget -q <https://packages.microsoft.com/config/ubuntu/18.04/prod.list>  
sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list  
sudo chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg  
sudo chown root:root /etc/apt/sources.list.d/microsoft-prod.list  

Install the .NET Runtime

Update the products available for installation, then install the .NET Runtime.

In your command prompt, run the following commands:  
sudo add-apt-repository universe
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install aspnetcore-runtime-2.1
sudo apt-get install dotnet-sdk-2.1

New-CronJob -Command {pwsh -c '. /home/raw-geo-data/job/FileOrganizer.ps1; $now | Out-File /home/raw-geo-data/job/move.log -Append; MoveFtpFile $now'} -Minute '*/30' | Out-Host

New-CronJob -Command {pwsh -c '. /home/raw-geo-data/job/FileOrganizer.ps1; $now = $now.AddDays(-1); $now | Out-File /home/raw-geo-data/job/move.log -Append; MoveFtpFile $now'} -Minute '*/30' | Out-Host

New-CronJob -Command {pwsh -c '$now = [System.DateTime]::get_UtcNow(); $now | Out-File /var/ftp/test.log -Append;'} -Minute '*/1' | Out-Host

New-CronJob -Command {pwsh -c '. /home/raw-geo-data/job/FileCompressor.ps1; Compress-ImagesToZip'} -Minute '*/5' | Out-Host

New-CronJob -Command {pwsh -c '. /home/raw-geo-data/job/FileCleanup.ps1; Compress-RawDataFiles $now; $now | Out-File /home/raw-geo-data/job/CompressRawDataFiles.log -Append'} -Hour '22' -Minute '1' | Out-Host

New-CronJob -Command {pwsh -c '. /home/raw-geo-data/job/FileCleanup.ps1; Clean-ExpiredDownloadFiles'} -Hour '23' -Minute '1' | Out-Host

sudo -s //login as root user  
