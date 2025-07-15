using System;
using System.Windows.Forms;

namespace eShift.Forms
{
    public partial class FrmCustomerDashboard : Form
    {
        public FrmCustomerDashboard()
        {
            InitializeComponent();
        }


        // Helper Methods

        // This LoadForm method is used to load different forms into the panelContent area of the dashboard.
        private void LoadForm(Form form)
        {
            panelNew.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelNew.Controls.Add(form);
            form.Show();
        }

        private void btnSidePanelDashboard_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmCustomerProfile());
        }

        private void btnSidePanelCustomers_Click(object sender, EventArgs e)
        {
            LoadForm(new FrmCustomer());
        }

        private void FrmCustomerDashboard_Load(object sender, EventArgs e)
        {
            LoadForm(new FrmCustomerProfile());
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
    }
}
