# Development environment setup

1. Install visual studio 2017 or visual Studio Code. ASP.net Core web development must be installed.  

2. Install jQWidgets for client controls.
    - jQWidgets is downloaded for free for our project. Copy the downloaded script to our project folder.  
    - Install jQWidgets.AspNetCore.Mvc.TagHelpers nuget package to current project.  
        Source: <https://www.jqwidgets.com/asp.net-core-mvc-tag-helpers-documentation/>  
        Nuget: <https://www.nuget.org/packages/jQWidgets.AspNetCore.Mvc.TagHelpers/>  
        In package manager console, run: Install-Package jQWidgets.AspNetCore.Mvc.TagHelpers -Version 2.0.1  
3. WinSCP .Net Assembly
    - Reference this assembly and access remote SFTP server programatically in .Net core application  
        Source: <https://www.nuget.org/packages/WinSCP/>  
        Nuget: <https://www.nuget.org/packages/WinSCP/>  
    - In package manager console, run: Install-Package WinSCP -Version 5.13.7  
