using eShift.Business.Interface;
using eShift.Model;
using eShift.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Business.Services
{
    class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepo;

        public StaffService(IStaffRepository staffRepo)
        {
            _staffRepo = staffRepo;
        }

        public void CreateStaff(Staff staff)
        {
            if (string.IsNullOrWhiteSpace(staff.StaffNumber))
                staff.StaffNumber = GenerateStaffNumber();

            if (string.IsNullOrWhiteSpace(staff.Email) ||
                string.IsNullOrWhiteSpace(staff.FirstName) ||
                string.IsNullOrWhiteSpace(staff.LastName) ||
                string.IsNullOrWhiteSpace(staff.Designation))
                throw new Exception("Missing required staff fields.");

            _staffRepo.CreateStaff(staff);
        }

        public Staff GetStaffById(int staffId)
        {
            var staff = _staffRepo.GetStaffById(staffId);
            if (staff == null)
                throw new Exception($"Staff with ID {staffId} not found.");

            return staff;
        }

        public List<Staff> GetAllStaff()
        {
            return _staffRepo.GetAllStaff();
        }

        public List<Staff> GetDrivers()
        {
            return _staffRepo.GetStaffByDesignation("DRIVER");
        }

        public List<Staff> GetAssistants()
        {
            return _staffRepo.GetStaffByDesignation("ASSISTANT");
        }

        public bool UpdateStaff(Staff staff)
        {
            if (string.IsNullOrWhiteSpace(staff.FirstName) ||
                string.IsNullOrWhiteSpace(staff.LastName))
                throw new Exception("Missing required fields for update.");

            return _staffRepo.UpdateStaff(staff);
        }

        public void DeleteStaff(int staffId)
        {
            _staffRepo.DeleteStaff(staffId);
        }


        public string GenerateStaffNumber()
        {
            int latestId = _staffRepo.GetLastStaffId();
            return $"STF{latestId + 1:0000}";
        }
    }
}
