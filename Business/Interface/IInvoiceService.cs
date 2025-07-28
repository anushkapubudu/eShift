using eShift.Model;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Business.Interface
{
    public interface IInvoiceService
    {
        void CreateInvoice(Invoice invoice);
        bool UpdateInvoice(Invoice invoice);
        void DeleteInvoice(int invoiceId);
        Invoice GetInvoiceById(int invoiceId);
        List<Invoice> GetAllInvoices();
        List<Invoice> GetInvoicesByCustomerId(int customerId);
        void ChangeInvoiceStatus(int invoiceId, InvoiceStatus newStatus);
        void AddPaymentToInvoice(int invoiceId, Payment payment);
        List<Payment> GetPaymentsForInvoice(int invoiceId);
        decimal CalculateRemainingBalance(int invoiceId);
        

    }
}
