Copyright (c) Autodesk, Inc. All rights reserved 

AutoCAD .Net Wizards for Visual Studio
by Cyrille Fauvel - Autodesk Developer Network (ADN)


Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


AutoCAD .NET Wizards
=======================

The installation file is in \AutoCADNetWizardsInstaller\

AutoCADNetWizards.msi or

AutoCAD_2014_dotnet_wizards.zip


Installing a managed DLL into the GAC on a developer machine without installer

    "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\gacutil" -I "ArxLocatorWizard2010\bin\Release\ObjectARXLocatorWizard.dll"

	
Test an installer with log file
--------------------------------------

Standard Install

    msiexec /i AutoCADNetWizards.msi /liwearucmopvx!* Tests\test.txt
	
	
Admin Install

    msiexec /a AutoCADNetWizards.msi /qf /liwearucmopvx!* Tests\test.txt
	
	
Uninstall

    msiexec /x AutoCADNetWizards.msi
	
