using eShift.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eShift.Forms.Admin
{
    public partial class FrmAdminDashboard: Form
    {
        public FrmAdminDashboard()
        {
            InitializeComponent();
            LoadForm(new FrmAdminSummery());
        }



        // Helper Methods
        // This LoadForm method is used to load different forms into the panelContent area of the dashboard.
        private void LoadForm(Form form)
        {
            //LoadUserName(_userEmail);
            panelNew.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelNew.Controls.Add(form);
            form.Show();
        }

      

        private void btnSidePanelDahbord_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmAdminSummery());
        }

        private void btnSidePanelManageJobs_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmAdminManageJobs());
        }

        private void btnSidePanelManageStaff_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmAdminManageStaff());
        }

        private void btnSidePanelManageVehicle_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmAdminManageVehicle());
        }

        private void btnSidePanelmanageContainers_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmAdminManageContainer());
        }

        private void btnSidePanelManageCustomer_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmAdminManageCustomers());
        }

        private void btnSidePanelmanageInvoice_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmAdminManageInvoice());
        }

        private void btnSidePanelReport_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmAdminReports());
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                
            this.Hide();
            var loginForm = new FrmLogin(); 
            loginForm.Show();
            }
        }


        // This method loads the user name and user type 
        //private void LoadUserName(string email)
        //{
        //    try
        //    {
        //        var user = _userService.GetUserDetails(email);
        //        if (user != null)
        //        {
        //            _user = user;
        //            lblUserName.Text = $"{user.FirstName} {user.LastName}";
        //            lblUserType.Text = $"{user.Role}";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error loading user details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
    }


}
