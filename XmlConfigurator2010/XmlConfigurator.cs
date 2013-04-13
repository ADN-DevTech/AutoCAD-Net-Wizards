using System ;
using System.Xml ;
using Microsoft.Win32 ;
using System.ComponentModel ;

namespace XmlConfigurator {

	#region public class XmlConfigElt
	public class XmlConfigElt {
		private string mxPath =null ;
		private string mValue =null ;

		public XmlConfigElt (string xPath, string eltValue) {
			mxPath =xPath ;
			mValue =eltValue ;
		}

		public string xPath {
			get {
				return (mxPath) ;
			}
		}

		public string Value {
			get {
				return (mValue) ;
			}
		}


		#region Implicit Operators
		static public implicit operator bool (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (false) ;
			return (bool.Parse (configElt.mValue)) ;
		}

		static public implicit operator char (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return ('\0') ;
			return (char.Parse (configElt.mValue)) ;
		}

		static public implicit operator decimal (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (0) ;
			return (decimal.Parse (configElt.mValue)) ;
		}

		static public implicit operator double (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (0.0) ;
			return (double.Parse (configElt.mValue)) ;
		}

		static public implicit operator float (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (0f) ;
			return (float.Parse (configElt.mValue)) ;
		}

		static public implicit operator int (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (0) ;
			return (int.Parse (configElt.mValue)) ;
		}

		static public implicit operator uint (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (0) ;
			return (uint.Parse (configElt.mValue)) ;
		}

		static public implicit operator long (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (0) ;
			return (long.Parse (configElt.mValue)) ;
		}
		static public implicit operator ulong (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (0) ;
			return (ulong.Parse (configElt.mValue)) ;
		}

		static public implicit operator short (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (0) ;
			return (short.Parse (configElt.mValue)) ;
		}

		static public implicit operator ushort (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (0) ;
			return (ushort.Parse (configElt.mValue)) ;
		}

		static public implicit operator string (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return ("") ;
			return (configElt.mValue) ;
		}

		static public implicit operator System.DateTime (XmlConfigElt configElt) {
			if ( configElt.mValue == null || configElt.mValue.Length == 0 )
				return (System.DateTime.Today) ;
			System.IFormatProvider format =new System.Globalization.CultureInfo ("en-US", true) ;
			System.DateTime last =System.DateTime.Parse (configElt.mValue, format, System.Globalization.DateTimeStyles.NoCurrentDateDefault) ;
			return (last) ;
		}
		#endregion
	}
	#endregion

	#region public class XmlConfigFile
	public class XmlConfigFile {
		private System.Xml.XmlDocument mXmlDoc =null ;
		private int mReferenceCount =1 ;

		public XmlConfigFile (string fileName) {
			Reload (fileName) ;
		}

		#region Reference Counting
		public int RefCount {
			get {
				return (mReferenceCount) ;
			}
		}
		public void AddRef () {
			lock (this) {
				mReferenceCount++ ;
			}
		}

		public void Release () {
			lock (this) {
				mReferenceCount-- ;
				//if ( mReferenceCount == 0 ) {
				//}
			}
		}
		#endregion

		public void Reload (string fileName) {
			mXmlDoc =null ;
			mXmlDoc =new System.Xml.XmlDocument () ;
			try {
				mXmlDoc.Load (fileName) ;
			} catch {
				//- Invalid XML file / or does not exist
				CreateXmlConfig (fileName) ;
			}
		}

		public void CreateXmlConfig (string fileName) {
			//System.Xml.XmlComment cmt =mXmlDoc.CreateComment ("XML Configurator generated file") ;
			//mXmlDoc.AppendChild (cmt) ;
			//System.Xml.XmlElement elt =mXmlDoc.CreateElement ("EmptyNode") ;
			//mXmlDoc.AppendChild (elt) ;
			XmlTextWriter cfgTextWriter ;
			try {
				string pathName =fileName.Substring (0, fileName.LastIndexOf ("\\")) ;
				if ( pathName.Length > 0 && !System.IO.Directory.Exists (pathName) )
					System.IO.Directory.CreateDirectory (pathName) ;

				cfgTextWriter =new XmlTextWriter (fileName, null) ;
				cfgTextWriter.Formatting =Formatting.Indented ;
				cfgTextWriter.WriteStartDocument (false) ;
				//- Document type = must contain no spaces
				//cfgTextWriter.WriteDocType ("AutodeskConfig", null, null, null) ;
				cfgTextWriter.WriteComment ("XML Configurator generated file") ;

				cfgTextWriter.WriteStartElement ("Autodesk") ;
				cfgTextWriter.WriteEndElement () ;

				//- Write the XML to file and close the writer
				cfgTextWriter.Flush () ;
				cfgTextWriter.Close () ;
				cfgTextWriter =null ;

				mXmlDoc.Load (fileName) ;
			} catch {
			}
		}

