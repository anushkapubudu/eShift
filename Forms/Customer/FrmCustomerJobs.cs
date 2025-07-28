using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using eShift.Model;
using eShift.Business;
using eShift.Utilities;
using eShift.Business.Interface;
using eShift.Business.Services;
using System.CodeDom;
using eShift.Repository.Interface;
using eShift.Repository;

namespace eShift.Forms.Customer
{
    public partial class FrmCustomerJobs : Form
    {
        private readonly int _customerId;
        private readonly IJobService _jobService;
        private List<Job> _jobs;

        public FrmCustomerJobs(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;
            IJobRepository jobRepositpry = new JobRepository();
            _jobService =  new JobService(jobRepositpry);

            LoadJobs();
            SetupFilters();
        }


        private void LoadJobs()
        {
            _jobs = _jobService.GetJobsByCustomerId(_customerId)
                      .Where(j => j.JobStatus != JobStatus.Pending.ToString())
                      .ToList();

            BindGrid(_jobs);
        }

        private void SetupFilters()
        {
            cmbStatus.DataSource = Enum.GetValues(typeof(JobStatus));
            cmbStatus.SelectedItem = null;

            txtSearch.TextChanged += TxtSearch_TextChanged;
            cmbStatus.SelectedIndexChanged += CmbStatus_SelectedIndexChanged;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterJobs();
        }

        private void CmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterJobs();
        }

        private void FilterJobs()
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            JobStatus? selectedStatus = cmbStatus.SelectedItem as JobStatus?;

            var filtered = _jobs.Where(j =>
                (string.IsNullOrEmpty(keyword) ||
                 j.JobNumber.ToLower().Contains(keyword) ||
                 j.PickupLocation.ToLower().Contains(keyword) ||
                 j.DropoffLocation.ToLower().Contains(keyword))
                &&
                (!selectedStatus.HasValue || j.JobStatus == selectedStatus.Value.ToString())
            ).ToList();

            BindGrid(filtered);
        }

        private void BindGrid(List<Job> jobs)
        {
            dgvJobs.DataSource = jobs.Select(j => new
            {
                j.JobNumber,
                j.PickupLocation,
                j.DropoffLocation,
                Status = j.JobStatus.ToString(),
                j.StartDate,
                j.EndDate
            }).ToList();

            dgvJobs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
