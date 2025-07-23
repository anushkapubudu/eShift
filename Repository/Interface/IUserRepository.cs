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
        List<User> GetAllUsers();
        User GetUserByEmail(string email);           
        bool IsEmailTaken(string email);
        bool IsEmailTakenByAnother(string email, int userId);
        bool UpdateUser(User user);
    }
}
