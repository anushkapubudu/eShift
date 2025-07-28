using eShift.Utilities;
using System;

namespace eShift.Model
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public int JobId { get; set; }

        public DateTime IssueDate { get; set; } = DateTime.Today;

        public DateTime DueDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxRate { get; set; } = 0.00m;

        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; } = 0.00m;

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public int CustomerId { get; set; }

    }
}
