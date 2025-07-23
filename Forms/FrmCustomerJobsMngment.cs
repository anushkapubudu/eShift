using eShift.Business.Interface;
using eShift.Repository;
using eShift.Model;
using System;
using System.Windows.Forms;
using eShift.Business.Services;
using eShift.Repository.Interface;
using System.Drawing;

namespace eShift.Forms
{
    public partial class FrmCustomerJobsMngment : Form
    {
        // Save customer ID from login
        private readonly string _userId;

        // Job service to talk to database
        private readonly IJobService _jobService;

        public FrmCustomerJobsMngment(string userId)
        {
            InitializeComponent();
            _userId = userId;

            // Set up job repo and service
            IJobRepository jobRepo = new JobRepository();
            _jobService = new JobService(jobRepo);

            // Load jobs for this customer
            LoadCustomerJobRequests();

            // Hook action click handler
            dgvJobRequest.CellClick += dgvJobRequest_CellClick;
        }

        // Create new job button click
        private void btnCreateNewJob_Click(object sender, EventArgs e)
        {
            using (var form = new CustomerJobCreateUpdateForm(_userId))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Reload job list after new job added
                    LoadCustomerJobRequests();
                }
            }
        }

        // Load jobs created by this customer
        private void LoadCustomerJobRequests()
        {
            try
            {
                int customerId = int.Parse(_userId);
                var jobs = _jobService.GetJobRequestsByCustomerId(customerId);

                // Show jobs in grid
                dgvJobRequest.DataSource = jobs;

                // Hide columns not needed
                dgvJobRequest.Columns["JobId"].Visible = false;
                dgvJobRequest.Columns["CustomerId"].Visible = false;
                dgvJobRequest.Columns["CustomerName"].Visible = false;

                // Rename some headers
                dgvJobRequest.Columns["PickupLocation"].HeaderText = "Pickup";
                dgvJobRequest.Columns["DropoffLocation"].HeaderText = "Drop-off";
                dgvJobRequest.Columns["StartDate"].HeaderText = "Date";
                dgvJobRequest.Columns["JobStatus"].HeaderText = "Status";

                // Add update and delete buttons only once
                if (!dgvJobRequest.Columns.Contains("Update"))
                {
                    var updateButton = new DataGridViewButtonColumn
                    {
                        Name = "Update",
                        HeaderText = "",
                        Text = "🖊️",
                        UseColumnTextForButtonValue = true,
                        Width = 60
                    };

                    var deleteButton = new DataGridViewButtonColumn
                    {
                        Name = "Delete",
                        HeaderText = "",
                        Text = "🗑️",
                        UseColumnTextForButtonValue = true,
                        Width = 60
                    };

                    dgvJobRequest.Columns.Add(updateButton);
                    dgvJobRequest.Columns.Add(deleteButton);

                    // Style Update button
                    dgvJobRequest.Columns["Update"].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    dgvJobRequest.Columns["Update"].DefaultCellStyle.ForeColor = Color.White;
                    dgvJobRequest.Columns["Update"].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    dgvJobRequest.Columns["Update"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Style Delete button
                    dgvJobRequest.Columns["Delete"].DefaultCellStyle.BackColor = Color.IndianRed;
                    dgvJobRequest.Columns["Delete"].DefaultCellStyle.ForeColor = Color.White;
                    dgvJobRequest.Columns["Delete"].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    dgvJobRequest.Columns["Delete"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }


                // Auto adjust column widths
                dgvJobRequest.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Call helper to format the grid
                FormatJobRequestGrid();
            }
            catch (Exception ex)
            {
                // Show error if loading jobs failed
                MessageBox.Show("Failed to load job requests.\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handle button clicks in grid
        private void dgvJobRequest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var column = dgvJobRequest.Columns[e.ColumnIndex];

            // If delete button clicked
            if (column.Name == "Delete")
            {
                var jobId = Convert.ToInt32(dgvJobRequest.Rows[e.RowIndex].Cells["JobId"].Value);

                var confirm = MessageBox.Show("Are you sure you want to delete this job?",
                                              "Confirm Delete",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Delete the job and reload list
                        _jobService.DeleteJob(jobId);
                        LoadCustomerJobRequests();
                    }
                    catch (Exception ex)
                    {
                        // Show error if delete failed
                        MessageBox.Show("Failed to delete job.\n" + ex.Message,
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

           
        }

        // Format and style the job grid
        private void FormatJobRequestGrid()
        {
            if (dgvJobRequest.Columns.Count == 0) return;

            // Hide unused columns
            dgvJobRequest.Columns["JobId"].Visible = false;
            dgvJobRequest.Columns["CustomerId"].Visible = false;
            dgvJobRequest.Columns["JobDescription"].Visible = false;
            dgvJobRequest.Columns["EndDate"].Visible = false;
            dgvJobRequest.Columns["CreatedAt"].Visible = false;
            dgvJobRequest.Columns["UpdatedAt"].Visible = false;
            dgvJobRequest.Columns["DeletedAt"].Visible = false;

            // Rename useful headers
            dgvJobRequest.Columns["JobNumber"].HeaderText = "Job";
            dgvJobRequest.Columns["PickupLocation"].HeaderText = "Pickup Address";
            dgvJobRequest.Columns["DropoffLocation"].HeaderText = "Drop-off Address";
            dgvJobRequest.Columns["StartDate"].HeaderText = "Scheduled Date";
            dgvJobRequest.Columns["JobStatus"].HeaderText = "Status";

            // Grid appearance settings
            dgvJobRequest.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvJobRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Highlight rows based on status
            foreach (DataGridViewRow row in dgvJobRequest.Rows)
            {
                var status = row.Cells["JobStatus"].Value?.ToString();
                if (status == "Pending")
                    row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                else if (status == "Draft")
                    row.DefaultCellStyle.BackColor = Color.LightGray;
            }
        }
    }
}
