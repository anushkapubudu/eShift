namespace eShift.Forms
{
    partial class FrmCustomerDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomerDashboard));
            this.sidePanel = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnMyProfile = new System.Windows.Forms.Button();
            this.btnInvoice = new System.Windows.Forms.Button();
            this.btnMyJobs = new System.Windows.Forms.Button();
            this.btnSidePanelMyJobs = new System.Windows.Forms.Button();
            this.btnSidePanelDashboard = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblUserType = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelNew = new System.Windows.Forms.Panel();
            this.sidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // sidePanel
            // 
            this.sidePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sidePanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.sidePanel.Controls.Add(this.pictureBox2);
            this.sidePanel.Controls.Add(this.btnLogOut);
            this.sidePanel.Controls.Add(this.btnMyProfile);
            this.sidePanel.Controls.Add(this.btnInvoice);
            this.sidePanel.Controls.Add(this.btnMyJobs);
            this.sidePanel.Controls.Add(this.btnSidePanelMyJobs);
            this.sidePanel.Controls.Add(this.btnSidePanelDashboard);
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(232, 745);
            this.sidePanel.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(31, 15);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(133, 80);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLogOut.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InfoText;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.Location = new System.Drawing.Point(3, 678);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(227, 50);
            this.btnLogOut.TabIndex = 1;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnMyProfile
            // 
            this.btnMyProfile.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMyProfile.Image = ((System.Drawing.Image)(resources.GetObject("btnMyProfile.Image")));
            this.btnMyProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMyProfile.Location = new System.Drawing.Point(3, 331);
            this.btnMyProfile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMyProfile.Name = "btnMyProfile";
            this.btnMyProfile.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnMyProfile.Size = new System.Drawing.Size(227, 50);
            this.btnMyProfile.TabIndex = 0;
            this.btnMyProfile.Text = "My Profile";
            this.btnMyProfile.UseVisualStyleBackColor = true;
            this.btnMyProfile.Click += new System.EventHandler(this.btnMyProfile_Click);
            // 
            // btnInvoice
            // 
            this.btnInvoice.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoice.Image = ((System.Drawing.Image)(resources.GetObject("btnInvoice.Image")));
            this.btnInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInvoice.Location = new System.Drawing.Point(3, 276);
            this.btnInvoice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnInvoice.Size = new System.Drawing.Size(227, 50);
            this.btnInvoice.TabIndex = 0;
            this.btnInvoice.Text = "My Invoices";
            this.btnInvoice.UseVisualStyleBackColor = true;
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // btnMyJobs
            // 
            this.btnMyJobs.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMyJobs.Image = ((System.Drawing.Image)(resources.GetObject("btnMyJobs.Image")));
            this.btnMyJobs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMyJobs.Location = new System.Drawing.Point(3, 220);
            this.btnMyJobs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMyJobs.Name = "btnMyJobs";
            this.btnMyJobs.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnMyJobs.Size = new System.Drawing.Size(227, 50);
            this.btnMyJobs.TabIndex = 0;
            this.btnMyJobs.Text = "My Jobs";
            this.btnMyJobs.UseVisualStyleBackColor = true;
            this.btnMyJobs.Click += new System.EventHandler(this.btnMyJobs_Click);
            // 
            // btnSidePanelMyJobs
            // 
            this.btnSidePanelMyJobs.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSidePanelMyJobs.Image = ((System.Drawing.Image)(resources.GetObject("btnSidePanelMyJobs.Image")));
            this.btnSidePanelMyJobs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSidePanelMyJobs.Location = new System.Drawing.Point(3, 161);
            this.btnSidePanelMyJobs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSidePanelMyJobs.Name = "btnSidePanelMyJobs";
            this.btnSidePanelMyJobs.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnSidePanelMyJobs.Size = new System.Drawing.Size(227, 50);
            this.btnSidePanelMyJobs.TabIndex = 0;
            this.btnSidePanelMyJobs.Text = "Job Request";
            this.btnSidePanelMyJobs.UseVisualStyleBackColor = true;
            this.btnSidePanelMyJobs.Click += new System.EventHandler(this.btnSidePanelMyJobs_Click);
            // 
            // btnSidePanelDashboard
            // 
            this.btnSidePanelDashboard.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSidePanelDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnSidePanelDashboard.Image")));
            this.btnSidePanelDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSidePanelDashboard.Location = new System.Drawing.Point(3, 101);
            this.btnSidePanelDashboard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSidePanelDashboard.Name = "btnSidePanelDashboard";
            this.btnSidePanelDashboard.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnSidePanelDashboard.Size = new System.Drawing.Size(227, 50);
            this.btnSidePanelDashboard.TabIndex = 0;
            this.btnSidePanelDashboard.Text = "New Request";
            this.btnSidePanelDashboard.UseVisualStyleBackColor = true;
            this.btnSidePanelDashboard.Click += new System.EventHandler(this.btnSidePanelDashboard_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Controls.Add(this.lblUserType);
            this.panel2.Controls.Add(this.lblUserName);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(232, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1005, 64);
            this.panel2.TabIndex = 1;
            // 
            // lblUserType
            // 
            this.lblUserType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserType.AutoSize = true;
            this.lblUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserType.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblUserType.Location = new System.Drawing.Point(820, 31);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(36, 13);
            this.lblUserType.TabIndex = 3;
            this.lblUserType.Text = "Admin";
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblUserName.Location = new System.Drawing.Point(820, 15);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(69, 13);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "User Name";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(779, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panelNew
            // 
            this.panelNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNew.BackColor = System.Drawing.SystemColors.Control;
            this.panelNew.Location = new System.Drawing.Point(232, 64);
            this.panelNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelNew.Name = "panelNew";
            this.panelNew.Size = new System.Drawing.Size(1005, 681);
            this.panelNew.TabIndex = 2;
            this.panelNew.Paint += new System.Windows.Forms.PaintEventHandler(this.panelNew_Paint);
            // 
            // FrmCustomerDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(1237, 742);
            this.Controls.Add(this.panelNew);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.sidePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmCustomerDashboard";
            this.Text = "eShift - Customer Dashboard";
            this.Load += new System.EventHandler(this.FrmCustomerDashboard_Load);
            this.sidePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sidePanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUserType;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnSidePanelDashboard;
        private System.Windows.Forms.Panel panelNew;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnSidePanelMyJobs;
        private System.Windows.Forms.Button btnMyJobs;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnMyProfile;
        private System.Windows.Forms.Button btnInvoice;
    }
}