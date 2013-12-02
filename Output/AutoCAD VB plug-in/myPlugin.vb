' (C) Copyright $year$ by $registeredorganization$

'
Imports System
Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.EditorInput

' This line is not mandatory, but improves loading performances
<Assembly: ExtensionApplication(GetType($safeprojectname$.MyPlugin))> 

Namespace $safeprojectname$

    ' This class is instantiated by AutoCAD once and kept alive for the 
    ' duration of the session. If you don't do any one time initialization 
    ' then you should remove this class.
    Public Class MyPlugin
        Implements IExtensionApplication

        Public Sub Initialize() Implements IExtensionApplication.Initialize
            ' Add one time initialization here
            ' One common scenario is to setup a callback function here that 
            ' unmanaged code can call. 
            ' To do this:
            ' 1. Export a function from unmanaged code that takes a function
            '    pointer and stores the passed in value in a global variable.
            ' 2. Call this exported function in this function passing delegate.
            ' 3. When unmanaged code needs the services of this managed module
            '    you simply call acrxLoadApp() and by the time acrxLoadApp 
            '    returns  global function pointer is initialized to point to
            '    the C# delegate.
            ' For more info see: 
            ' http:'msdn2.microsoft.com/en-US/library/5zwkzwf4(VS.80).aspx
            ' http:'msdn2.microsoft.com/en-us/library/44ey4b32(VS.80).aspx
            ' http:'msdn2.microsoft.com/en-US/library/7esfatk4.aspx
            ' as well as some of the existing AutoCAD managed apps.

            ' Initialize your plug-in application here
        End Sub

        Public Sub Terminate() Implements IExtensionApplication.Terminate
            ' Do plug-in application clean up here
        End Sub
    End Class

End Namespace