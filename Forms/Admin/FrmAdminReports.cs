using eShift.Business.Interface;
using eShift.Business.Service;
using eShift.Utilities;
using eShift.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace eShift.Forms.Admin
{
    public partial class FrmAdminReports : Form
    {
        private readonly IReportService _reportService;

        public FrmAdminReports()
        {
            InitializeComponent();
            cmbReportType.DataSource = Enum.GetValues(typeof(ReportType));
            _reportService = new ReportService();
        }

        private void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            var reportType = (ReportType)cmbReportType.SelectedItem;
            var fromDate = dtFrom.Value.Date;
            var toDate = dtTo.Value.Date;

            var reportData = GetReportData(reportType, fromDate, toDate);

            var saveDialog = new SaveFileDialog
            {
                FileName = $"{reportType}_Report_{DateTime.Now:yyyyMMdd}.pdf",
                Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK && reportData != null)
            {
                switch (reportType)
                {
                    case ReportType.Revenue:
                        ReportUtility.ExportToPdf((List<RevenueReport>)reportData, saveDialog.FileName, reportType.ToString());
                        break;
                    case ReportType.Customers:
                        ReportUtility.ExportToPdf((List<CustomerReport>)reportData, saveDialog.FileName, reportType.ToString());
                        break;
                    case ReportType.Jobs:
                        ReportUtility.ExportToPdf((List<JobReport>)reportData, saveDialog.FileName, reportType.ToString());
                        break;
                }

                MessageBox.Show("PDF report generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void btnGenerateExcel_Click(object sender, EventArgs e)
        {
            var reportType = (ReportType)cmbReportType.SelectedItem;
            var fromDate = dtFrom.Value.Date;
            var toDate = dtTo.Value.Date;

            var reportData = GetReportData(reportType, fromDate, toDate);

            var saveDialog = new SaveFileDialog
            {
                FileName = $"{reportType}_Report_{DateTime.Now:yyyyMMdd}.xlsx",
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK && reportData != null)
            {
                switch (reportType)
                {
                    case ReportType.Revenue:
                        ReportUtility.ExportToExcel((List<RevenueReport>)reportData, saveDialog.FileName);
                        break;
                    case ReportType.Customers:
                        ReportUtility.ExportToExcel((List<CustomerReport>)reportData, saveDialog.FileName);
                        break;
                    case ReportType.Jobs:
                        ReportUtility.ExportToExcel((List<JobReport>)reportData, saveDialog.FileName);
                        break;
                }

                MessageBox.Show("Excel report generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private object GetReportData(ReportType reportType, DateTime fromDate, DateTime toDate)
        {
            switch (reportType)
            {
                case ReportType.Revenue:
                    return _reportService.GetRevenueReport(fromDate, toDate);
                case ReportType.Customers:
                    return _reportService.GetCustomerReport(fromDate, toDate);
                case ReportType.Jobs:
                    return _reportService.GetJobReport(fromDate, toDate);
                default:
                    MessageBox.Show("Unknown report type selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
            }
        }
    }
}
