using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Model;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace eShift.Forms
{
    public partial class FrmCustomerProfile: Form
    {
        private readonly IUserService _userService;
        private readonly string _userEmail;
        private User _existingUser;

        public FrmCustomerProfile(string userEmail)
        {
            InitializeComponent();
            IUserRepository userRepo = new UserRepository();
            _userService = new UserService(userRepo);
            _userEmail = userEmail;
            LoadUserDetails();
        }

        private void LoadUserDetails()
        {
            try
            {
                var user = _userService.GetUserDetails(_userEmail);
                if (user != null)
                {
                    _existingUser = user;
                    AppendUserDetailsToUI();
                    ClearPasswordFileds();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AppendUserDetailsToUI()
        {
            txtFirstName.Text = _existingUser.FirstName;
            txtLastName.Text = _existingUser.LastName;
            txtEmail.Text = _existingUser.Email;
            txtPhoneNo.Text = _existingUser.Telephone;
            txtAddress.Text = _existingUser.Address;
        }

        private void ClearPasswordFileds()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmNewPassword.Clear();
        }

        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim().ToLower();
            string phone = txtPhoneNo.Text.Trim();
            string address = txtAddress.Text.Trim();
            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmNewPassword.Text.Trim();

            // Basic field validation
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("First name, last name, and email are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidationUtil.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Password change logic only if current password field is filled
            string passwordHash = null;

            if (!string.IsNullOrWhiteSpace(currentPassword))
            {
                // All password fields must be filled
                if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
                {
                    MessageBox.Show("Please enter both new password and confirm password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("New password and confirm password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                // Hash the new password
                passwordHash = PasswordUtil.Instance.HashPassword(newPassword);


                // current password verification 
                if (!PasswordUtil.Instance.VerifyPassword(txtCurrentPassword.Text.Trim(), _existingUser.PasswordHash))
                {
                    MessageBox.Show("Current password is invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Build updated user object
            var updatedUser = new User
            {
                UserId = _existingUser.UserId, 
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Telephone = phone,
                Address = address,
                PasswordHash = passwordHash 
            };

            // Pass data to service layer
            var result = _userService.UpdateCustomerProfile(updatedUser);

            switch (result)
            {
                case CustomerUpdateResult.Success:
                    LoadUserDetails(); // Reload user details to reflect changes
                    MessageBox.Show("Your profile has been updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case CustomerUpdateResult.EmailInUse:
                    MessageBox.Show("This email is already in use by another account.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case CustomerUpdateResult.ValidationError:
                    MessageBox.Show("Some required fields are missing or invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                default:
                    MessageBox.Show("Something went wrong while updating. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
