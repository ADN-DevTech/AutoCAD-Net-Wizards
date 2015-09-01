// Copyright (C) 2013 Autodesk, Inc., and/or its licensors.
// All rights reserved.
//
// The coded instructions, statements, computer programs, and/or related
// material (collectively the "Data") in these files contain unpublished
// information proprietary to Autodesk, Inc. ("Autodesk") and/or its licensors,
// which is protected by U.S. and Canadian federal copyright law and by
// international treaties.
//
// The Data is provided for use exclusively by You. You have the right to use,
// modify, and incorporate this Data into other products for purposes authorized 
// by the Autodesk software license agreement, without fee.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND. AUTODESK
// DOES NOT MAKE AND HEREBY DISCLAIMS ANY EXPRESS OR IMPLIED WARRANTIES
// INCLUDING, BUT NOT LIMITED TO, THE WARRANTIES OF NON-INFRINGEMENT,
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE, OR ARISING FROM A COURSE 
// OF DEALING, USAGE, OR TRADE PRACTICE. IN NO EVENT WILL AUTODESK AND/OR ITS
// LICENSORS BE LIABLE FOR ANY LOST REVENUES, DATA, OR PROFITS, OR SPECIAL,
// DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES, EVEN IF AUTODESK AND/OR ITS
// LICENSORS HAS BEEN ADVISED OF THE POSSIBILITY OR PROBABILITY OF SUCH DAMAGES.

using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using Microsoft.VisualStudio.TemplateWizard;
using EnvDTE;
using VSLangProj;
using System.Management;
using System.IO;

namespace ObjectARXLocatorWizard
{
    public enum VerticalProductName
    {
        None,
        Map3D,
        Civil3D,
        ACA,
        AME,
        ACAD
    }

    public class Wizard : IWizard
    {
        public static string AddInCompany = "<see Assembly>";
        public static string AddInProductName = "<see Assembly>";
        public static string AddInVersion = "<see Assembly>";
        public const string AddInBaseConfigName = "Settings";
        
        private DTE dte;

        private string sdkpath;
        private string acadpath;

        //AutoCAD references
        private bool acdbmgd;
        private bool acmgd;
        private bool accoremgd;
        private bool adwindows;
        private bool acwindows;
        private bool actcmgd;
        private bool acdx;
        private bool interop0;
        private bool interop1;
        private bool acdbmgdbrep;
        private bool accui;

        //AutoCAD Map 3D references
        private bool mapPlatform;
        private bool mapPlatformCore;
        private bool osgeoMgFoundation;
        private bool osgeoMgGeom;
        private bool osgeoMgPlatformBase;
        private bool osgeoFdoGeom;
        private bool osgeoFdoCommon;
        private bool osgeoFdo;
        private bool acMPloygonMgd;
        private bool mgdMapApi;

        //Civil 3D references
        private bool aecBaseMgd;
        private bool aeccDbMgd;
        private bool civilComCommon;
        private bool comAeccLand;
        private bool comAeccPipe;
        private bool comAeccSurvey;
        private bool comAeccRoadway;

        //ACA and AME References
        private bool aecArchMgd;
        private bool aecArchDACHMgd;
        private bool aecBaseUtilsMgd;
        private bool aecProjectBaseMgd;
        private bool aecPropDataMgd;
        private bool aecRcpMgd;
        private bool aecStructureMgd;
        private bool aecUIBaseMgd;
        private bool aecUtilityMgd;

        //AME References
        private bool aecbBldSrvMgd;
        private bool aecbElecBaseMgd;
        private bool aecbHvacBaseMgd;
        private bool aecbPlumbingBaseMgd;
        private bool aecbPipeBaseMgd;
        private bool aecbPartBaseMgd;

        private VerticalProductName prodName = VerticalProductName.None; //ACAD ???

