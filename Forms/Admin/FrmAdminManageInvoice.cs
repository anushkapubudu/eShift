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
    public partial class FrmAdminManageInvoice : Form
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IJobService _jobService;
        private List<Invoice> allInvoices = new List<Invoice>();
        private Invoice editingInvoice = null;

        public FrmAdminManageInvoice()
        {
            InitializeComponent();

            IInvoiceRepository invoiceRepo = new InvoiceRepository();
            IPaymentRepository paymentRepo = new PaymentRepository();
            _invoiceService = new InvoiceService(invoiceRepo, paymentRepo);

            IJobRepository jobRepo = new JobRepository();
            _jobService = new JobService(jobRepo);

            BindStatusDropdown();
            LoadInvoices();
            BindJobsDropdown();

            dtgvInvoice.CellClick += dtgvInvoice_CellClick;
            txtSearch.TextChanged += txtSearch_TextChanged;
            dtDueDate.Value = DateTime.Today.AddDays(7);
        }

        private void BindStatusDropdown()
        {
            cmbStatus.DataSource = Enum.GetValues(typeof(InvoiceStatus));
        }

        private void BindJobsDropdown()
        {
            var jobs = _jobService.GetAllJobs();
            cmbJobs.DataSource = jobs;
            cmbJobs.DisplayMember = "JobNumber";
            cmbJobs.ValueMember = "JobId";
            cmbJobs.SelectedIndex = -1;
        }


        private void LoadInvoices()
        {
            allInvoices = _invoiceService.GetAllInvoices();
            dtgvInvoice.DataSource = allInvoices;

            dtgvInvoice.Columns["InvoiceId"].Visible = false;
            dtgvInvoice.Columns["CreatedAt"].Visible = false;
            dtgvInvoice.Columns["UpdatedAt"].Visible = false;

            dtgvInvoice.Columns["InvoiceNumber"].HeaderText = "Invoice Id";
            dtgvInvoice.Columns["JobId"].HeaderText = "Job Id";
            dtgvInvoice.Columns["IssueDate"].HeaderText = "Issued";
            dtgvInvoice.Columns["DueDate"].HeaderText = "Due";
            dtgvInvoice.Columns["SubTotal"].HeaderText = "Subtotal";
            dtgvInvoice.Columns["TaxRate"].HeaderText = "Tax Rate";
            dtgvInvoice.Columns["TotalAmount"].HeaderText = "Total";
            dtgvInvoice.Columns["PaidAmount"].HeaderText = "Paid";
            dtgvInvoice.Columns["Status"].HeaderText = "Status";

            dtgvInvoice.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (
       string.IsNullOrWhiteSpace(txtSubTotal.Text) ||
                string.IsNullOrWhiteSpace(txtTaxRate.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSubTotal.Text.Trim(), out decimal subTotal) ||
                !decimal.TryParse(txtTaxRate.Text.Trim(), out decimal taxRate) ||
                subTotal < 0 || taxRate < 0)
            {
                MessageBox.Show("Invalid numeric values.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = subTotal + (subTotal * taxRate / 100);
            var status = (InvoiceStatus)cmbStatus.SelectedItem;

            if (editingInvoice != null)
            {
                // UPDATE
                editingInvoice.JobId = (int)cmbJobs.SelectedValue;
                editingInvoice.DueDate = dtDueDate.Value.Date;
                editingInvoice.SubTotal = subTotal;
                editingInvoice.TaxRate = taxRate;
                editingInvoice.TotalAmount = total;
                editingInvoice.Status = status;
                editingInvoice.UpdatedAt = DateTime.Now;

                bool updated = _invoiceService.UpdateInvoice(editingInvoice);
                MessageBox.Show(updated ? "Updated successfully." : "Update failed.",
                    updated ? "Success" : "Error",
                    MessageBoxButtons.OK, updated ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                editingInvoice = null;
                btnSubmit.Text = "Create";
            }
            else
            {
                // CREATE
                var invoice = new Invoice
                {
                    JobId = (int)cmbJobs.SelectedValue,
                    IssueDate = DateTime.Now,
                    DueDate = dtDueDate.Value.Date,
                    SubTotal = subTotal,
                    TaxRate = taxRate,
                    TotalAmount = total,
                    PaidAmount = 0.00m,
                    Status = status,
                    CreatedAt = DateTime.Now
                };

                try
                {
                    _invoiceService.CreateInvoice(invoice);
                    MessageBox.Show("Invoice added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add invoice.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ClearFields();
            LoadInvoices();
        }

        private void dtgvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                editingInvoice = (Invoice)dtgvInvoice.Rows[e.RowIndex].DataBoundItem;
                cmbJobs.SelectedValue = editingInvoice.JobId;
                txtSubTotal.Text = ((int)Math.Floor(editingInvoice.SubTotal)).ToString();
                txtTaxRate.Text = ((int)Math.Floor(editingInvoice.TaxRate)).ToString();
                dtDueDate.Value = editingInvoice.DueDate;
                cmbStatus.SelectedItem = editingInvoice.Status;

                btnSubmit.Text = "Update";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            var filtered = allInvoices.Where(i =>
                i.InvoiceNumber.ToLower().Contains(keyword) ||
                i.Status.ToString().ToLower().Contains(keyword)
            ).ToList();

            dtgvInvoice.DataSource = filtered;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (editingInvoice == null)
            {
                MessageBox.Show("Select an invoice to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Delete invoice #{editingInvoice.InvoiceNumber}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                _invoiceService.DeleteInvoice(editingInvoice.InvoiceId);
                MessageBox.Show("Invoice deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editingInvoice = null;
                btnSubmit.Text = "Create";
                ClearFields();
                LoadInvoices();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            btnSubmit.Text = "Create";
            txtSubTotal.Text = string.Empty;
            txtTaxRate.Text = string.Empty;
            dtDueDate.Value = DateTime.Today.AddDays(7);
            cmbStatus.SelectedIndex = 0;
            cmbJobs.SelectedIndex = -1;
        }

      
    }
}
