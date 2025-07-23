using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Repository.Interface;
using eShift.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using eShift.Utilities;
using eShift.Repository.Services;
using eShift.Model;

namespace eShift.Forms.Admin
{
    public partial class FrmAdminManageJobs : Form
    {
        // Service layer references for job and user logic
        private readonly IJobService _jobService;
        private readonly IUserService _userService;

        public FrmAdminManageJobs()
        {
            InitializeComponent();

            // Initialize repositories and services
            IJobRepository jobRepo = new JobRepository();
            IUserRepository userRepo = new UserRepository();
            _jobService = new JobService(jobRepo);
            _userService = new UserService(userRepo);

            // Load dropdown values and job grid
            LoadFilterValues();
            ApplyJobFilters();

            // Bind filter events
            txtSearchJob.TextChanged += (s, e) => ApplyJobFilters();
            cmbStatusFilter.SelectedIndexChanged += (s, e) => ApplyJobFilters();
            cmbCustomerFilter.SelectedIndexChanged += (s, e) => ApplyJobFilters();
            dgvJobs.CellClick += dgvJobs_CellClick;
        }

        // Load values for filter dropdowns
        private void LoadFilterValues()
        {
            // Status dropdown with "All" option
            var statuses = Enum.GetNames(typeof(JobStatus)).ToList();
            statuses.Insert(0, "All");
            cmbStatusFilter.DataSource = statuses;
            cmbStatusFilter.SelectedIndex = 0;

            // Customer dropdown with "All Customers" option
            var customers = _userService.GetAllCustomerSelection();
            customers.Insert(0, new CustomerSelection { UserId = 0, FullName = "All Customers" });
            cmbCustomerFilter.DataSource = customers;
            cmbCustomerFilter.DisplayMember = "FullName";
            cmbCustomerFilter.ValueMember = "UserId";
            cmbCustomerFilter.SelectedIndex = 0;
        }

        // Apply keyword + dropdown filters
        private void ApplyJobFilters()
        {
            List<Job> allJobs;

            try
            {
                allJobs = _jobService.GetAllJobs(); 
            }
            catch (Exception ex)
            {
                // Show alert if fetch fails
                MessageBox.Show("Error loading job data. Please try again later.",
                                "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Clear any old data in grid
                dgvJobs.DataSource = null;
                dgvJobs.Rows.Clear();
                dgvJobs.Columns.Clear();
                return;
            }

            // filtering data 
            string keyword = txtSearchJob.Text.Trim().ToLower();
            string selectedStatus = cmbStatusFilter.SelectedItem?.ToString();
            int selectedCustomerId = Convert.ToInt32(cmbCustomerFilter.SelectedValue);

            var filteredJobs = allJobs.Where(job =>
                (string.IsNullOrEmpty(keyword) ||
                    job.JobNumber.ToLower().Contains(keyword) ||
                    job.PickupLocation.ToLower().Contains(keyword) ||
                    job.DropoffLocation.ToLower().Contains(keyword)) &&
                (selectedStatus == "All" || job.JobStatus == selectedStatus) &&
                (selectedCustomerId == 0 || job.CustomerId == selectedCustomerId)
            ).ToList();

            if (filteredJobs.Count == 0)
            {
                dgvJobs.DataSource = null;
                dgvJobs.Rows.Clear();
                dgvJobs.Columns.Clear();
                dgvJobs.Columns.Add("Message", "Jobs");
                dgvJobs.Rows.Add("No matching jobs found.");
                dgvJobs.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                dgvJobs.DataSource = filteredJobs;
                FormatJobsGrid();
            }
        }


        // Format grid: rename, hide, and style columns
        private void FormatJobsGrid()
        {
            try
            {
                // Hide unused backend fields
                dgvJobs.Columns["JobId"].Visible = false;
                dgvJobs.Columns["CustomerId"].Visible = false;
                dgvJobs.Columns["JobDescription"].Visible = false;
                dgvJobs.Columns["CreatedAt"].Visible = false;
                dgvJobs.Columns["UpdatedAt"].Visible = false;
                dgvJobs.Columns["DeletedAt"].Visible = false;
                

                // Rename visible columns
                dgvJobs.Columns["PickupLocation"].HeaderText = "Pickup Address";
                dgvJobs.Columns["DropoffLocation"].HeaderText = "Destination Address";
                dgvJobs.Columns["StartDate"].HeaderText = "Start Date";
                dgvJobs.Columns["EndDate"].HeaderText = "End Date";
                dgvJobs.Columns["JobStatus"].HeaderText = "Status";
                dgvJobs.Columns["JobNumber"].HeaderText = "Job No";
                dgvJobs.Columns["CustomerName"].HeaderText = "Customer";

                // Add "Update" button if not already added
                if (!dgvJobs.Columns.Contains("Update"))
                {
                    var updateButton = new DataGridViewButtonColumn
                    {
                        Name = "Update",
                        HeaderText = "",
                        Text = "Update",
                        UseColumnTextForButtonValue = true,
                        Width = 70
                    };
                    dgvJobs.Columns.Add(updateButton);

                    // Style update button cell
                    dgvJobs.Columns["Update"].DefaultCellStyle.BackColor = Color.LightGreen;
                    dgvJobs.Columns["Update"].DefaultCellStyle.ForeColor = Color.Black;
                    dgvJobs.Columns["Update"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Add "Delete" button if not already added
                if (!dgvJobs.Columns.Contains("Delete"))
                {
                    var deleteButton = new DataGridViewButtonColumn
                    {
                        Name = "Delete",
                        HeaderText = "",
                        Text = "Delete",
                        UseColumnTextForButtonValue = true,
                        Width = 70
                    };
                    dgvJobs.Columns.Add(deleteButton);

                    // Style delete button cell
                    dgvJobs.Columns["Delete"].DefaultCellStyle.BackColor = Color.Red;
                    dgvJobs.Columns["Delete"].DefaultCellStyle.ForeColor = Color.White;
                    dgvJobs.Columns["Delete"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                // Show error if formatting fails
                MessageBox.Show($"Error formatting jobs grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete Job Row Action
        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var column = dgvJobs.Columns[e.ColumnIndex];

            // Check if Delete button clicked
            if (column.Name == "Delete")
            {
                var jobId = Convert.ToInt32(dgvJobs.Rows[e.RowIndex].Cells["JobId"].Value);

                var confirm = MessageBox.Show("Are you sure you want to delete this job?",
                                              "Confirm Delete", MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        _jobService.DeleteJob(jobId);
                        ApplyJobFilters(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Delete failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
