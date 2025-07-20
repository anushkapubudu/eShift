using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Model
{
    class Container
    {
        public int ContainerId { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerType { get; set; }
        public decimal CapacityKg { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