        #region IWizard Members
        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run. Set the replace parameters
        /// based on the user input.
        /// </summary>
        /// <param name="automationObject">The automation object being used by the template wizard.</param>
        /// <param name="replacementsDictionary">The list of standard parameters to be replaced.</param>
        /// <param name="runKind">A WizardRunKind indicating the type of wizard run.</param>
        /// <param name="customParams">The custom parameters with which to perform parameter replacement in the project.</param>
        void IWizard.RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly(); // GetEntryAssembly()
            AddInCompany =((AssemblyCompanyAttribute)Attribute.GetCustomAttribute (ass, typeof(AssemblyCompanyAttribute), false)).Company ;
            AssemblyTitleAttribute titleAttr = ass.GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), false)[0] as AssemblyTitleAttribute;
            AddInProductName = titleAttr.Title;
            AddInVersion =ass.GetName ().Version.ToString () ;

            //- Display the wizard UI to gather user input. 
            //- If the user cancels the UI, return to the new project dialog.
            ObjectARXLocatorForm optForm = new ObjectARXLocatorForm();
            if (optForm.ShowDialog() != DialogResult.OK)
                throw new WizardBackoutException();

            this.sdkpath = optForm.sdkpath.Text;
            this.acadpath = optForm.acadpath.Text;

            this.prodName = optForm.CurrentVerticalProductName;

            #region Get AutoCAD References UI check status
            //AutoCAD  
            this.acdbmgd = optForm.acdbmgd.Checked;
            this.acmgd = optForm.acmgd.Checked;
            this.accoremgd = optForm.accoremgd.Checked;
            this.adwindows = optForm.adwindows.Checked;
            this.acwindows = optForm.acwindows.Checked;
            this.actcmgd = optForm.actcmgd.Checked;
            this.acdx = optForm.acdx.Checked;
            this.interop0 = optForm.interop0.Checked;
            this.interop1 = optForm.interop1.Checked;
            this.acdbmgdbrep = optForm.acdbmgdbrep.Checked;
            this.accui = optForm.accui.Checked;
            #endregion
            #region Get Map 3D References UI check status
            //Map 3D
            if (prodName == VerticalProductName.Map3D)
            {
                //map 3d
                this.mapPlatform = optForm.mapPlatform.Checked;
                this.mapPlatformCore = optForm.mapPlatformCore.Checked;
                this.osgeoMgFoundation = optForm.OsgeoMgFoundation.Checked;
                this.osgeoMgGeom = optForm.OsgeoMgGeom.Checked;
                this.osgeoMgPlatformBase = optForm.OsgeoMgPlatformBase.Checked;
                this.osgeoFdoCommon = optForm.OsgeoFdoCommon.Checked;
                this.osgeoFdoGeom = optForm.OsgeoFdoGeom.Checked;
                this.osgeoFdo = optForm.OsgeoFdo.Checked;
                this.acMPloygonMgd = optForm.AcMPloygonMgd.Checked;
                this.mgdMapApi = optForm.MgdMapApi.Checked;
            }
            #endregion
            #region Get Civil 3D References UI check status            
            //Civil 3D
            if (prodName == VerticalProductName.Civil3D)
            {
                //map3d
                this.mapPlatform = optForm.mapPlatform.Checked;
                this.mapPlatformCore = optForm.mapPlatformCore.Checked;
                this.osgeoMgFoundation = optForm.OsgeoMgFoundation.Checked;
                this.osgeoMgGeom = optForm.OsgeoMgGeom.Checked;
                this.osgeoMgPlatformBase = optForm.OsgeoMgPlatformBase.Checked;
                this.osgeoFdoCommon = optForm.OsgeoFdoCommon.Checked;
                this.osgeoFdoGeom = optForm.OsgeoFdoGeom.Checked;
                this.osgeoFdo = optForm.OsgeoFdo.Checked;
                this.acMPloygonMgd = optForm.AcMPloygonMgd.Checked;
                this.mgdMapApi = optForm.MgdMapApi.Checked;
                //civil 3d
                this.aecBaseMgd = optForm.aecBaseMgd.Checked;
                this.aeccDbMgd = optForm.aeccDbMgd.Checked;
                this.comAeccLand = optForm.aeccLand.Checked;
                this.comAeccPipe = optForm.aeccPipe.Checked;
                this.comAeccSurvey = optForm.aeccSurvey.Checked;
                this.comAeccRoadway = optForm.aeccRoadway.Checked;
                this.civilComCommon = comAeccLand || comAeccPipe
                                || comAeccSurvey || comAeccRoadway;
            }
            #endregion
            #region Get ACA References UI check status 
            //ACA
            if (prodName == VerticalProductName.ACA)
            {
                this.aecArchMgd = optForm.acaAecArchMgd.Checked;
                this.aecArchDACHMgd = optForm.acaArchDACHMgd.Checked;
                this.aecBaseMgd = optForm.acaAecBaseMgd.Checked;
                this.aecBaseUtilsMgd = optForm.acaAecBaseUtilsMgd.Checked;
                this.aecProjectBaseMgd = optForm.acaAecProjectBaseMgd.Checked;
                this.aecPropDataMgd = optForm.acaAecPropDataMgd.Checked;
                this.aecRcpMgd = optForm.acaAecRcpMgd.Checked;
                this.aecStructureMgd = optForm.acaAecStructureMgd.Checked;
                this.aecUIBaseMgd = optForm.acaAecUIBaseMgd.Checked;
                this.aecUtilityMgd = optForm.acaAecUtilityMgd.Checked;
            }
            #endregion
            #region Get AME References UI check status
            //AME
            if (prodName == VerticalProductName.AME)
            {
                this.aecArchMgd = optForm.ameAecArchMgd.Checked;
                this.aecArchDACHMgd = optForm.ameAecArchDACHMgd.Checked;
                this.aecBaseMgd = optForm.ameAecBaseMgd.Checked;
                this.aecBaseUtilsMgd = optForm.ameAecBaseUtilsMgd.Checked;
                this.aecProjectBaseMgd = optForm.ameAecProjectBaseMgd.Checked;
                this.aecPropDataMgd = optForm.ameAecPropDataMgd.Checked;
                this.aecRcpMgd = optForm.ameAecRcpMgd.Checked;
                this.aecStructureMgd = optForm.ameAecStructureMgd.Checked;
                this.aecUIBaseMgd = optForm.ameAecUIBaseMgd.Checked;
                this.aecUtilityMgd = optForm.ameAecUtilityMgd.Checked;

                this.aecbBldSrvMgd = optForm.ameAecbBldSrvMgd.Checked;
                this.aecbElecBaseMgd = optForm.ameAecbElecBaseMgd.Checked;
                this.aecbHvacBaseMgd = optForm.ameAecbHvacBaseMgd.Checked;
                this.aecbPlumbingBaseMgd = optForm.ameAecbPlumbingBaseMgd.Checked;
                this.aecbPipeBaseMgd = optForm.ameAecbPipeBaseMgd.Checked;
                this.aecbPartBaseMgd = optForm.ameAecbPartBaseMgd.Checked;
            }
            #endregion

            //- This approach works for both Express Editions and full VS
            this.AddReferencesOnExpress(replacementsDictionary);

            // Replace parameters can be added dynamically in IWizard.RunStarted, 
            // as demonstrated in the code below.
            //replacementsDictionary.Add("$AboutBoxCodeProjectItem$", "");

            // CustomParameters in .vstemplate file contains default replace parameters.
            // If the user chooses not to use them, reset these replace parameters to 
            // empty string so that they don't appear in the generated code.
            //replacementsDictionary["$EditMenuItemsCreation$"] = "";
        }

        /// <summary>
        /// Indicates whether the specified project item should be added to the project.
        /// </summary>
        /// <param name="filePath">The path to the project item.</param>
        /// <returns>True if the project item should be added to the project; otherwise, false.</returns>
        bool IWizard.ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        /// <summary>
        /// Runs custom wizard logic before opening an item in the template.
        /// This method is called before opening any item that has the OpenInEditor attribute.
        /// </summary>
        /// <param name="projectItem">The project item that will be opened.</param>
        void IWizard.BeforeOpeningFile(ProjectItem projectItem)
        {
        }
        
        /// <summary>
        /// Runs custom wizard logic when a project item has finished generating.
        /// This method is only called for item templates, not for project templates.
        /// </summary>
        /// <param name="projectItem">The project item that finished generating.</param>
        void IWizard.ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            throw new NotImplementedException("Not implemented for project template wizard.");
        }

        /// <summary>
        /// Runs custom wizard logic when a project has finished generating.
        /// </summary>
        /// <param name="project">The project that finished generating.</param>
        void IWizard.ProjectFinishedGenerating(Project project)
        {
            string refPath = string.Empty;
            string startArgs = string.Empty;

            if (this.sdkpath.Trim().Length > 0 && Directory.Exists(this.sdkpath.Trim()))
            {
                string sdkpathRoot = this.sdkpath.Substring(0, this.sdkpath.LastIndexOf("\\"));
                string cpu = Distinguish64or32System();
                string extInc = (cpu == "64") ? "\\inc-x64" : "\\inc-win32";
                this.dte = project.DTE;
                refPath = this.sdkpath;
                refPath += ";" + sdkpathRoot + extInc;
            }
            else
            {
                //Do not ObjectARX SDK, add references path to AutoCAD installation folder
                refPath += ";" + this.acadpath;
            }


            if (prodName == VerticalProductName.Map3D)
            {
                refPath += ";" + this.acadpath + "\\Map";
                refPath += ";" + this.acadpath + "\\Map\\fdo\\bin";
                startArgs = "/ld " + "\"" + this.acadpath + "\\AecBase.dbx\" /product \"Map\"";
            }
            if (prodName == VerticalProductName.Civil3D)
            {
                refPath += ";" + this.acadpath + "\\Map";
                refPath += ";" + this.acadpath + "\\Map\\fdo\\bin";
                refPath += ";" + this.acadpath + "\\C3D";
                startArgs = "/ld " + "\"" + this.acadpath + "\\AecBase.dbx\" /product \"Map\"";
            }


            if (prodName == VerticalProductName.ACA)
            {
                refPath += ";" + this.acadpath + "\\ACA";
                startArgs = "/ld " + "\"" + this.acadpath + "\\AecBase.dbx\" /product \"ACA\"";
            }

            if (prodName == VerticalProductName.AME)
            {
                refPath += ";" + this.acadpath + "\\ACA";
                refPath += ";" + this.acadpath + "\\MEP";
                startArgs = "/ld " + "\"" + this.acadpath + "\\AecBase.dbx\"  /product \"MEP\"";
            }

            project.Properties.Item("ReferencePath").Value = refPath;
            //- Does not work on Express Editions, See AddReferencesOnExpress
            VSProject vsproject = project.Object as VSProject;
            //this.AddReferencesOnVS(vsproject);

            //- Debugger
            string acadpath = this.acadpath; //this.FindAutoCAD();
            if (this.SetupDebuggerOnVS(vsproject, acadpath, startArgs) == false)
            { 
                this.SetupDebuggerOnExpress(project, acadpath, refPath, startArgs);
            }
        }

        /// <summary>
        /// Runs custom wizard logic when the wizard has completed all tasks. 
        /// This method is called after the project is created.
        /// </summary>
        void IWizard.RunFinished()
        {
        }

        #endregion

        #region ObjectARX
        #region Obsoleted, do not use this
        
        //[Obsolete("Do not use this, it does not work in VS Express. Use AddReferencesOnExpress instead")]
        //void AddReferencesOnVS(VSProject vsproject)
        //{
        //    //- AcMgd, AcDbMgd, and AcCoreMgd are always added

        //    //- This approach code will not work on Express Editions since VSProject
        //    //- is not available on Express Editions
        //    try
        //    {
        //        Reference vsref = null;
        //        //vsref =vsproject.References.Find ("AcMgd.dll") ;
        //        if (this.adwindows)
        //        {
        //            vsref = vsproject.References.Add("AdWindows.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.acwindows)
        //        {
        //            vsref = vsproject.References.Add("AcWindows.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.actcmgd)
        //        {
        //            vsref = vsproject.References.Add("AcTcMgd.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.acdx)
        //        {
        //            vsref = vsproject.References.Add("AcDx.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.interop0)
        //        {
        //            vsref = vsproject.References.Add("Autodesk.AutoCAD.Interop.Common.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.interop1)
        //        {
        //            vsref = vsproject.References.Add("Autodesk.AutoCAD.Interop.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.acdbmgdbrep)
        //        {
        //            vsref = vsproject.References.Add("acdbmgdbrep.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.accui)
        //        {
        //            vsref = vsproject.References.Add("AcCui.dll");
        //            vsref.CopyLocal = false;
        //        }

        //        //Add Map 3D references here
        //        if (this.mgdMapApi)
        //        {
        //            vsref = vsproject.References.Add("ManagedMapApi.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.mapPlatform)
        //        {
        //            vsref = vsproject.References.Add("Autodesk.Map.Platform.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.mapPlatformCore) //May be removed in Jedi finial release...
        //        {
        //            vsref = vsproject.References.Add("Autodesk.Map.Platform.Core.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.osgeoMgPlatformBase)
        //        {
        //            vsref = vsproject.References.Add("OSGeo.MapGuide.PlatformBase.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.osgeoMgGeom)
        //        {
        //            vsref = vsproject.References.Add("OSGeo.MapGuide.Geometry.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.osgeoMgFoundation)
        //        {
        //            vsref = vsproject.References.Add("OSGeo.MapGuide.Foundation.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.acMPloygonMgd)
        //        {
        //            vsref = vsproject.References.Add("AcMPolygonMGD.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        //FDO assemblies 
        //        if (this.osgeoFdo)
        //        {
        //            vsref = vsproject.References.Add("OSGeo.FDO.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.osgeoFdoCommon)
        //        {
        //            vsref = vsproject.References.Add("OSGeo.FDO.Common.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        if (this.osgeoFdoGeom)
        //        {
        //            vsref = vsproject.References.Add("OSGeo.FDO.Geometry.dll");
        //            vsref.CopyLocal = false;
        //        }
        //        //Civil 3D references
        //        if (this.aecBaseMgd)
        //        {
        //            vsref = vsproject.References.Add("AecBaseMgd.dll");
        //            vsref.CopyLocal = false;
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        #endregion
        void AddReferencesOnExpress(Dictionary<string, string> replacementsDictionary)
        {
            //- AcMgd, AcDbMgd and AcCoreMgd are always added

            if (this.adwindows)
                replacementsDictionary.Add("$AdWindows$", "1");
            if (this.acwindows)
                replacementsDictionary.Add("$AcWindows$", "1"); 
            if (this.actcmgd)
                replacementsDictionary.Add("$AcTcMgd$", "1"); 
            if (this.acdx)
                replacementsDictionary.Add("$AcDx$", "1");
            if (this.interop0)
                replacementsDictionary.Add("$AutoCAD_Interop_Common$", "1");
            if (this.interop1)
                replacementsDictionary.Add("$AutoCAD_Interop$", "1");
            if (this.acdbmgdbrep)
                replacementsDictionary.Add("$acdbmgdbrep$", "1");
            if (this.accui)
                replacementsDictionary.Add("$AcCui$", "1");

            // Add Map 3D references here...
            if (this.mgdMapApi)
            {
                replacementsDictionary.Add("$mgdMapApi$", "1");
            }
            if (this.mapPlatform)
            {
                replacementsDictionary.Add("$mapPlatform$", "1");
            }
            if (this.mapPlatformCore) //May be removed in Jedi finial release...
            {
                replacementsDictionary.Add("$mapPlatformCore$", "1");
            }
            if (this.osgeoMgPlatformBase)
            {
                replacementsDictionary.Add("$osgeoMgPlatformBase$", "1");
            }
            if (this.osgeoMgGeom)
            {
                replacementsDictionary.Add("$osgeoMgGeom$", "1");
            }
            if (this.osgeoMgFoundation)
            {
                replacementsDictionary.Add("$osgeoMgFoundation$", "1");
            }
            if (this.acMPloygonMgd)
            {
                replacementsDictionary.Add("$acMPloygonMgd$", "1");
            }
            //FDO assemblies 
            if (this.osgeoFdo)
            {
                replacementsDictionary.Add("$osgeoFdo$", "1");
            }
            if (this.osgeoFdoCommon)
            {
                replacementsDictionary.Add("$osgeoFdoCommon$", "1");
            }
            if (this.osgeoFdoGeom)
            {
                replacementsDictionary.Add("$osgeoFdoGeom$", "1");
            }

            //Common for all AEC products, including Civil 3D, ACA and AME
            if (this.aecBaseMgd)
            {
                replacementsDictionary.Add("$aecBaseMgd$", "1");
            }
            //Civil 3D references
            if (this.aeccDbMgd)
            {
                replacementsDictionary.Add("$aeccDbMgd$", "1");
            }
            if (this.civilComCommon)
            {
                replacementsDictionary.Add("$civilComCommon$", "1");
            }
            if (this.comAeccLand)
            {
                replacementsDictionary.Add("$comAeccLand$", "1");
            }
            if (this.comAeccSurvey)
            {
                replacementsDictionary.Add("$comAeccSurvey$", "1");
            }
            if (this.comAeccRoadway)
            {
                replacementsDictionary.Add("$comAeccRoadway$", "1");
            }
            if (this.comAeccPipe)
            {
                replacementsDictionary.Add("$comAeccPipe$", "1");
            }

            //AEC products(ACA, AME) references
            //ACA
            if (this.aecArchMgd)
            {
                replacementsDictionary.Add("$aecArchMgd$", "1");
            }
            if (this.aecArchDACHMgd)
            {
                replacementsDictionary.Add("$aecArchDACHMgd$", "1");
            }
            if (this.aecBaseUtilsMgd)
            {
                replacementsDictionary.Add("$aecBaseUtilsMgd$", "1");
            }
            if (this.aecProjectBaseMgd)
            {
                replacementsDictionary.Add("$aecProjectBaseMgd$", "1");
            }
            if (this.aecPropDataMgd)
            {
                replacementsDictionary.Add("$aecPropDataMgd$", "1");
            }
            if (this.aecRcpMgd)
            {
                replacementsDictionary.Add("$aecRcpMgd$", "1");
            }
            if (this.aecStructureMgd)
            {
                replacementsDictionary.Add("$aecStructureMgd$", "1");
            }
            if (this.aecUIBaseMgd)
            {
                replacementsDictionary.Add("$aecUIBaseMgd$", "1");
            }
            if (this.aecUtilityMgd)
            {
                replacementsDictionary.Add("$aecUtilityMgd$", "1");
            }
            //AME
            if (this.aecbBldSrvMgd)
            {
                replacementsDictionary.Add("$aecbBldSrvMgd$", "1");
            }
            if (this.aecbElecBaseMgd)
            {
                replacementsDictionary.Add("$aecbElecBaseMgd$", "1");
            }
            if (this.aecbHvacBaseMgd)
            {
                replacementsDictionary.Add("$aecbHvacBaseMgd$", "1");
            }
            if (this.aecbPlumbingBaseMgd)
            {
                replacementsDictionary.Add("$aecbPlumbingBaseMgd$", "1");
            }
            if (this.aecbPipeBaseMgd)
            {
                replacementsDictionary.Add("$aecbPipeBaseMgd$", "1");
            }
            if (this.aecbPartBaseMgd)
            {
                replacementsDictionary.Add("$aecbPartBaseMgd$", "1");
            }
        }

        string FindAutoCAD()
        {
            string acadpath;
            try
            {
                RegistryKey ukey = Registry.CurrentUser;
                RegistryKey acad = ukey.OpenSubKey("SOFTWARE\\Autodesk\\AutoCAD");
                string curver = acad.GetValue("CurVer") as string;
                RegistryKey version = ukey.OpenSubKey("SOFTWARE\\Autodesk\\AutoCAD\\" + curver);
                string key = version.GetValue("CurVer") as string;
                //- We cannot read HKEY_LOCAL_MACHINE on Vista
                //RegistryKey lkey = Registry.LocalMachine;
                //RegistryKey acad2 = lkey.OpenSubKey("SOFTWARE\\Autodesk\\AutoCAD\\" + curver + "\\" + key);
                //string acadpath = acad2.GetValue("AcadLocation") as string;
                RegistryKey acad2 = ukey.OpenSubKey("SOFTWARE\\Autodesk\\AutoCAD\\" + curver + "\\" + key);
                acadpath = acad2.GetValue("NFWFile") as string;
                int pos = acadpath.IndexOf("\\Help");
                acadpath = acadpath.Substring(0, pos);
            }
            catch
            {
                //*If AutoCAD 2017 is not found, we will default to ACAD 2015*//
                acadpath = "C:\\Program Files\\Autodesk\\AutoCAD 2015";
            }
            return (acadpath);
        }

        bool SetupDebuggerOnVS(VSProject vsproject, string acadpath, string startArgs)
        {
            bool ret = false;
            try
            {
                //Configuration config =vsproject.Project.ConfigurationManager.Item("Debug", "Any CPU");
                //ProjectConfigurationProperties pp =config.Properties as ProjectConfigurationProperties ;
                //pp.StartProgram = "acad.exe";
                //pp.StartWorkingDirectory = "";
                vsproject.Project.ConfigurationManager.ActiveConfiguration.Properties.Item("StartProgram").Value = acadpath + "\\acad.exe";
                vsproject.Project.ConfigurationManager.ActiveConfiguration.Properties.Item("StartAction").Value = 1; // "Program";
                vsproject.Project.ConfigurationManager.ActiveConfiguration.Properties.Item("StartArguments").Value = startArgs;
                ret = true;
            }
            catch
            {
                //MessageBox.Show("debug vs error");
            }
            return (ret);
        }

        void SetupDebuggerOnExpress(Project project, string acadpath, string refpath,string startArgs)
        {
            string name = project.FullName + ".user" ;
            string text =
                  "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n"
                + "<PropertyGroup>\n"
                + "<ReferencePath>" + refpath + "</ReferencePath>\n"
                + "</PropertyGroup>\n"
                + "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\n"
                + "<StartProgram>" + acadpath + "\\acad.exe</StartProgram>\n"
                + "<StartAction>Program</StartAction>\n"
                + "<StartArguments>" + startArgs + "</StartArguments>\n"
                + "</PropertyGroup>\n"
                + "</Project>" ;
            System.IO.StreamWriter SW = new System.IO.StreamWriter(name);
            SW.Write (text);
            SW.Flush() ;
            SW.Close(); 
        }

        #endregion

        #region Utils
        public string Distinguish64or32System()
        {
            //              32bit OS	        64bit OS
            // 32bit CPU	AddressWidth = 32	N/A
            // 64bit CPU	AddressWidth = 32	AddressWidth = 64
            try
            {
                string addressWidth = String.Empty;
                ConnectionOptions mConnOption = new ConnectionOptions();
                ManagementScope mMs = new ManagementScope(@"\\localhost", mConnOption);
                ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
                ManagementObjectCollection mObjectCollection = mSearcher.Get();
                foreach (ManagementObject mObject in mObjectCollection)
                {
                    addressWidth = mObject["AddressWidth"].ToString();
                }
                return addressWidth;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return String.Empty;
            }
        }
        #endregion

    }

}
