# AutoCAD .NET Wizards

- Modern wizards for .NET Core projects

- This tool uses VSIX technology to install a VS Template on Visual Studio.

- All command-line instructions should run on the[ Visual Studio Developer Command Prompt.]([Command-line shells & prompt for developers - Visual Studio (Windows) | Microsoft Learn](https://learn.microsoft.com/en-us/visualstudio/ide/reference/command-prompt-powershell?view=vs-2022))

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
  
<img width="500" height="380" alt="InstallWiz_01" src="https://github.com/user-attachments/assets/bc2acd01-5cdf-45ce-80ef-a246db3a5009" /><br>

<img width="500" height="380" alt="InstallWiz_02" src="https://github.com/user-attachments/assets/1c602569-6429-4355-84c4-3ba8d73f9784" /><br>

<img width="500" height="380" alt="InstallWiz_03" src="https://github.com/user-attachments/assets/6d060971-79b1-49fd-ae39-4be19a8250cd" /><br>

<img width="800" height="500" alt="VSWiz_01" src="https://github.com/user-attachments/assets/c857c83f-961a-4bb8-ace0-10bf86a0016f" /><br>



### Tips

- After creating the project from the template pack, edit `launchSettings.json` to change the executable path to acad.exe.
  
  - **Wizard template fetches** the AutoCAD NuGet package from the Microsoft NuGet Server.
  - **The template project will resolve to the local NuGet package** if it already exists at `%USERPROFILE%\.nuget`.
  - **To add the ObjectARX SDK or Civil SDK from a local file disk, edit the project file (.csproj).**
    - Add `<AssemblySearchPaths>D:\Arx2027\inc\;$(AssemblySearchPaths)</AssemblySearchPaths>`.

- Select the project from solution explorer and right click.
  
  ![EdiProjectFile](https://github.com/ADN-DevTech/AutoCAD-Net-Wizards/assets/6602398/77fc0b17-f914-4d36-8f53-a6b788bab670)

- A typical .NET plugin project to accomodate AutoCAD, ACA, C3D etc.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0-windows</TargetFramework>
    <Platforms>x64</Platforms>
    <OutputType>Library</OutputType>
    <RootNamespace>Example</RootNamespace>
    <GenerateAssemblyInfo>false</GenerateAssemblyInfo>
  </PropertyGroup>
  <PropertyGroup>
     <!--Edit ArxSdk to local ObjectARX Path-->
    <ArxSdk>D:\ArxSDKs\arx2027</ArxSdk>
     <!--Edit AcadDir to AutoCAD 2027 Install path-->
    <AcadDir>D:\ACAD\AutoCAD 2027</AcadDir>
    <ArxMgdPath>$(AcadDir)</ArxMgdPath>
    <OMFMgdPath>$(AcadDir)\ACA\</OMFMgdPath>
    <AeccMgdPath>$(AcadDir)\C3D\</AeccMgdPath>
    <AssemblySearchPaths>$(ArxSdk)\inc\;$(OMFMgdPath);$(AeccMgdPath);$(AssemblySearchPaths)</AssemblySearchPaths>
  </PropertyGroup>
  <PropertyGroup Condition="'$(Configuration)|$(Platform)' == 'Debug|x64'">
    <OutputPath>bin\x64\Debug\</OutputPath>
    <CodeAnalysisRuleSet>MinimumRecommendedRules.ruleset</CodeAnalysisRuleSet>
  </PropertyGroup>
  <PropertyGroup Condition="'$(Configuration)|$(Platform)' == 'Release|x64'">
    <OutputPath>bin\x64\Release\</OutputPath>
    <CodeAnalysisRuleSet>MinimumRecommendedRules.ruleset</CodeAnalysisRuleSet>
  </PropertyGroup>
  <PropertyGroup>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <AppendRuntimeIdentifierToOutputPath>false</AppendRuntimeIdentifierToOutputPath>
  </PropertyGroup>
    <ItemGroup Condition=" '$(UsingMicrosoftNETSdk)' == 'true' or '$(CLRSupport)' == 'NetCore' ">
        <FrameworkReference Include="Microsoft.WindowsDesktop.App" />
    </ItemGroup>
    <ItemDefinitionGroup Condition=" '$(CLRSupport)' == 'NetCore' ">
        <ClCompile>
            <DisableSpecificWarnings>4945;%(DisableSpecificWarnings)</DisableSpecificWarnings>
        </ClCompile>
    </ItemDefinitionGroup>
  <ItemGroup>
    <Reference Include="acdbmgd">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>$(ArxMgdPath)\acdbmgd.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="acmgd">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>$(ArxMgdPath)\acmgd.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="accoremgd">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>$(ArxMgdPath)\accoremgd.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="AecBaseMgd">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>$(OMFMgdPath)\AecBaseMgd.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="AeccDbMgd">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>$(AeccMgdPath)\AeccDbMgd.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <!--Add any other references here -->  

  </ItemGroup>
</Project>
```

- launchSettings for various profiles

```json
{
  "profiles": {
    "Example": {
      "commandName": "Project"
    },
    "Acad": {
      "commandName": "Executable",
      "executablePath": "$(AcadDir)\\acad.exe"
    },
    "C3D-Metric": {
      "commandName": "Executable",
      "executablePath": "$(AcadDir)\\acad.exe",
      "commandLineArgs": " /ld \"$(AcadDir)\\AecBase.dbx\" /p \"<<C3D_Metric>>\" /product C3D /language en-US"
    },
    "C3D-Imperial": {
      "commandName": "Executable",
      "executablePath": "$(AcadDir)\\acad.exe",
      "commandLineArgs": " /ld \"$(AcadDir)\\AecBase.dbx\" /p \"<<C3D_Imperial>>\" /product C3D /language en-US"
    }
  }
}
```

### Uninstall Plugin VSIX

- Through CLI

```bash
vsixinstaller /u:AutoCAD2025_07DA9910-9E94-471B-BD32-565D05D4D857
```

- Through UI
  <img width="1000" height="500" alt="uninstallWiz" src="https://github.com/user-attachments/assets/d7ed1334-eb87-4079-ad64-840c65b38979" />




### Written By

- Madhukar Moogala , Autodesk Platform Services (@galakar)

### Updated By

- Sreeparna Mandal
