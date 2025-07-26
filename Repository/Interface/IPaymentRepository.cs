using eShift.Model;
using System.Collections.Generic;

namespace eShift.Repository.Interface
{
    public interface IPaymentRepository
    {
        void AddPayment(Payment payment);
        bool UpdatePayment(Payment payment);
        void DeletePayment(int paymentId);

        Payment GetPaymentById(int paymentId);
        List<Payment> GetPaymentsByInvoice(int invoiceId);
        List<Payment> GetAllPayments();
    }
}
