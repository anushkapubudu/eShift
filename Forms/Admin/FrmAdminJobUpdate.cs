using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Repository.Interface;
using eShift.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eShift.Model;
using eShift.Repository.Services;
using eShift.Utilities;

namespace eShift.Forms.Admin
{
    public partial class FrmAdminJobUpdate : Form
    {
        private readonly int _jobId;
        private readonly IJobService _jobService;
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        private readonly IVehicleService _vehicleService;
        private readonly IContainerService _containerService;
        private readonly ITransportUnitService _unitService;
        private TransportUnit selectedUnit;
        private Job _job;



        public FrmAdminJobUpdate(int jobId)
        {
            InitializeComponent();
            _jobId = jobId;

            // Setup services
            IJobRepository jobRepo = new JobRepository();
            _jobService = new JobService(jobRepo);

            IUserRepository userRepo = new UserRepository();
            _userService = new UserService(userRepo);

            IStaffRepository staffRepo = new StaffRepository();
            _staffService = new StaffService(staffRepo);

            IVehicleRepository vehicleRepo = new VehicleRepository();
            _vehicleService = new VehicleService(vehicleRepo);

            IContainerRepository containerRepo = new ContainerRepository();
            _containerService = new ContainerService(containerRepo);

            ITransportUnitRepository unitRepo = new TransportUnitRepository();
            _unitService = new TransportUnitService(unitRepo);

            

            LoadJobDetails();
            LoadDrivers();
            LoadAssistants();
            LoadVehicles();
            LoadContainers();
            LoadTransportUnits();
        }

        private void LoadJobDetails()
        {
            var job = _jobService.GetJobById(_jobId);
            if (job == null)
            {
                MessageBox.Show("Job not found.");
                this.Close();
                return;
            }
            else
            {
                _job = job;
                lblJobPickupAddress.Text = job.PickupLocation;
                lblJobDestinationAddress.Text = job.DropoffLocation;
                lblJobDescription.Text = job.JobDescription;
                lblJobEstimatedDistance.Text = "0";
                lblJobEstimatedWeight.Text = "0";

                // Job Status combobox bind and set current
                cmbStatus.DataSource = Enum.GetValues(typeof(JobStatus));
                cmbStatus.SelectedItem = Enum.Parse(typeof(JobStatus), job.JobStatus);

                LoadCustomer(job.CustomerId);  
            }
        }

        private void LoadCustomer(int customerId)
        {
            var customer = _userService.GetUserById(customerId);
            if (customer == null)
            {
                lblCustomerFullName.Text = "N/A";
                lblCustomerEmail.Text = "N/A";
                lblCustomerContactNo.Text = "N/A";
                return;
            }

            lblCustomerFullName.Text = customer.FirstName + " " + customer.LastName;
            lblCustomerEmail.Text = customer.Email;
            lblCustomerContactNo.Text = customer.Telephone;
        }

        private void LoadDrivers()
        {
            var drivers = _staffService.GetDrivers();
            cmbDriver.DataSource = drivers;
            cmbDriver.DisplayMember = "FullName";
            cmbDriver.ValueMember = "StaffId";
            cmbDriver.SelectedIndex = -1; 
        }

        private void LoadAssistants()
        {
            var assistants = _staffService.GetAssistants();
            cmbAssistance.DataSource = assistants;
            cmbAssistance.DisplayMember = "FullName";
            cmbAssistance.ValueMember = "StaffId";
            cmbAssistance.SelectedIndex = -1; 
        }

        private void LoadVehicles()
        {
            var vehicles = _vehicleService.GetAllVehicles();
            cmbvehicle.DataSource = vehicles;
            cmbvehicle.DisplayMember = "DisplayName";
            cmbvehicle.ValueMember = "VehicleId";
            cmbvehicle.SelectedIndex = -1; 
        }

        private void LoadContainers()
        {
            var containers = _containerService.GetAllContainers();
            cmbContainer.DataSource = containers;
            cmbContainer.DisplayMember = "DisplayName";
            cmbContainer.ValueMember = "ContainerId";
            cmbContainer.SelectedIndex = -1; 
        }

