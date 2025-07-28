using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Forms.Admin;
using eShift.Repository.Interface;
using eShift.Repository.Services;
using eShift.Utilities;
using System;
using System.Windows.Forms;

namespace eShift.Forms
{
    public partial class FrmLogin : Form
    {
        private readonly IUserService _userService;
        public FrmLogin()
        {
            InitializeComponent();
            IUserRepository userRepo = new UserRepository();
            _userService = new UserService(userRepo);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FrmRegister registerForm = new FrmRegister();
            registerForm.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim().ToLower();
            string password = txtPassword.Text.Trim();

            // Basic required field check
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both email and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Email format validation
            if (!ValidationUtil.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var result = _userService.Login(email, password);

                if (result == LoginResult.Success)
                {   // Get the logged in user details
                    var user = _userService.GetUserDetails(email);
                    if (user != null)
                    {
                        // redirect to the dashboard based on user role
                        if (user.Role == "Customer")
                        {
                            FrmCustomerDashboard customerDashboard = new FrmCustomerDashboard(user.Email);
                            this.Hide(); 
                            customerDashboard.Show();
                        
                        }else if(user.Role == "ADMIN")
                        {
                            FrmAdminDashboard adminDashboard = new FrmAdminDashboard();
                            this.Hide();
                            adminDashboard.Show();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred. Please try again later.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Login Exception: {ex.Message}");
            }
        }
    }
}
