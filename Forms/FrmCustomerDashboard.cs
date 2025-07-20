using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Model;
using eShift.Repository.Interface;
using eShift.Repository.Services;
using System;
using System.Windows.Forms;

namespace eShift.Forms
{
    public partial class FrmCustomerDashboard : Form
    {
        private readonly IUserService _userService;
        private readonly string _userEmail;
        private User _user;

        public FrmCustomerDashboard(string userEmail)
        {
            InitializeComponent();
            IUserRepository userRepo = new UserRepository();
            _userService = new UserService(userRepo);
            _userEmail = userEmail;
        }


        private void btnSidePanelDashboard_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmCustomerProfile(_userEmail));
        }

        private void btnSidePanelMyJobs_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmCustomerJobsMngment(_user.UserId.ToString()));
        }

        private void btnSidePanelCustomers_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmCustomer());
        }

        private void FrmCustomerDashboard_Load(object sender, EventArgs e)
        {
            LoadForm(new FrmCustomerProfile(_userEmail));
        }

        private void panelNew_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmLogin loginForm = new FrmLogin();
            loginForm.Show();
            this.Close();
        }

        // logout button action
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FrmLogin loginForm = new FrmLogin();
            loginForm.Show();
            this.Close();
        }

        // Helper Methods

        // This LoadForm method is used to load different forms into the panelContent area of the dashboard.
        private void LoadForm(Form form)
        {
            LoadUserName(_userEmail);
            panelNew.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelNew.Controls.Add(form);
            form.Show();
        }

        // This method loads the user name and user type 
        private void LoadUserName(string email)
        {
            try
            {
                var user = _userService.GetUserDetails(email);
                if (user != null)
                {
                    _user = user;
                    lblUserName.Text = $"{user.FirstName} {user.LastName}";
                    lblUserType.Text = $"{user.Role}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
