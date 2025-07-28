using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Model
{
    public class JobReport
    {
        public string JobNumber { get; set; }
        public int CustomerId { get; set; }
        public string Pickup { get; set; }
        public string Dropoff { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DriverName { get; set; }        
        public string AssistantName { get; set; }     
    }

}
