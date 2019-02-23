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
    sudo apt-get update