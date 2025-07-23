using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Model;
using eShift.Repository.Interface;
using eShift.Repository.Services;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace eShift.Forms.Admin
{
    public partial class FrmAdminManageStaff : Form
    {
        private readonly IStaffService _staffService;
        private Staff StaffEditingModel;

        public FrmAdminManageStaff()
        {
            InitializeComponent();
            ClearFormFields();
            IStaffRepository staffRepo = new StaffRepository();
            _staffService = new StaffService(staffRepo);
            BindStaffTypes(); 
            LoadStaff();      
        }

        private void BindStaffTypes()
        {
            var staffTypeList = Enum.GetValues(typeof(StaffType)).Cast<StaffType>().ToList();

            // Bind to user creation dropdown
            cmbUserType.DataSource = new BindingSource(staffTypeList, null);

            // Bind to filter dropdown with "All" first
            var filterList = new List<object> { "All" };
            filterList.AddRange(staffTypeList.Select(type => type.ToString()));
            cmbUserTypeFilter.DataSource = new BindingSource(filterList, null);
        }

        private void LoadStaff()
        {
            var staffList = _staffService.GetAllStaff();
            dtgvStaff.DataSource = staffList;

            // Optional: customize grid display

            dtgvStaff.Columns["DeletedAt"].Visible = false;
            dtgvStaff.Columns["StaffId"].Visible = false;
            dtgvStaff.Columns["StaffId"].Visible = false;
            dtgvStaff.Columns["LicenceNumber"].Visible = false;
            dtgvStaff.Columns["CreatedAt"].Visible = false;
            dtgvStaff.Columns["UpdatedAt"].Visible = false;
            dtgvStaff.Columns["FullName"].Visible = false;

            dtgvStaff.Columns["StaffNumber"].HeaderText = "ID";
            dtgvStaff.Columns["FirstName"].HeaderText = "First Name";
            dtgvStaff.Columns["LastName"].HeaderText = "Last Name";
            dtgvStaff.Columns["Designation"].HeaderText = "Role";
            dtgvStaff.Columns["Email"].HeaderText = "Email";
            dtgvStaff.Columns["ContactNumber"].HeaderText = "Phone";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate form inputs
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtContactNo.Text) ||
                cmbUserType.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate email and phone formats
            if (!ValidationUtil.IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidationUtil.IsValidPhone(txtContactNo.Text.Trim()))
            {
                MessageBox.Show("Please enter a valid phone number. Format: 07XXXXXXXX", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool IsUpdate = StaffEditingModel != null && btnSubmit.Text == "Update";

            var staff = new Staff
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                ContactNumber = txtContactNo.Text.Trim(),
                Designation = cmbUserType.SelectedItem.ToString(),
                LicenceNumber = txtLicenceNum.Text.Trim(),  

            };

            try
            {
                if (IsUpdate)
                {
                    staff.StaffId = StaffEditingModel.StaffId;
                    staff.StaffNumber = StaffEditingModel.StaffNumber;


                    _staffService.UpdateStaff(staff);
                    MessageBox.Show("Staff member updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    _staffService.CreateStaff(staff);
                    MessageBox.Show("Staff member created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                ClearFormFields();
                LoadStaff(); // refresh 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create staff.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearFormFields()
        {
            btnSubmit.Text = "Create";
            btnDelete.Visible = false;
            btnClear.Visible = false;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtLicenceNum.Text = string.Empty;
            cmbUserType.SelectedIndex = -1;
        }

        private void dtgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ClearFormFields();

                btnSubmit.Text = "Update";
                btnDelete.Visible = true;
                btnClear.Visible = true;

                StaffEditingModel = (Staff)dtgvStaff.Rows[e.RowIndex].DataBoundItem;

                txtFirstName.Text = StaffEditingModel.FirstName;
                txtLastName.Text = StaffEditingModel.LastName;
                txtEmail.Text = StaffEditingModel.Email;
                txtContactNo.Text = StaffEditingModel.ContactNumber;
                txtLicenceNum.Text = StaffEditingModel.LicenceNumber;
                cmbUserType.SelectedItem = Enum.Parse(typeof(StaffType), StaffEditingModel.Designation);

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFormFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (StaffEditingModel == null)
            {
                MessageBox.Show("Please select a staff member to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete {StaffEditingModel.FirstName} {StaffEditingModel.LastName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _staffService.DeleteStaff(StaffEditingModel.StaffId);
                    MessageBox.Show("Staff member deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    StaffEditingModel = null;
                    ClearFormFields();
                    LoadStaff(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete staff.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
