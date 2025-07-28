using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Model;
using eShift.Repository.Interface;
using eShift.Repository.Services;
using eShift.Utilities;
using System;
using System.Windows.Forms;

namespace eShift.Forms
{
    public partial class FrmRegister : Form
    {
        private readonly IUserService _userService;

        public FrmRegister()
        {
            InitializeComponent();
            IUserRepository userRepo = new UserRepository();
            _userService = new UserService(userRepo);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!AreRequiredFieldsFilled())
            {
                ShowMessage("Please fill in all required fields.", "Validation Error", MessageBoxIcon.Warning);
                return;
            }else if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                ShowMessage("Your Password and confirm passwords are not matched.", "Validation Error", MessageBoxIcon.Warning);
                return;
            }

            var user = CreateUserFromInput();
            string plainPassword = txtPassword.Text.Trim();

            if (!IsValidUserInput(user)) return;

            try
            {
                var result = _userService.RegisterUser(user, plainPassword);
                HandleRegistrationResult(result,user.Email);
            }
            catch (Exception ex)
            {
                ShowMessage("Something went wrong. Please try again.", "Error", MessageBoxIcon.Error);
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private bool AreRequiredFieldsFilled()
        {
            return !string.IsNullOrWhiteSpace(txtFirstName.Text) &&
                   !string.IsNullOrWhiteSpace(txtLastName.Text) &&
                   !string.IsNullOrWhiteSpace(txtAddress.Text) &&
                   !string.IsNullOrWhiteSpace(txtPhoneNo.Text) &&
                   !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                   !string.IsNullOrWhiteSpace(txtConfirmPassword.Text);
        }

        private User CreateUserFromInput()
        {
            return new User
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Telephone = txtPhoneNo.Text.Trim(),
                Email = txtEmail.Text.Trim().ToLower(),
                Role = "Customer"
            };
        }

        private bool IsValidUserInput(User user)
        {
            if (!ValidationUtil.IsValidEmail(user.Email))
            {
                ShowMessage("Please enter a valid email address.", "Validation Error", MessageBoxIcon.Warning);
                return false;
            }

            if (!ValidationUtil.IsValidPhone(user.Telephone))
            {
                ShowMessage("Please enter a valid phone number.", "Validation Error", MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void HandleRegistrationResult(RegistrationResult result, String UserEmail)
        {
            switch (result)
            {
                case RegistrationResult.Success:
                    ShowMessage("Registration successful!");
                    FrmCustomerDashboard customerDashboard = new FrmCustomerDashboard(UserEmail);
                    this.Hide();
                    customerDashboard.Show();
                    break;
                case RegistrationResult.MissingFields:
                    ShowMessage("Please fill all required fields.");
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    ShowMessage("Email is already registered.");
                    break;
            }
        }

        private void ShowMessage(string message, string title = "Info", MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            FrmLogin loginForm = new FrmLogin();
            this.Hide();
            loginForm.Show();
        }
    }
}