        private void LoadTransportUnits()
        {
            try
            {
              
                var units = _unitService.GetTransportUnitsByJobId(_jobId);
                dtgvTransportData.DataSource = units;


               
                var allUnits = _unitService.GetTransportUnitsByJobId(_jobId);
                var allStaff = _staffService.GetDrivers();                
                var allVehicles = _vehicleService.GetAllVehicles();

                var gridData = allUnits.Select(u => new
                {
                    Unit = u,                  
                    Driver = allStaff.FirstOrDefault(s => s.StaffId == u.DriverId)?.FullName ?? "N/A",
                    Vehicle = allVehicles.FirstOrDefault(v => v.VehicleId == u.VehicleId)?.DisplayName ?? "N/A",
                    StartDate = u.AssignedStart.ToString("yyyy-MM-dd"),
                    EndDate = u.AssignedEnd?.ToString("yyyy-MM-dd") ?? "Not set"
                }).ToList();



                dtgvTransportData.DataSource = gridData;

                dtgvTransportData.Columns["Unit"].Visible = false;
                dtgvTransportData.Columns["Driver"].HeaderText = "Driver";
                dtgvTransportData.Columns["Vehicle"].HeaderText = "Vehicle";
                dtgvTransportData.Columns["StartDate"].HeaderText = "Start Date";
                dtgvTransportData.Columns["EndDate"].HeaderText = "End Date";

                dtgvTransportData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtgvTransportData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load transport units.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTranfortSubmit_Click(object sender, EventArgs e)
        {
            // Validate required selections
            if (cmbDriver.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a driver.");
                return;
            }

            if (cmbvehicle.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a vehicle.");
                return;
            }

            if (cmbContainer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a container.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTranspportDescription.Text))
            {
                MessageBox.Show("Please provide a transport description.");
                return;
            }

            if (!int.TryParse(txtNoOfItems.Text.Trim(), out int itemCount) || itemCount <= 0)
            {
                MessageBox.Show("Please enter a valid item count.");
                return;
            }

            int? weight = null;
            if (!string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                if (!int.TryParse(txtWeight.Text.Trim(), out int parsedWeight) || parsedWeight < 0)
                {
                    MessageBox.Show("Please enter a valid weight.");
                    return;
                }
                weight = parsedWeight;
            }

            // Create the TransportUnit object
            var unit = new TransportUnit
            {
                VehicleId = Convert.ToInt32(cmbvehicle.SelectedValue),
                JobId = _jobId,
                ContainerId = Convert.ToInt32(cmbContainer.SelectedValue),
                DriverId = Convert.ToInt32(cmbDriver.SelectedValue),
                AssistantId = cmbAssistance.SelectedIndex == -1 ? null : (int?)cmbAssistance.SelectedValue,
                AssignedStart = dtpStartDate.Value,
                AssignedEnd = dtpEnddate.Value,
                ItemDescription = txtTranspportDescription.Text.Trim(),
                ItemCount = itemCount,
                Weight = weight,
                IsFragile = chbxIsFragile.Checked
            };

            bool IsUpdate = btnTranfortSubmit.Text == "Update" && selectedUnit != null;

            try
            {

                if (!IsUpdate)
                {
                    _unitService.CreateTransportUnit(unit);
                    MessageBox.Show("Transport unit assigned successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    

                }
                else {
                    unit.TransportUnitId = selectedUnit.TransportUnitId;
                    _unitService.UpdateTransportUnit(unit);
                    MessageBox.Show("Transport unit updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Clear fields and Refresh list
                ClearTransportUnitFields();
                LoadTransportUnits(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to assign transport unit.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTransportUnitDetailsClear_Click(object sender, EventArgs e)
        {
            // write this method to clean all fileds related to transport unit
            ClearTransportUnitFields();
        }

        private void ClearTransportUnitFields()
        {
            selectedUnit = null;
            btnTranfortSubmit.Text = "Assign to Transport Unit";
            cmbDriver.SelectedIndex = -1;
            cmbAssistance.SelectedIndex = -1;
            cmbvehicle.SelectedIndex = -1;
            cmbContainer.SelectedIndex = -1;

            txtTranspportDescription.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtNoOfItems.Text = string.Empty;

            chbxIsFragile.Checked = false;

            dtpStartDate.Value = DateTime.Now;
            dtpEnddate.Value = DateTime.Now;
        }

        private void ondtgvCellMoueseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedUnit = (TransportUnit)dtgvTransportData.Rows[e.RowIndex].Cells["Unit"].Value;

                cmbDriver.SelectedValue = selectedUnit.DriverId;
                cmbAssistance.SelectedValue = selectedUnit.AssistantId ?? -1;
                cmbvehicle.SelectedValue = selectedUnit.VehicleId;
                cmbContainer.SelectedValue = selectedUnit.ContainerId;

                txtTranspportDescription.Text = selectedUnit.ItemDescription;
                txtNoOfItems.Text = selectedUnit.ItemCount.ToString();
                txtWeight.Text = selectedUnit.Weight?.ToString() ?? string.Empty;
                chbxIsFragile.Checked = selectedUnit.IsFragile;
                dtpStartDate.Value = selectedUnit.AssignedStart;
                dtpEnddate.Value = selectedUnit.AssignedEnd ?? DateTime.Now;

                btnTranfortSubmit.Text = "Update"; 
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a job status.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedStatus = cmbStatus.SelectedItem.ToString();

            // Only update if status has changed
            if (_job.JobStatus != selectedStatus)
            {
                _job.JobStatus = selectedStatus;

                try
                {
                    _jobService.UpdateJobDetails(_job);
                    MessageBox.Show("Job status updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update job.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Job status is already set to selected value.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
