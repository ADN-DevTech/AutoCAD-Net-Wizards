@echo off
%~d0
cd /d %~dp0

if exist w:\nul subst w: /d
subst w: "C:\Program Files (x86)\WiX Toolset v3.11"

w:\bin\candle.exe AutoCADNetWizards.wxs -out temp\AutoCADNetWizards.wixobj
w:\bin\light.exe -ext "w:\bin\WixNetFxExtension.dll" -sw1076 -b .. temp\AutoCADNetWizards.wixobj -out AutoCADNetWizards.msi 
REM rem -ext WixUIExtension
 if exist AutoCAD_2019_dotnet_wizards.zip del AutoCAD_2019_dotnet_wizards.zip > nul
"C:\Program Files\WinRAR\WinRAR.exe" a AutoCAD_2019_dotnet_wizards.zip AutoCADNetWizards.msi > nul

pause