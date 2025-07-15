using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eShift.Forms
{
    public partial class FrmRegister: Form
    {
        public FrmRegister()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FrmLogin loginForm = new FrmLogin();
            this.Hide();
            loginForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
