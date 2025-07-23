using eShift.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Repository.Interface
{
    interface IStaffRepository
    {
        void CreateStaff(Staff staff);
        Staff GetStaffById(int staffId);
        List<Staff> GetAllStaff();
        List<Staff> GetStaffByDesignation(string designation); 
        bool UpdateStaff(Staff staff);
        void DeleteStaff(int staffId);
        int GetLastStaffId();
    }
}
