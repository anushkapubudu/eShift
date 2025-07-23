using eShift.Business.Interface;
using eShift.Model;
using eShift.Repository.Interface;
using System;
using System.Collections.Generic;

namespace eShift.Business.Services
{
    class TransportUnitService : ITransportUnitService
    {
        private readonly ITransportUnitRepository _transportRepo;

        public TransportUnitService(ITransportUnitRepository transportRepo)
        {
            _transportRepo = transportRepo;
        }

        public void CreateTransportUnit(TransportUnit unit)
        {
            if (unit.JobId <= 0 || unit.VehicleId <= 0 || unit.ContainerId <= 0 || unit.DriverId <= 0 || unit.AssignedStart == default)
                throw new Exception("Missing required transport unit data.");

            _transportRepo.CreateTransportUnit(unit);
        }

        public TransportUnit GetTransportUnitById(int unitId)
        {
            var unit = _transportRepo.GetTransportUnitById(unitId);
            if (unit == null)
                throw new Exception($"TransportUnit ID {unitId} not found.");

            return unit;
        }

        public List<TransportUnit> GetAllTransportUnits()
        {
            return _transportRepo.GetAllTransportUnits();
        }

        public List<TransportUnit> GetTransportUnitsByJobId(int jobId)
        {
            return _transportRepo.GetTransportUnitsByJobId(jobId);
        }

        public bool UpdateTransportUnit(TransportUnit unit)
        {
            if (unit.TransportUnitId <= 0  || unit.JobId <= 0)
                throw new Exception("Invalid transport unit data for update.");

            return _transportRepo.UpdateTransportUnit(unit);
        }

        public void DeleteTransportUnit(int unitId)
        {
            _transportRepo.DeleteTransportUnit(unitId);
        }
    }
}
