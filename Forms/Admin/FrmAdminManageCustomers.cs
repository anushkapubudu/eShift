using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Model;
using eShift.Repository;
using eShift.Repository.Interface;
using eShift.Repository.Services;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace eShift.Forms.Admin
{
    public partial class FrmAdminManageCustomers : Form
    {
        private readonly IUserService _userService;
        private List<User> allCustomers = new List<User>();
        private User editingCustomer = null;

        public FrmAdminManageCustomers()
        {
            InitializeComponent();

            IUserRepository customerRepo = new UserRepository();
            _userService = new UserService(customerRepo);

            LoadCustomers();

            dtgvCustomer.CellClick += dtgvCustomer_CellClick;
            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        private void LoadCustomers()
        {
            allCustomers = _userService.GetAllCustomers();
            dtgvCustomer.DataSource = allCustomers;

            dtgvCustomer.Columns["UserId"].Visible = false;
            dtgvCustomer.Columns["PasswordHash"].Visible = false;

            dtgvCustomer.Columns["FirstName"].HeaderText = "First Name";
            dtgvCustomer.Columns["LastName"].HeaderText = "Last Name";
            dtgvCustomer.Columns["Address"].HeaderText = "Address";
            dtgvCustomer.Columns["Email"].HeaderText = "Email";
            dtgvCustomer.Columns["Telephone"].HeaderText = "Phone";

            dtgvCustomer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtContactNo.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidationUtil.IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Invalid email format.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidationUtil.IsValidPhone(txtContactNo.Text.Trim()))
            {
                MessageBox.Show("Phone must match 07XXXXXXXX format.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (editingCustomer != null)
            {
                // UPDATE 
                editingCustomer.FirstName = txtFirstName.Text.Trim();
                editingCustomer.LastName = txtLastName.Text.Trim();
                editingCustomer.Address = txtAddress.Text.Trim();
                editingCustomer.Email = txtEmail.Text.Trim();
                editingCustomer.Telephone = txtContactNo.Text.Trim();

                bool updated = _userService.UpdateCustomer(editingCustomer);
                MessageBox.Show(updated ? "Updated successfully." : "Update failed.",
                    updated ? "Success" : "Error",
                    MessageBoxButtons.OK, updated ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                editingCustomer = null;
                btnSubmit.Text = "Create";
            }
            else
            {
                // CREATE 
                var customer = new User
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telephone = txtContactNo.Text.Trim(),
                    Role = "Customer",
                };

                try
                {
                    _userService.RegisterUser(customer, "1234");
                    MessageBox.Show("Customer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add customer.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ClearFields();
            LoadCustomers();
        }

        private void dtgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                editingCustomer = (User)dtgvCustomer.Rows[e.RowIndex].DataBoundItem;

                txtFirstName.Text = editingCustomer.FirstName;
                txtLastName.Text = editingCustomer.LastName;
                txtAddress.Text = editingCustomer.Address;
                txtEmail.Text = editingCustomer.Email;
                txtContactNo.Text = editingCustomer.Telephone;

                btnSubmit.Text = "Update";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            var filtered = allCustomers.Where(c =>
                c.FirstName.ToLower().Contains(keyword) ||
                c.LastName.ToLower().Contains(keyword) ||
                c.Email.ToLower().Contains(keyword) ||
                c.Address.ToLower().Contains(keyword) ||
                c.Telephone.Contains(keyword)
            ).ToList();

            dtgvCustomer.DataSource = filtered;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (editingCustomer == null)
            {
                MessageBox.Show("Select a customer to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Delete customer {editingCustomer.FirstName} {editingCustomer.LastName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                _userService.DeleteUser(editingCustomer.UserId);
                MessageBox.Show("Customer deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                editingCustomer = null;
                btnSubmit.Text = "Create";
                ClearFields();
                LoadCustomers();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtContactNo.Text = string.Empty;
        }

       
    }
}
