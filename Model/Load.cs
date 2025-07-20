using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Model
{
    class Load
    {
        public int LoadId { get; set; }
        public string LoadNumber { get; set; }
        public int JobId { get; set; }
        public int? TransportUnitId { get; set; }
        public string Description { get; set; }
        public decimal WeightKg { get; set; }
        public string LoadStatus { get; set; } = "PENDING";
        public DateTime CreatedAt { get; set; }
    }
}
