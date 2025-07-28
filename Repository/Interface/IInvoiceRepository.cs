using eShift.Model;
using eShift.Utilities;
using System.Collections.Generic;

namespace eShift.Repository.Interface
{
    public interface IInvoiceRepository
    {
        void AddInvoice(Invoice invoice);
        bool UpdateInvoice(Invoice invoice);
        void DeleteInvoice(int invoiceId);
        Invoice GetInvoiceById(int invoiceId);
        List<Invoice> GetAllInvoices();
        void UpdateInvoiceStatus(int invoiceId, InvoiceStatus status);
        void UpdatePaidAmount(int invoiceId, decimal paidAmount);
        int GetLastInvoiceId();
        List<Invoice> GetInvoicesByCustomerId(int customerId);

    }
}
