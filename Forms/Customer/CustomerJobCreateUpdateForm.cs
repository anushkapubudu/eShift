using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Model;
using eShift.Repository;
using eShift.Repository.Interface;
using eShift.Repository.Services;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eShift.Forms
{
    public partial class CustomerJobCreateUpdateForm : Form
    {
        private readonly string _userId;
        private readonly IJobService _jobService;

        public CustomerJobCreateUpdateForm(string userId)
        {
            InitializeComponent();
            _userId = userId;
            IJobRepository jobRepo = new JobRepository();
            _jobService = new JobService(jobRepo);
        }

        private void btnSaveJobrequest_Click(object sender, EventArgs e)
        {
            // Get values 
            string pickupAddress = txtPickupAddress.Text.Trim();
            string dropOffAddress = txtDropOffAddress.Text.Trim();
            string jobDescription = txtJobDescription.Text.Trim();
            string weightText = txtWeight.Text.Trim();
            DateTime jobDate = dtpJobDate.Value;

            // Basic validation
            if (string.IsNullOrWhiteSpace(pickupAddress) ||
                string.IsNullOrWhiteSpace(dropOffAddress) ||
                string.IsNullOrWhiteSpace(jobDescription) ||
                string.IsNullOrWhiteSpace(weightText))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(weightText, out decimal weightKg))
            {
                MessageBox.Show("Please enter a valid weight value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build job object
            var newJob = new Job
            {
                PickupLocation = pickupAddress,
                DropoffLocation = dropOffAddress,
                JobDescription = jobDescription,
                StartDate = jobDate,
                EndDate = jobDate,
                JobStatus = JobStatus.Pending.ToString(),
                CustomerId = Int32.Parse(_userId),
            };

            try
            {
                _jobService.CreateJob(newJob);

                MessageBox.Show("Job request saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save job request.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
