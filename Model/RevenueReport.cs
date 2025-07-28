using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Model
{
    public class RevenueReport
    {
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string Status { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetRevenue { get; set; }
    }

}
