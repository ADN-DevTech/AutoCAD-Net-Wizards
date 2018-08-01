### Steps to Build

 -  [Get Wix 3.11](http://wixtoolset.org/releases/v3.11.1/stable)
 -  [Get Wix Tool Extension for VS2017](https://marketplace.visualstudio.com/items?itemName=RobMensching.WixToolsetVisualStudio2017Extension)
 -  You need [7Z](https://www.7-zip.org/)/Zip tool, please download and put it C:\Utils\7z.exe this is only a convenience or else you may need change `post build` event of every visual project to ZIP the files.
	 - for example in AutoCAD VB Plugin project ,  `"C:\Utils\7z.exe" a -r "..\$(ProjectName).zip" *.* `
- Open the Visual Solution `"..\AutoCADNetWizardsInstaller\AutoCADNetWizards.msi.sln"` in Visual Studio 2017 
- Build Solution
	 
