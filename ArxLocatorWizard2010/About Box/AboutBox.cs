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
