The installation file is in \AutoCADNetWizardsInstaller\
AutoCADNetWizards.msi or
AutoCAD_2016_dotnet_wizards.zip

Installing a managed DLL into the GAC on a developer machine without installer
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\gacutil" -I "ArxLocatorWizard2010\bin\Release\ObjectARXLocatorWizard.dll"

Test an installer with log file
Standard Install
msiexec /i AutoCADNetWizards.msi /liwearucmopvx!* Tests\test.txt
Admin Install
msiexec /a AutoCADNetWizards.msi /qf /liwearucmopvx!* Tests\test.txt
Unistall
msiexec /x AutoCADNetWizards.msi