		#region Implicit Operators
		static public implicit operator System.Xml.XmlDocument (XmlConfigFile configFile) {
			if ( configFile.mXmlDoc == null )
				return (null) ;
			return (configFile.mXmlDoc) ;
		}
		#endregion
	}
	#endregion

	#region public class XmlConfig
	public class XmlConfig : System.ComponentModel.Component {

		#region Enums
		public enum XmlConfigSync {
			Connect =0,
			Discard,
			ForceSave
		} ;
		#endregion

		#region Component Designer generated code
		private System.ComponentModel.Container mComponents =null ;
		public readonly int mInstanceID ;
		private static int mNextInstanceID =0 ;
		private static long mClassInstanceCount =0 ;

		private void InitializeComponent () {
			mComponents =new System.ComponentModel.Container () ;
		}
		#endregion

		#region Data Members
		private static System.Collections.Hashtable mXmlDocs =new System.Collections.Hashtable () ;
		private static System.Collections.ArrayList mbXmlModifieds =new System.Collections.ArrayList () ;

		private string mCompany =null ;
		private string mAppName =null ;
		private string mBaseName =null ;

		#endregion

		#region Constructors / Destructor
		public XmlConfig (string company, string appName, string baseName) {
			InitializeComponent () ;
			lock (typeof (XmlConfig)) {
				mInstanceID =mNextInstanceID++ ;
				mClassInstanceCount++ ;
			}
			mCompany =company ;
			mAppName =appName ;
			mBaseName =baseName ;
			if ( Load () == false )
				throw new ArgumentException ("No configuration file available") ;
		}

		~XmlConfig () {
			lock (typeof (XmlConfig)) {
				mClassInstanceCount-- ;
			}
			Unload () ;
		}
		#endregion

		#region Manipulating XML File
		//- Default search should be:
		//-  HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders
		//-   AppData / C:\Documents and Settings\cyrille\Application Data\Autodesk\Tools\settings.xml
		//-  HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders
		//-   Common AppData / C:\Documents and Settings\All\Application Data\Autodesk\Tools\settings.xml

		private string BuildName (bool bAllUsers, bool bMustExits) {
			if ( bAllUsers == false ) {
				Microsoft.Win32.RegistryKey keycu =Microsoft.Win32.Registry.CurrentUser.OpenSubKey (
					"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Folders"
				) ;
				string fileName =string.Format ("{0}\\{1}\\{2}\\{3}.xml", keycu.GetValue ("AppData"), mCompany, mAppName, mBaseName) ;
				if ( System.IO.File.Exists (fileName) == true || bMustExits == false )
					return (fileName) ;
			}

			Microsoft.Win32.RegistryKey keylm =Microsoft.Win32.Registry.LocalMachine.OpenSubKey (
				"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Folders"
			) ;
			string fileNameAll =string.Format ("{0}\\{1}\\{2}\\{3}.xml", keylm.GetValue ("Common AppData"), mCompany, mAppName, mBaseName) ;
			if ( System.IO.File.Exists (fileNameAll) == true || bMustExits == false )
				return (fileNameAll) ;

			return (null) ;
		}

		private bool Load () {
			string fileName =BuildName (false, true) ;
			lock (typeof (XmlConfig)) {
				if ( fileName == null ) {
					fileName =BuildName (true, false) ;
					XmlConfigFile xmlCfgFile =new XmlConfigFile (fileName) ;
					mXmlDocs.Add (fileName, xmlCfgFile) ;
					mbXmlModifieds.Add ((System.Xml.XmlDocument)xmlCfgFile) ;
					Save (fileName) ;
					return (true) ;
				}
				if ( mXmlDocs.Contains (fileName) == false ) {
					mXmlDocs.Add (fileName, new XmlConfigFile (fileName)) ;
				} else {
					XmlConfigFile cfgFile =(XmlConfigFile)mXmlDocs [fileName] ;
					cfgFile.AddRef () ;
				}
			}
			return (true) ;
		}

