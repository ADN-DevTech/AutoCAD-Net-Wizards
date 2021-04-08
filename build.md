### Steps to Build

 -  [Get Wix 3.14](https://wixtoolset.org/releases/v3-14-0-4118/)
 -  [Get Wix Tool Extension for VS2019](https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2019Extension)
 -  You need [7Z](https://www.7-zip.org/)/Zip tool, please download and put it C:\Utils\7z.exe this is only a convenience or else you may need change `post build` event of every visual project to ZIP the files.
	 - for example in AutoCAD VB Plugin project ,  `"C:\Utils\7z.exe" a -r "..\$(ProjectName).zip" *.* `
 -  Set the path in terminal using: set path=C:\Utils
- Open the Visual Solution `"..\AutoCADNetWizardsInstaller\AutoCADNetWizards.sln"` in Visual Studio 2019
- Build Solution
	 
