namespace eShift.Forms.Admin
{
    partial class FrmAdminReports
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGenerateExcel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGeneratePdf = new System.Windows.Forms.Button();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Menu;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dtTo);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtFrom);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cmbReportType);
            this.panel2.Controls.Add(this.btnGenerateExcel);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.btnGeneratePdf);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(836, 198);
            this.panel2.TabIndex = 31;
            // 
            // btnGenerateExcel
            // 
            this.btnGenerateExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateExcel.BackColor = System.Drawing.Color.OliveDrab;
            this.btnGenerateExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateExcel.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateExcel.ForeColor = System.Drawing.SystemColors.Window;
            this.btnGenerateExcel.Location = new System.Drawing.Point(596, 83);
            this.btnGenerateExcel.Name = "btnGenerateExcel";
            this.btnGenerateExcel.Size = new System.Drawing.Size(217, 35);
            this.btnGenerateExcel.TabIndex = 38;
            this.btnGenerateExcel.Text = "Generate Report (Exel)";
            this.btnGenerateExcel.UseVisualStyleBackColor = false;
            this.btnGenerateExcel.Click += new System.EventHandler(this.btnGenerateExcel_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(14, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 21);
            this.label2.TabIndex = 28;
            this.label2.Text = "GENERATE REPORTS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 15);
            this.label6.TabIndex = 26;
            this.label6.Text = "Select Report Type :";
            // 
            // btnGeneratePdf
            // 
            this.btnGeneratePdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneratePdf.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnGeneratePdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneratePdf.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneratePdf.ForeColor = System.Drawing.SystemColors.Window;
            this.btnGeneratePdf.Location = new System.Drawing.Point(596, 128);
            this.btnGeneratePdf.Name = "btnGeneratePdf";
            this.btnGeneratePdf.Size = new System.Drawing.Size(217, 35);
            this.btnGeneratePdf.TabIndex = 25;
            this.btnGeneratePdf.Text = "Generate Report (Pdf)";
            this.btnGeneratePdf.UseVisualStyleBackColor = false;
            this.btnGeneratePdf.Click += new System.EventHandler(this.btnGeneratePdf_Click);
            // 
            // cmbReportType
            // 
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Location = new System.Drawing.Point(18, 73);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(253, 24);
            this.cmbReportType.TabIndex = 41;
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(21, 133);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(253, 22);
            this.dtFrom.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 15);
            this.label7.TabIndex = 42;
            this.label7.Text = "Date From :";
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(302, 133);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(253, 22);
            this.dtTo.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(302, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 44;
            this.label1.Text = "Date To :";
            // 
            // FrmAdminReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 683);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAdminReports";
            this.Text = "FrmAdminReports";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnGenerateExcel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGeneratePdf;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label label7;
    }
}