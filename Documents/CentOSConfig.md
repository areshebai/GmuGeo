# CentOS Server Configuration

## Server

**SSH hostname:** jpssflood.gmu.edu  
**Username:** ywang95  
**Password:** 9c3f-6ee2b2a492f3  

## Install the .NET Runtime

### register the Microsoft key

Before installing .NET, you'll need to register the Microsoft key, register the product repository, and install required dependencies. This only needs to be done once per machine.
sudo rpm -Uvh <https://packages.microsoft.com/config/rhel/7/packages-microsoft-prod.rpm>

Install the .NET Runtime
sudo yum update
sudo yum install aspnetcore-runtime-2.1

## Framework-dependent deployments (FDD)

For an FDD, you deploy only your app and third-party dependencies. Your app will use the version of .NET Core that's present on the target system. This is the default deployment model for .NET Core and ASP.NET Core apps that target .NET Core.

Why create a framework-dependent deployment?
Deploying an FDD has a number of advantages:

You don't have to define the target operating systems that your .NET Core app will run on in advance. Because .NET Core uses a common PE file format for executables and libraries regardless of operating system, .NET Core can execute your app regardless of the underlying operating system. For more information on the PE file format, see .NET Assembly File Format.

The size of your deployment package is small. You only deploy your app and its dependencies, not .NET Core itself.

Unless overridden, FDDs will use the latest serviced runtime installed on the target system. This allows your application to use the latest patched version of the .NET Core runtime.

Multiple apps use the same .NET Core installation, which reduces both disk space and memory usage on host systems.

There are also a few disadvantages:

Your app can run only if the version of .NET Core your app targets, or a later version, is already installed on the host system.

It's possible for the .NET Core runtime and libraries to change without your knowledge in future releases. In rare cases, this may change the behavior of your app.

## Apache Server Test

apachectl configtest
