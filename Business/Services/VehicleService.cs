using eShift.Model;
using eShift.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepo;

        public VehicleService(IVehicleRepository vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (string.IsNullOrWhiteSpace(vehicle.PlateNumber) ||
                string.IsNullOrWhiteSpace(vehicle.VehicleType) ||
                vehicle.CapacityKg <= 0)
                throw new Exception("Missing or invalid vehicle data.");

            _vehicleRepo.AddVehicle(vehicle);
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            var vehicle = _vehicleRepo.GetVehicleById(vehicleId);
            if (vehicle == null)
                throw new Exception($"Vehicle with ID {vehicleId} not found.");

            return vehicle;
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _vehicleRepo.GetAllVehicles();
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            if (string.IsNullOrWhiteSpace(vehicle.PlateNumber) ||
                vehicle.CapacityKg <= 0)
                throw new Exception("Invalid data for update.");

            return _vehicleRepo.UpdateVehicle(vehicle);
        }

        public void DeleteVehicle(int vehicleId)
        {
            _vehicleRepo.DeleteVehicle(vehicleId);
        }
    }

