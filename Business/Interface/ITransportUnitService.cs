using eShift.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Business.Interface
{
    interface ITransportUnitService
    {
        void CreateTransportUnit(TransportUnit unit);
        TransportUnit GetTransportUnitById(int unitId);
        List<TransportUnit> GetAllTransportUnits();               
        List<TransportUnit> GetTransportUnitsByJobId(int jobId);  
        bool UpdateTransportUnit(TransportUnit unit);
        void DeleteTransportUnit(int unitId);
    }
}
