using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Model
{
    class Vehicle
    {

        public string DisplayName => PlateNumber + " " + VehicleType;

        public int VehicleId { get; set; }
        public string PlateNumber { get; set; }
        public string VehicleType { get; set; }
        public decimal CapacityKg { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
