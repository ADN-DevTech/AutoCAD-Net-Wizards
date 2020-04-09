using Microsoft.Win32;
using ObjectARXLocatorWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArxLocatorTestCase
{
    class Program
    {
        static void Main(string[] args)
        {

            //ObjectARXLocatorForm form = new ObjectARXLocatorForm();
            //form.Show();

            RegistryKey ukey = Registry.CurrentUser;
            RegistryKey acad = ukey.OpenSubKey("SOFTWARE\\Autodesk\\AutoCAD");
            string curver = acad.GetValue("CurVer") as string;
            RegistryKey version = ukey.OpenSubKey("SOFTWARE\\Autodesk\\AutoCAD\\" + curver);
            string key = version.GetValue("CurVer") as string;
            RegistryKey acad2 = ukey.OpenSubKey("SOFTWARE\\Autodesk\\AutoCAD\\" + curver + "\\" + key);
            string acadpath = acad2.GetValue("SupportFolder") as string;
            int pos = acadpath.IndexOf("\\Support");
            acadpath = acadpath.Substring(0, pos);
        }
    }
}
