using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using eShift.Model;
using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Repository.Interface;
using eShift.Repository;
using eShift.Repository.Services;
using eShift.Utilities;

namespace eShift.Forms.Customer
{
    public partial class FrmCustomerInvoice : Form
    {
        private readonly int _customerId;
        private readonly IInvoiceService _invoiceService;
        private List<Invoice> _invoices;

        public FrmCustomerInvoice(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;

            IInvoiceRepository invoiceRepo = new InvoiceRepository();
            IPaymentRepository paymentRepo = new PaymentRepository();
            _invoiceService = new InvoiceService(invoiceRepo, paymentRepo);

            LoadInvoices();
            SetupFilters();
        }

        private void LoadInvoices()
        {
            _invoices = _invoiceService.GetInvoicesByCustomerId(_customerId)
                         .Where(i => i.Status != InvoiceStatus.Draft)
                         .ToList();

            BindGrid(_invoices);
        }

        private void SetupFilters()
        {
            cmbStatus.DataSource = Enum.GetValues(typeof(InvoiceStatus));
            cmbStatus.SelectedItem = null;

            txtSearch.TextChanged += TxtSearch_TextChanged;
            cmbStatus.SelectedIndexChanged += CmbStatus_SelectedIndexChanged;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterInvoices();
        }

        private void CmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterInvoices();
        }

        private void FilterInvoices()
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            InvoiceStatus? selectedStatus = cmbStatus.SelectedItem as InvoiceStatus?;

            var filtered = _invoices.Where(i =>
                (string.IsNullOrEmpty(keyword) ||
                 i.InvoiceNumber.ToLower().Contains(keyword))
                &&
                (!selectedStatus.HasValue || i.Status == selectedStatus.Value)
            ).ToList();

            BindGrid(filtered);
        }

        private void BindGrid(List<Invoice> invoices)
        {
            dgvInvoices.DataSource = invoices.Select(i => new
            {
                i.InvoiceNumber,
                i.JobId,
                i.SubTotal,
                Status = i.Status,
                i.PaidAmount,
                i.DueDate
            }).ToList();

            dgvInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
