using eShift.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Repository.Interface
{
    interface IVehicleRepository
    {
        void AddVehicle(Vehicle vehicle);
        Vehicle GetVehicleById(int vehicleId);
        List<Vehicle> GetAllVehicles();
        bool UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(int vehicleId);
    }
}
