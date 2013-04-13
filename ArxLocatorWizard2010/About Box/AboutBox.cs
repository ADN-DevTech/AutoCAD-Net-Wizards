using System;
using System.Drawing ;
using System.Collections ;
using System.ComponentModel ;
using System.Windows.Forms ;
using System.Diagnostics ;

#pragma warning disable 1591

namespace ObjectARXLocatorWizard {

	public partial class AboutBox : Form {
		
		public AboutBox () {
			InitializeComponent () ;
			//- Display the proper current version number
			this.label1.Text =Wizard.AddInProductName ;
			this.label4.Text =Wizard.AddInVersion ;
		
		}

		private void linkLabel1_LinkClicked (object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			//- http://www.autodesk.com/developautocad
			System.Diagnostics.Process.Start (linkLabel1.Text) ;
		}

		private void linkLabel2_LinkClicked (object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			//- oarxwiz-feedback@autodesk.com
			System.Diagnostics.Process.Start ("mailto:" + linkLabel2.Text) ;
		}

	}
}
