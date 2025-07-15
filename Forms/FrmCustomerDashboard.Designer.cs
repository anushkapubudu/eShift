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
            this.sidePanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSidePanelDashboard = new System.Windows.Forms.Button();
            this.btnSidePanelCustomers = new System.Windows.Forms.Button();
            this.panelNew = new System.Windows.Forms.Panel();
            this.sidePanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // sidePanel
            // 
            this.sidePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sidePanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.sidePanel.Controls.Add(this.btnSidePanelCustomers);
            this.sidePanel.Controls.Add(this.btnSidePanelDashboard);
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(232, 745);
            this.sidePanel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(232, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1006, 64);
            this.panel2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button1.Location = new System.Drawing.Point(885, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "LOG OUT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label2.Location = new System.Drawing.Point(726, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Admin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(726, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Anushka Pubudu";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pictureBox1.Location = new System.Drawing.Point(684, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 36);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btnSidePanelDashboard
            // 
            this.btnSidePanelDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSidePanelDashboard.Location = new System.Drawing.Point(3, 105);
            this.btnSidePanelDashboard.Name = "btnSidePanelDashboard";
            this.btnSidePanelDashboard.Size = new System.Drawing.Size(226, 51);
            this.btnSidePanelDashboard.TabIndex = 0;
            this.btnSidePanelDashboard.Text = "Dashboard";
            this.btnSidePanelDashboard.UseVisualStyleBackColor = true;
            this.btnSidePanelDashboard.Click += new System.EventHandler(this.btnSidePanelDashboard_Click);
            // 
            // btnSidePanelCustomers
            // 
            this.btnSidePanelCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSidePanelCustomers.Location = new System.Drawing.Point(3, 162);
            this.btnSidePanelCustomers.Name = "btnSidePanelCustomers";
            this.btnSidePanelCustomers.Size = new System.Drawing.Size(226, 51);
            this.btnSidePanelCustomers.TabIndex = 1;
            this.btnSidePanelCustomers.Text = "Customers";
            this.btnSidePanelCustomers.UseVisualStyleBackColor = true;
            this.btnSidePanelCustomers.Click += new System.EventHandler(this.btnSidePanelCustomers_Click);
            // 
            // panelNew
            // 
            this.panelNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNew.BackColor = System.Drawing.SystemColors.Control;
            this.panelNew.Location = new System.Drawing.Point(232, 64);
            this.panelNew.Name = "panelNew";
            this.panelNew.Size = new System.Drawing.Size(1006, 681);
            this.panelNew.TabIndex = 2;
            this.panelNew.Paint += new System.Windows.Forms.PaintEventHandler(this.panelNew_Paint);
            // 
            // FrmCustomerDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(1238, 742);
            this.Controls.Add(this.panelNew);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.sidePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmCustomerDashboard";
            this.Text = "FrmCustomerDashboard";
            this.Load += new System.EventHandler(this.FrmCustomerDashboard_Load);
            this.sidePanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sidePanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSidePanelCustomers;
        private System.Windows.Forms.Button btnSidePanelDashboard;
        private System.Windows.Forms.Panel panelNew;
    }
}