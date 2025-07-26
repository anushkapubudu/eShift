using eShift.Business.Interface;
using eShift.Model;
using eShift.Repository.Interface;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Business.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IPaymentRepository _paymentRepo;

        public InvoiceService(IInvoiceRepository invoiceRepo, IPaymentRepository paymentRepo)
        {
            _invoiceRepo = invoiceRepo;
            _paymentRepo = paymentRepo;
        }

        public void CreateInvoice(Invoice invoice)
        {
            invoice.Status = InvoiceStatus.Draft;
            invoice.CreatedAt = DateTime.Now;
            _invoiceRepo.AddInvoice(invoice);
        }

        public bool UpdateInvoice(Invoice invoice)
        {
            var existing = _invoiceRepo.GetInvoiceById(invoice.InvoiceId);
            if (existing == null) return false;

            existing.SubTotal = invoice.SubTotal;
            //existing.Description = invoice.Description;
            existing.DueDate = invoice.DueDate;
            existing.Status = invoice.Status;

            _invoiceRepo.UpdateInvoice(existing);
            return true;
        }

        public void DeleteInvoice(int invoiceId)
        {
            _invoiceRepo.DeleteInvoice(invoiceId);
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return _invoiceRepo.GetInvoiceById(invoiceId);
        }

        public List<Invoice> GetAllInvoices()
        {
            return _invoiceRepo.GetAllInvoices();
        }

        public void ChangeInvoiceStatus(int invoiceId, InvoiceStatus newStatus)
        {
            var invoice = _invoiceRepo.GetInvoiceById(invoiceId);
            if (invoice != null)
            {
                invoice.Status = newStatus;
                _invoiceRepo.UpdateInvoice(invoice);
            }
        }

        public void AddPaymentToInvoice(int invoiceId, Payment payment)
        {
            var invoice = _invoiceRepo.GetInvoiceById(invoiceId);
            if (invoice == null) return;

            payment.InvoiceId = invoiceId;
            payment.PaymentDate = DateTime.Now;
            _paymentRepo.AddPayment(payment);

            var balance = CalculateRemainingBalance(invoiceId);
            if (balance == 0)
                ChangeInvoiceStatus(invoiceId, InvoiceStatus.Paid);
        }

        public List<Payment> GetPaymentsForInvoice(int invoiceId)
        {
            return _paymentRepo.GetPaymentsByInvoice(invoiceId);
        }

        public decimal CalculateRemainingBalance(int invoiceId)
        {
            var invoice = _invoiceRepo.GetInvoiceById(invoiceId);
            var payments = _paymentRepo.GetPaymentsByInvoice(invoiceId);
            var paidAmount = payments.Sum(p => p.Amount);

            return invoice != null ? invoice.SubTotal - paidAmount : 0;
        }
    }

}
