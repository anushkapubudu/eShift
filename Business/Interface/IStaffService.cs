using eShift.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Business.Interface
{
    interface IStaffService
    {
        void CreateStaff(Staff staff);
        Staff GetStaffById(int staffId);
        List<Staff> GetAllStaff();
        List<Staff> GetDrivers();      
        List<Staff> GetAssistants();   
        bool UpdateStaff(Staff staff);
        void DeleteStaff(int staffId);
        string GenerateStaffNumber();
    }
}
