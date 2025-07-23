using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Model
{
    class Job
    {
        public int JobId { get; set; }
        public string JobNumber { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public string JobDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobStatus { get; set; } = "PENDING";
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
