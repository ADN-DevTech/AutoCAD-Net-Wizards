@echo off
%~d0
cd /d %~dp0

set ProjectName=AutoCAD CSharp plug-in
set output = Output
set ProjectDir=..\..\%ProjectName%\
set OutDir = ..\%output%

mkdir %OutDir% 2>nul
mkdir "%OutDir%\%ProjectName%" 2>nul
mkdir "%OutDir%\%ProjectName%\Properties" 2>nul
cd "%OutDir%\%ProjectName%"

if exist "..\%ProjectName%.zip" del "..\%ProjectName%.zip"
del *.* /s /q > nul
copy /Y "%ProjectDir%%ProjectName% - 2021.csproj" ".\%ProjectName%.csproj" > nul
copy /Y "%ProjectDir%myPlugin.cs" . > nul
copy /Y "%ProjectDir%myCommands.cs" . > nul
copy /Y "%ProjectDir%myCommands.resx" . > nul
copy /Y "%ProjectDir%myCommands.Designer.cs" . > nul
copy /Y "%ProjectDir%Properties\*.*" ".\Properties" > nul
copy /Y "%ProjectDir%Template Data\__TemplateIcon.ico" . > nul
copy /Y "%ProjectDir%Template Data\MyTemplate.vstemplate" . > nul
7z a -r "..\%ProjectName%.zip" *.* > nul
pause
