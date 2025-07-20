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
        private readonly string _userId;
        private readonly IJobService _jobService;

        public FrmCustomerJobsMngment(string userId)
        {
            InitializeComponent();
            _userId = userId;

            IJobRepository jobRepo = new JobRepository();
            _jobService = new JobService(jobRepo);

            LoadCustomerJobRequests();
        }

        private void btnCreateNewJob_Click(object sender, EventArgs e)
        {
            using (var form = new CustomerJobCreateUpdateForm(_userId))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadCustomerJobRequests();
                }
            }
        }

        private void LoadCustomerJobRequests()
        {
            try
            {
                int customerId = int.Parse(_userId);
                var jobs = _jobService.GetJobRequestsByCustomerId(customerId);

                dgvJobRequest.DataSource = jobs;

                dgvJobRequest.Columns["JobId"].Visible = false;
                dgvJobRequest.Columns["CustomerId"].Visible = false;
                dgvJobRequest.Columns["PickupLocation"].HeaderText = "Pickup";
                dgvJobRequest.Columns["DropoffLocation"].HeaderText = "Drop-off";
                dgvJobRequest.Columns["StartDate"].HeaderText = "Date";
                dgvJobRequest.Columns["JobStatus"].HeaderText = "Status";

                if (!dgvJobRequest.Columns.Contains("Update"))
                {
                    var updateButton = new DataGridViewButtonColumn
                    {
                        Name = "Update",
                        HeaderText = "Update",
                        Text = "🖊️",
                        UseColumnTextForButtonValue = true
                    };

                    var deleteButton = new DataGridViewButtonColumn
                    {
                        Name = "Delete",
                        HeaderText = "Delete",
                        Text = "❌",
                        UseColumnTextForButtonValue = true
                    };

                    
                    dgvJobRequest.Columns.Add(updateButton);
                    dgvJobRequest.Columns.Add(deleteButton);
                }
                dgvJobRequest.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                FormatJobRequestGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load job requests.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Helper method
        private void FormatJobRequestGrid()
        {
            if (dgvJobRequest.Columns.Count == 0) return;

            dgvJobRequest.Columns["JobId"].Visible = false;
            dgvJobRequest.Columns["CustomerId"].Visible = false;
            dgvJobRequest.Columns["JobDescription"].Visible = false;
            dgvJobRequest.Columns["EndDate"].Visible = false;
            dgvJobRequest.Columns["CreatedAt"].Visible = false;
            dgvJobRequest.Columns["UpdatedAt"].Visible = false;
            dgvJobRequest.Columns["DeletedAt"].Visible = false;


            dgvJobRequest.Columns["JobNumber"].HeaderText = "Job #";
            dgvJobRequest.Columns["PickupLocation"].HeaderText = "Pickup Address";
            dgvJobRequest.Columns["DropoffLocation"].HeaderText = "Drop-off Address";
            dgvJobRequest.Columns["StartDate"].HeaderText = "Scheduled Date";
            dgvJobRequest.Columns["JobStatus"].HeaderText = "Status";


            dgvJobRequest.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvJobRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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
