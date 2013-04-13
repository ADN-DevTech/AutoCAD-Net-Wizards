@echo off
%~d0
cd /d %~dp0

set ProjectName=AutoCAD CSharp plug-in
set ProjectDir=..\..\%ProjectName%\

mkdir Output 2>nul
mkdir "Output\%ProjectName%" 2>nul
mkdir "Output\%ProjectName%\Properties" 2>nul
cd "Output\%ProjectName%"

if exist "..\%ProjectName%.zip" del "..\%ProjectName%.zip"
del *.* /s /q > nul
copy /Y "%ProjectDir%%ProjectName% - 2014.csproj" ".\%ProjectName%.csproj" > nul
copy /Y "%ProjectDir%myPlugin.cs" . > nul
copy /Y "%ProjectDir%myCommands.cs" . > nul
copy /Y "%ProjectDir%myCommands.resx" . > nul
copy /Y "%ProjectDir%myCommands.Designer.cs" . > nul
copy /Y "%ProjectDir%Properties\*.*" ".\Properties" > nul
copy /Y "%ProjectDir%Template Data\__TemplateIcon.ico" . > nul
copy /Y "%ProjectDir%Template Data\MyTemplate.vstemplate" . > nul
"C:\Program Files\WinRAR\WinRAR.exe" a -r "..\%ProjectName%.zip" *.* > nul
copy /Y "%ProjectDir%Template Data\MyTemplate - WDExpress.vstemplate" "MyTemplate.vstemplate" > nul
"C:\Program Files\WinRAR\WinRAR.exe" a -r "..\%ProjectName%-WD.zip" *.* > nul

pause
