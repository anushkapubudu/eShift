using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Model
{
    class Staff
    {

        public string FullName => FirstName + " " + LastName;


        public int StaffId { get; set; }
        public string StaffNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }   // DRIVER or ASSISTANT
        public string LicenceNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
