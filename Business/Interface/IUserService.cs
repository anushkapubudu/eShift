using eShift.Model;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Business.Interface
{
    interface IUserService
    {
        RegistrationResult RegisterUser(User user, string plainPassword);
        LoginResult Login(string email, string plainPassword);
        User GetUserDetails(string email);
    }
}
