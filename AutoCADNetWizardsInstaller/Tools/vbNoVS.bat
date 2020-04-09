rem This batch is to build the VB Wizard ZIP without Visual Studio
@echo off
%~d0
cd /d %~dp0

set ProjectName=AutoCAD VB plug-in
set output = Output
set ProjectDir=..\..\%ProjectName%\
set OutDir = ..\%output%

mkdir %OutDir% 2>nul
mkdir "%OutDir%\%ProjectName%" 2>nul
mkdir "%OutDir%\%ProjectName%\Properties" 2>nul
cd "%OutDir%\%ProjectName%"

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
7z a -r "..\%ProjectName%.zip" *.* > nul
pause
