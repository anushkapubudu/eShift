using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Model
{
    class TransportUnit
    {
        public int TransportUnitId { get; set; }
        public int VehicleId { get; set; }
        public int ContainerId { get; set; }
        public int DriverId { get; set; }
        public int? AssistantId { get; set; }
        public DateTime AssignedStart { get; set; }
        public DateTime? AssignedEnd { get; set; }
    }
}
