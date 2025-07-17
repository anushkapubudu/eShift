using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShift.Model;

namespace eShift.Repository.Interface
{
    interface IUserRepository
    {
        void CreateUser(User user);                 
        User GetUserByEmail(string email);           
        bool IsEmailTaken(string email);
    }
}
