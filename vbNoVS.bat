rem This batch is to build the VB Wizard ZIP without Visual Studio
@echo off
%~d0
cd /d %~dp0

set ProjectName=AutoCAD VB plug-in
set ProjectDir=..\..\%ProjectName%\

mkdir Output 2>nul
mkdir "Output\%ProjectName%" 2>nul
mkdir "Output\%ProjectName%\My Project" 2>nul
cd "Output\%ProjectName%"

if exist "..\%ProjectName%.zip" del "..\%ProjectName%.zip"
del *.* /s /q > nul
copy /Y "%ProjectDir%%ProjectName% - 2019.vbproj" ".\%ProjectName%.vbproj" > nul
copy /Y "%ProjectDir%myPlugin.vb" . > nul
copy /Y "%ProjectDir%myCommands.vb" . > nul
copy /Y "%ProjectDir%myCommands.resx" . > nul
copy /Y "%ProjectDir%myCommands.Designer.vb" . > nul
copy /Y "%ProjectDir%My Project\*.*" ".\My Project" > nul
copy /Y "%ProjectDir%Template Data\__TemplateIcon.ico" . > nul
copy /Y "%ProjectDir%Template Data\MyTemplate.vstemplate" . > nul
"C:\Program Files\WinRAR\WinRAR.exe" a -r "..\%ProjectName%.zip" *.* > nul
copy /Y "%ProjectDir%Template Data\MyTemplate - WDExpress.vstemplate" "MyTemplate.vstemplate" > nul
"C:\Program Files\WinRAR\WinRAR.exe" a -r "..\%ProjectName%-WD.zip" *.* > nul

pause
