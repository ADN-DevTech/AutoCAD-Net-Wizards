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
  - Repeat same for `ExamplePluginVB`
 
    ![ExportTemplate](https://github.com/ADN-DevTech/AutoCAD-Net-Wizards/assets/6602398/847a729e-f3b6-4a42-b230-5327381fe3e0)


## Steps To Install
- Download and unpack https://github.com/ADN-DevTech/AutoCAD-Net-Wizards/releases/download/v2025/PluginVsix.zip
- Double Click PluginVsix.vsix

  ![VsixInstall-1](https://github.com/ADN-DevTech/AutoCAD-Net-Wizards/assets/6602398/3c40eeed-ab2e-4e3b-afa4-17a4e7ae1211)

  ![VsixInstall-2](https://github.com/ADN-DevTech/AutoCAD-Net-Wizards/assets/6602398/94618cfd-40a1-4580-9b90-3971ee4702c2)
  
  ![AutoCAD-Wizard](https://github.com/ADN-DevTech/AutoCAD-Net-Wizards/assets/6602398/dc1a3cee-4519-4d05-8d0f-9561c19b166e)  


