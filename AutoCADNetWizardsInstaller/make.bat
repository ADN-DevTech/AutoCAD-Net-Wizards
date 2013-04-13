@echo off
%~d0
cd /d %~dp0

if exist w:\nul subst w: /d
subst w: "C:\Program Files (x86)\WiX Toolset v3.8"

w:\bin\candle.exe AutoCADNetWizards.wxs -out temp\AutoCADNetWizards.wixobj
w:\bin\light.exe -sw1076 -b .. temp\AutoCADNetWizards.wixobj -out AutoCADNetWizards.msi 
rem -ext WixUIExtension
if exist del AutoCAD_2014_dotnet_wizards.zip del AutoCAD_2014_dotnet_wizards.zip > nul
"C:\Program Files\WinRAR\WinRAR.exe" a AutoCAD_2014_dotnet_wizards.zip AutoCADNetWizards.msi > nul

pause