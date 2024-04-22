# AutoCAD .NET Wizards

A modern version of wizards supporting .NET core projects, this uses VSIX technology to install VS Template on Visual Studio.





## Steps To Build

```bash
git clone https://github.com/ADN-DevTech/AutoCAD-Net-Wizards.git
cd AutoCAD-Net-Wizards\AutoCAD-Plugin-Template
devenv AutoCAD-Plugin.sln
msbuild PluginVsix\PluginVsix.csproj /t:build /p:Configuration=Release;Platform=x64
```

- To create template packs for both C# and VB
  
  - Set `ExamplePlugin` as `Start Up Project`.
  
  - From Menubar->Project->Export Template

## Steps To Install
