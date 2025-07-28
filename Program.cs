using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace eShift
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //'GlobalFontSettings' does not contain a definition for 'UseWindowsFonts'
            PdfSharp.Fonts.GlobalFontSettings.UseWindowsFontsUnderWindows = true;
            Application.Run(new Forms.FrmCustomerDashboard("anushka3@gmail.com"));
            //Application.Run(new Forms.Admin.FrmAdminDashboard());
        }
    }
}
