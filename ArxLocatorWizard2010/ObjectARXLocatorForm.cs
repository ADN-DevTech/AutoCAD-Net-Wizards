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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ObjectARXLocatorWizard
{
	public partial class ObjectARXLocatorForm : Form
	{
		public VerticalProductName CurrentVerticalProductName = VerticalProductName.None;

		private XmlConfigurator.XmlConfig mConfig = null;
		//C:\Users\cyrille\AppData\Roaming\Autodesk\ObjectARX Wizards for AutoCAD 2010\settings.xml
		//C:\ProgramData\Autodesk\AutoCAD .Net Wizards\Settings.xml

		public ObjectARXLocatorForm()
		{
			InitializeComponent();
			//- Read path from last time
            mConfig = new XmlConfigurator.XmlConfig(Wizard.AddInCompany, Wizard.AddInProductName, Wizard.AddInBaseConfigName);
        
            this.sdkpath.Text = mConfig.ValueAt(".//Autodesk/Wizards/ObjectARX/Location");
            //this.sdkpath.Text ="C:\\Users\\cyrille\\__sdk\\ObjectARX\\2010_Gator" ;
            this.acadpath.Text = mConfig.ValueAt(".//Autodesk/Wizards/ObjectARX/AcadLocation");
		   
		}

		private void searchsdk_Click(object sender, EventArgs e)
		{
			if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				string path = this.folderBrowserDialog.SelectedPath;
				this.sdkpath.Text = path;
				//- Save path for next time
                mConfig.SetValueAt(".//Autodesk/Wizards/ObjectARX/Location", path);
                mConfig.Save();
				
				//It is OK without SDK for vertical products, for ACAD it is a must.
				//this.button1.Enabled = (this.sdkpath.Text.Trim().Length != 0
				//   && System.IO.Directory.Exists(this.sdkpath.Text.Trim()));
			}
		}

		private void sdkpath_TextChanged(object sender, EventArgs e)
		{
			bool exists = System.IO.File.Exists(this.sdkpath.Text.Trim() + "\\AcMgd.dll");
			if (!exists)
			{
				MessageBox.Show(
					 "This folder does not contains AcMgd.dll, if you continue,\n"
				   + "Please Specify the location of the folder inside the ObjectARX SDK that contains AcMgd.dll",
				   "Not correct SDK Path",
				   MessageBoxButtons.OK,
				   MessageBoxIcon.Warning);
				this.sdkpath.Focus();
			}

			mConfig.SetValueAt(".//Autodesk/Wizards/ObjectARX/Location", this.sdkpath.Text.Trim());
			mConfig.Save();

			//this.button1.Enabled = (this.sdkpath.Text.Trim().Length != 0
			//    && System.IO.Directory.Exists(this.sdkpath.Text.Trim()));
		}

		private void searchacad_Click(object sender, EventArgs e)
		{
			if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				string path = this.folderBrowserDialog.SelectedPath;
				this.acadpath.Text = path;
				//- Save path for next time
				mConfig.SetValueAt(".//Autodesk/Wizards/ObjectARX/AcadLocation", path);
				mConfig.Save();
				this.button1.Enabled = (this.acadpath.Text.Trim().Length != 0
				   && System.IO.Directory.Exists(this.acadpath.Text.Trim()));
				ProductChanged();
			}
		}

		private void acadpath_TextChanged(object sender, EventArgs e)
		{
			mConfig.SetValueAt(".//Autodesk/Wizards/ObjectARX/AcadLocation", this.acadpath.Text.Trim());
			mConfig.Save();

			if (this.acadpath.Text.Trim().Length != 0
				&& System.IO.Directory.Exists(this.acadpath.Text.Trim()))
			{
				this.button1.Enabled = true;
			}
			else
			{
				this.button1.Enabled = false;
			}
			
		}

		private void aboutbox_Click(object sender, EventArgs e)
		{
			AboutBox dlg = new AboutBox();
			dlg.ShowDialog();
			dlg.Dispose();
		}

		private void interop0_CheckedChanged(object sender, EventArgs e)
		{
			if (!interop0.Checked)
				interop1.Checked = false;
		}
		
		private void interop1_CheckedChanged(object sender, EventArgs e)
		{
			if (interop1.Checked)
				interop0.Checked = true;
		}

		private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
		{

		}

		private void ObjectARXLocatorForm_Load(object sender, EventArgs e)
		{
			BindInstalledVerticalProducts(cbRunAs);

			ProductChanged();
		}

		private void ProductChanged()
		{
			this.CurrentVerticalProductName = GetVerticalProductName();

			DeletePage("tpMap3D");
			DeletePage("tpCivil3D");
			DeletePage("tpACA");
			DeletePage("tpAME");

			if (CurrentVerticalProductName == VerticalProductName.Map3D)
			{
				AddPage("tpMap3D");
				this.tabControlRefs.SelectTab("tpMap3D");
   
			}
			if (CurrentVerticalProductName == VerticalProductName.Civil3D)
			{
				AddPage("tpMap3D");
				AddPage("tpCivil3D");
				this.tabControlRefs.SelectTab("tpCivil3D");
			}
			if (CurrentVerticalProductName == VerticalProductName.ACA)
			{
				AddPage("tpACA");
				this.tabControlRefs.SelectTab("tpACA");
			}
			if (CurrentVerticalProductName == VerticalProductName.AME)
			{
				AddPage("tpAME");
				this.tabControlRefs.SelectTab("tpAME");
			}

		}

		#region Tool methods
		private List<TabPage> lstPages = new List<TabPage>();

		public void DeletePage(string strName)
		{
			foreach (TabPage tabPage in tabControlRefs.TabPages)
			{
				if (tabPage.Name == strName)
				{
					tabControlRefs.TabPages.Remove(tabPage);
					if (!lstPages.Contains(tabPage))
					{
						lstPages.Add(tabPage);
					}
					
					break;
				}
			}
		}

		public void AddPage(string strName)
		{
			foreach (TabPage page in lstPages)
			{
				if (page.Name == strName && !PageExists(strName))
				{
					tabControlRefs.TabPages.Add(page);
					tabControlRefs.TabPages[strName].Select();
				}
			}
		}

		public bool PageExists(string pageName)
		{
			foreach (TabPage tabPage in tabControlRefs.TabPages)
			{
				if (tabPage.Name == pageName)
				{
					return true;
				}
			}

			return false;

		}
		#endregion

		#region Civil 3D Extension
		private void CheckCiviCommonRef()
		{
			aecBaseAppLip.Checked = true;
			aecBaseObjectLib.Checked = true;
			autocadTypeLib.Checked = true;
			dbxCommon.Checked = true;

		}


		private void aeccRoadway_CheckedChanged(object sender, EventArgs e)
		{
			if (aeccRoadway.Checked)
			{
				CheckCiviCommonRef();
			}
		}

		private void aeccLand_CheckedChanged(object sender, EventArgs e)
		{
			if (aeccLand.Checked)
			{
				CheckCiviCommonRef();
			}
		}

		private void aeccPipe_CheckedChanged(object sender, EventArgs e)
		{
			if (aeccPipe.Checked)
			{
				CheckCiviCommonRef();
			}
		}

		private void aeccSurvey_CheckedChanged(object sender, EventArgs e)
		{
			if (aeccSurvey.Checked)
			{
				CheckCiviCommonRef();
			}
		}

		#endregion

		#region Get Vertical Product name 
		//TODO: better way to get vertical product name, as they will 
		//be installed in same folder with ACAD in 2013
		VerticalProductName GetVerticalProductName()
		{
			SetRunAsControlsVisible(true);

 			if (this.cbRunAs.SelectedItem.ToString().Trim().ToUpper() == "MAP3D")
			{
				return VerticalProductName.Map3D;
			}
			else if (this.cbRunAs.SelectedItem.ToString().Trim().ToUpper() == "CIVIL3D")
			{
				return VerticalProductName.Civil3D;
			}
			else if (this.cbRunAs.SelectedItem.ToString().Trim().ToUpper() == "ACA")
			{   
				return VerticalProductName.ACA;
			}
			else if (this.cbRunAs.SelectedItem.ToString().Trim().ToUpper() == "AME")
			{
				return VerticalProductName.AME;
			}
			else
				return VerticalProductName.ACAD;
		}

		private void SetRunAsControlsVisible(bool visible)
		{
			lblRunAs.Enabled = visible;
			lblRunAs.Visible = visible;
			cbRunAs.Visible = visible;
			cbRunAs.Enabled = visible;

		}

		void BindInstalledVerticalProducts(ComboBox cb)
		{
			List<VerticalProductName> prods = GetInstalledVerticalProducts();
			cbRunAs.BeginUpdate();
			foreach (VerticalProductName item in prods)
			{
				cbRunAs.Items.Add(item.ToString());
			}
			cbRunAs.EndUpdate();

			cbRunAs.SelectedIndex = 0;
		}

		private List<VerticalProductName> GetInstalledVerticalProducts()
		{
			List <VerticalProductName> prods = new List<VerticalProductName>();

			//TODO: read registry for installed vertical products
			prods.Add(VerticalProductName.ACAD);
			prods.Add(VerticalProductName.ACA);
			prods.Add(VerticalProductName.AME);
			prods.Add(VerticalProductName.Civil3D);
			prods.Add(VerticalProductName.Map3D);

			return prods;
		}
		#endregion

		#region Select/Clear all
		
		private void btnSelectAll_Click(object sender, EventArgs e)
		{
			TabPage currentTab = tabControlRefs.SelectedTab;
			CheckAllCheckBoxes(currentTab);
		}

		private void btnClearAll_Click(object sender, EventArgs e)
		{
			TabPage currentTab = tabControlRefs.SelectedTab;
			ClearAllCheckBoxes(currentTab);
		}

		private void CheckAllCheckBoxes(Control parent)
		{
			
			foreach (Control ctrl in parent.Controls)
			{
				if (ctrl.Enabled && ctrl.GetType() == typeof(CheckBox) )
				{
					(ctrl as CheckBox).Checked = true;
				}
			}
		}

		private void ClearAllCheckBoxes(Control parent)
		{
			foreach (Control ctrl in parent.Controls)
			{
				if (ctrl.Enabled && ctrl.GetType() == typeof(CheckBox))
				{
					(ctrl as CheckBox).Checked = false;
				}
			}
		}
		#endregion

		private void cbRunAs_SelectedIndexChanged(object sender, EventArgs e)
		{
			ProductChanged();
		}
	 
	}
}
