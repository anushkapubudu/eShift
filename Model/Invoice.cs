using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Model
{
    class Invoice
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
        public string Status { get; set; } = "DRAFT";
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
