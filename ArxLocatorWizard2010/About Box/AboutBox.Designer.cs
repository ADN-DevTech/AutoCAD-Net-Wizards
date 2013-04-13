namespace ObjectARXLocatorWizard {

	partial class AboutBox {
		private System.Windows.Forms.Label label1 ;
		private System.Windows.Forms.Label label2 ;
		private System.Windows.Forms.Button button1 ;
		private System.Windows.Forms.Label label3 ;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.LinkLabel linkLabel2;

		private System.ComponentModel.Container components =null ;

		protected override void Dispose (bool disposing) {
			if ( disposing ) {
				if ( components != null ) {
					components.Dispose () ;
				}
			}
			base.Dispose (disposing) ;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent () {
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label10 = new System.Windows.Forms.Label();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(185, 197);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Ok";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(48, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(272, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "<title>";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(48, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(384, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Copyright © Autodesk, Inc. - All rights reserved.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(192, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(240, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "by Autodesk Developer Network";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(48, 48);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "<version>";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(192, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 5;
			this.label5.Text = "Cyrille Fauvel";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 143);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(201, 16);
			this.label9.TabIndex = 9;
			this.label9.Text = "Updates freely available at:";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(219, 143);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(221, 16);
			this.linkLabel1.TabIndex = 10;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://www.autodesk.com/developautocad";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 167);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(201, 16);
			this.label10.TabIndex = 11;
			this.label10.Text = "Email wishlist and bug reports to:";
			// 
			// linkLabel2
			// 
			this.linkLabel2.Location = new System.Drawing.Point(264, 167);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(176, 16);
			this.linkLabel2.TabIndex = 12;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "oarxwiz-feedback@autodesk.com";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// AboutBox
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button1;
			this.ClientSize = new System.Drawing.Size(448, 232);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutBox";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About...";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
