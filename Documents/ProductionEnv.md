
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

**Web App Configuration** /etc/httpd/conf.d/gmuffapp.conf  
**Web App monitor service** /etc/systemd/system/kestrel-gmuffapp.service  
**Web App deplooyment path** /var/www/gmuffapp  
**Kml location for Google Map API** /home/www-html/kmls
**FTP file server for download** /var/ftp/

### CronJob Configuration

```powershell
New-CronJob -Command {pwsh -c '. /home/raw-geo-data/job/FileOrganizer.ps1; $now | Out-File /home/raw-geo-data/job/move.log -Append; MoveFtpFile $now'} -Minute '*/30' | Out-Host

New-CronJob -Command {pwsh -c '$now = [System.DateTime]::get_UtcNow(); $now | Out-File /var/ftp/test.log -Append;'} -Minute '*/1' | Out-Host

New-CronJob -Command {pwsh -c '. /home/raw-geo-data/job/FileCompressor.ps1; Compress-ImagesToZip'} -Minute '*/5' | Out-Host
```

# Google Earth Engine Guide

## Account Information

yan.wang.gmu@gmail.com (expired)  
ywang95.gmu.edu@gmail.com  

## API Key

AIzaSyDhIevfSa6lw6HauJuesotlyxxbOR_kmsA (expired)  
AIzaSyCXVemTzeOvkKbvJFWi60hkwaIwD37s1LA  

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

New-CronJob -Command {pwsh -c '$now = [System.DateTime]::get_UtcNow(); $now | Out-File /var/ftp/test.log -Append;'} -Minute '*/1' | Out-Host

New-CronJob -Command {pwsh -c '. /home/raw-geo-data/job/FileCompressor.ps1; Compress-ImagesToZip'} -Minute '*/5' | Out-Host

sudo -s //login as root user  
