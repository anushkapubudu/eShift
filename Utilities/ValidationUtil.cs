using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eShift.Utilities
{
    public static class ValidationUtil
    {
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        public static bool IsValidPhone(string phone)
        {
            string pattern = @"^07\d{8}$";
            return Regex.IsMatch(phone, pattern);
        }
    }
}