		private void Unload () {
			string fileName =BuildName (false, true) ;
			if ( fileName == null )
				return ;
			lock (typeof (XmlConfig)) {
				if ( mXmlDocs.Contains (fileName) == true ) {
					XmlConfigFile cfgFile =(XmlConfigFile)mXmlDocs [fileName] ;
					cfgFile.Release () ;
					if ( cfgFile.RefCount == 0 ) {
						mXmlDocs.Remove (fileName) ;
						mbXmlModifieds.Remove ((System.Xml.XmlDocument)cfgFile) ;
					}
				}
			}
		}

		public bool Reload (XmlConfigSync mode) {
			string fileName =BuildName (false, true) ;
			if ( fileName == null )
				return (false) ;
			//- First save changes if asked for
			lock (typeof (XmlConfig)) {
				XmlConfigFile cfgFile =(XmlConfigFile)mXmlDocs [fileName] ;
				if (   XmlDoc != null
					&& (   (mbXmlModifieds.Contains (XmlDoc) == true && mode != XmlConfigSync.Discard)
						|| mode != XmlConfigSync.ForceSave
					)
				) {
					XmlDoc.Save (fileName) ;
					mbXmlModifieds.Remove (XmlDoc) ;
					return (true) ;
				}
				//- Reload Xml document
				mbXmlModifieds.Remove (XmlDoc) ;
				//- Reload the document
				cfgFile.Reload (fileName) ;
				return (true) ;
			}
		}

		public void Save () {
			string fileName =BuildName (false, true) ;
			if ( fileName == null )
				return ;
			Save (fileName) ;
		}

		private void Save (string fileName) {
			lock (typeof (XmlConfig)) {
				if ( mbXmlModifieds.Contains (XmlDoc) != true )
					return ;
				string pathName =fileName.Substring (0, fileName.LastIndexOf ("\\")) ;
				if ( pathName.Length > 0 && !System.IO.Directory.Exists (pathName) )
					System.IO.Directory.CreateDirectory (pathName) ;
				XmlDoc.Save (fileName) ;
				mbXmlModifieds.Remove (XmlDoc) ;
			}
		}

		public System.Xml.XmlDocument XmlDoc {
			get {
				System.Xml.XmlDocument xmlDoc =(XmlConfigFile)mXmlDocs [BuildName (false, true)] ;
				return (xmlDoc) ;
			}
		}
		#endregion

		#region Element Access
		public virtual XmlConfigElt ValueAt (string xPath) {
			if ( XmlDoc == null )
				return (new XmlConfigElt (xPath, null)) ;
			lock (typeof (XmlConfig)) {
				System.Xml.XmlNode node =XmlDoc.SelectSingleNode (xPath) ;
				if ( node == null )
					return (new XmlConfigElt (xPath, null)) ;
				System.Xml.XmlNodeReader nodeReader =new System.Xml.XmlNodeReader (node) ;
				nodeReader.Read () ;
				return (new XmlConfigElt (xPath, nodeReader.ReadElementString ())) ;
			}
		}

		public virtual XmlConfigElt ValueAt (string xPath, object defVal) {
			XmlConfigElt elt =ValueAt (xPath) ;
			if ( elt.Value != null )
				return (elt) ;
			return (new XmlConfigElt (xPath, defVal.ToString ())) ;
		}

		public virtual void SetValueAt (string xPath, object val) {
			if ( XmlDoc == null )
				return ;
			lock (typeof (XmlConfig)) {
				System.Xml.XmlNode node =XmlDoc.SelectSingleNode (xPath) ;
				if ( node == null ) {
					//- Create the node
					string [] pathes =xPath.Substring (3).Split ('/') ;
					string path ="./" ;
					System.Xml.XmlNode parentNode =null ;
					foreach ( string st in pathes ) {
						path +="/" + st ;
						if ( (node =XmlDoc.SelectSingleNode (path)) == null ) {
							if ( parentNode == null )
								parentNode =XmlDoc.DocumentElement ;
							System.Xml.XmlElement elt =XmlDoc.CreateElement (st) ;
							parentNode =parentNode.AppendChild (elt) ;
						} else {
							parentNode =node ;
						}
					}
					node =XmlDoc.SelectSingleNode (xPath) ;
				}
				if ( node.InnerText != val.ToString () ) {
					node.InnerText =val.ToString () ;
					if ( mbXmlModifieds.Contains (XmlDoc) == false )
						mbXmlModifieds.Add (XmlDoc) ;
				}
			}
		}
		public virtual bool Exists (string xPath) {
			if ( XmlDoc == null )
				return (false) ;
			lock (typeof (XmlConfig)) {
				System.Xml.XmlNode node =XmlDoc.SelectSingleNode (xPath) ;
				if ( node == null )
					return (false) ;
				return (true) ;
			}
		}
		#endregion

	}
	#endregion

}
