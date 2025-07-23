using eShift.Model;
using System.Collections.Generic;

namespace eShift.Repository.Interface
{
    interface ITransportUnitRepository
    {
        void CreateTransportUnit(TransportUnit unit);
        TransportUnit GetTransportUnitById(int unitId);
        List<TransportUnit> GetAllTransportUnits();
        List<TransportUnit> GetTransportUnitsByJobId(int jobId);
        bool UpdateTransportUnit(TransportUnit unit);
        void DeleteTransportUnit(int unitId);
    }
}
