using eShift.Utilities;
using System;

namespace eShift.Model
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int InvoiceId { get; set; } 

        public DateTime PaymentDate { get; set; } = DateTime.Today;

        public decimal Amount { get; set; }

        public PaymentMethod Method { get; set; } = PaymentMethod.Cash;

        public string ReferenceNo { get; set; }

        public string Remarks { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } 
    }
}
