using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Model
{
    public class CustomerReport
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int JobCount { get; set; }
        public int InvoiceCount { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
