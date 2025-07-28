using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Model;
using eShift.Repository;
using eShift.Repository.Interface;
using eShift.Repository.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eShift.Forms.Admin
{
    public partial class FrmAdminManageVehicle : Form
    {
        private readonly IVehicleService _vehicleService;
        private Vehicle editingVehicle = null;

        public FrmAdminManageVehicle()
        {
            InitializeComponent();

            IVehicleRepository vehicleRepo = new VehicleRepository();
            _vehicleService = new VehicleService(vehicleRepo);
            txtSearch.TextChanged += txtSearch_TextChanged;

            LoadVehicles();
        }

        private void LoadVehicles()
        {
            var vehicles = _vehicleService.GetAllVehicles();
            dtgvVehicle.DataSource = vehicles;

            // Optional display settings
            dtgvVehicle.Columns["DeletedAt"].Visible = false;
            dtgvVehicle.Columns["VehicleId"].Visible = false;

            dtgvVehicle.Columns["PlateNumber"].HeaderText = "Plate No.";
            dtgvVehicle.Columns["VehicleType"].HeaderText = "Type";
            dtgvVehicle.Columns["CapacityKg"].HeaderText = "Capacity (kg)";
            dtgvVehicle.Columns["CreatedAt"].Visible = false;
            dtgvVehicle.Columns["DeletedAt"].Visible = false;

            dtgvVehicle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtPlateNumber.Text) ||
                string.IsNullOrWhiteSpace(txtVehicleType.Text) ||
                string.IsNullOrWhiteSpace(txtCapacity.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCapacity.Text.Trim(), out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Please enter a valid capacity (numeric and positive).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (editingVehicle != null)
            {
                // UPDATE logic
                editingVehicle.PlateNumber = txtPlateNumber.Text.Trim();
                editingVehicle.VehicleType = txtVehicleType.Text.Trim();
                editingVehicle.CapacityKg = capacity;

                bool updated = _vehicleService.UpdateVehicle(editingVehicle);
                if (updated)
                    MessageBox.Show("Vehicle updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                editingVehicle = null;
                btnSubmit.Text = "Add";
            }
            else
            {
                // CREATE logic
                var vehicle = new Vehicle
                {
                    PlateNumber = txtPlateNumber.Text.Trim(),
                    VehicleType = txtVehicleType.Text.Trim(),
                    CapacityKg = capacity
                };

                try
                {
                    _vehicleService.AddVehicle(vehicle);
                    MessageBox.Show("Vehicle added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to create vehicle.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ClearFields();
            LoadVehicles();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            var allVehicles = _vehicleService.GetAllVehicles();

            var filtered = allVehicles.Where(v =>
                v.PlateNumber.ToLower().Contains(keyword) ||
                v.VehicleType.ToLower().Contains(keyword) 
            ).ToList();

            dtgvVehicle.DataSource = filtered;
        }


        private void dtgvVehicle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                editingVehicle = (Vehicle)dtgvVehicle.Rows[e.RowIndex].DataBoundItem;

                txtPlateNumber.Text = editingVehicle.PlateNumber;
                txtVehicleType.Text = editingVehicle.VehicleType;
                txtCapacity.Text = ((int)Math.Floor(editingVehicle.CapacityKg)).ToString();

                btnSubmit.Text = "Update";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (editingVehicle == null)
            {
                MessageBox.Show("Please select a vehicle to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Delete vehicle {editingVehicle.PlateNumber}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                _vehicleService.DeleteVehicle(editingVehicle.VehicleId);
                MessageBox.Show("Vehicle deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                editingVehicle = null;
                btnSubmit.Text = "Add";

                ClearFields();
                LoadVehicles();
            }
        }

        private void ClearFields()
        {
            btnSubmit.Text = "Add";
            txtPlateNumber.Text = string.Empty;
            txtVehicleType.Text = string.Empty;
            txtCapacity.Text = string.Empty;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

       
    }
}
