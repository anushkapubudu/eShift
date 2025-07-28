namespace eShift.Forms
{
    partial class FrmCustomerJobsMngment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomerJobsMngment));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateNewJob = new System.Windows.Forms.Button();
            this.dgvJobRequest = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobRequest)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(31, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "MY JOB REQUESTS";
            // 
            // btnCreateNewJob
            // 
            this.btnCreateNewJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateNewJob.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateNewJob.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateNewJob.Image")));
            this.btnCreateNewJob.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateNewJob.Location = new System.Drawing.Point(780, 25);
            this.btnCreateNewJob.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateNewJob.Name = "btnCreateNewJob";
            this.btnCreateNewJob.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnCreateNewJob.Size = new System.Drawing.Size(204, 50);
            this.btnCreateNewJob.TabIndex = 3;
            this.btnCreateNewJob.Text = "New Job";
            this.btnCreateNewJob.UseVisualStyleBackColor = true;
            this.btnCreateNewJob.Click += new System.EventHandler(this.btnCreateNewJob_Click);
            // 
            // dgvJobRequest
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvJobRequest.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvJobRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvJobRequest.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvJobRequest.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvJobRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJobRequest.Location = new System.Drawing.Point(36, 101);
            this.dgvJobRequest.Margin = new System.Windows.Forms.Padding(4);
            this.dgvJobRequest.Name = "dgvJobRequest";
            this.dgvJobRequest.Size = new System.Drawing.Size(948, 548);
            this.dgvJobRequest.TabIndex = 4;
            // 
            // FrmCustomerJobsMngment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 750);
            this.Controls.Add(this.dgvJobRequest);
            this.Controls.Add(this.btnCreateNewJob);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmCustomerJobsMngment";
            this.Text = "FrmCustomerJobsMngment";
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobRequest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateNewJob;
        private System.Windows.Forms.DataGridView dgvJobRequest;
    }
}