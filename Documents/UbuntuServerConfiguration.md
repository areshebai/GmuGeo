Ubuntu Setup</br>


Mysql Database
==================================================================
Install mysql    
    sudo apt-get update
    sudo apt-get install mysql-server
    mysql -V

Reset root password for mysql
    sudo cat /etc/mysql/debian.cnf
    mysql -u debian-sys-maint -p
    USE mysql
    SELECT User, Host, plugin FROM mysql.user;
    UPDATE user SET authentication_string=PASSWORD('07Apples') WHERE user='root';
    FLUSH PRIVILEGES;
    exit

Mysql trial
    mysql -u root -p
    show databases

Apache web server
==================================================================
    sudo apt-get update
    sudo apt-get install apache2 -y
    apache2 - V

    sudo service apache2 restart

ASP.net core and SDK
==================================================================
Refer to this document: https://odan.github.io/2018/07/17/aspnet-core-2-ubuntu-setup.html

Before installing .NET, you’ll need to register the Microsoft key, register the product repository, and install required dependencies. This only needs to be done once per machine.

Open a command prompt and run the following commands:
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
wget -q https://packages.microsoft.com/config/ubuntu/18.04/prod.list 
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