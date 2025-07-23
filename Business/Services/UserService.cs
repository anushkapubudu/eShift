using eShift.Business.Interface;
using eShift.Model;
using eShift.Repository.Interface;
using eShift.Repository.Services;
using eShift.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace eShift.Business.Services
{
    class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }


        LoginResult IUserService.Login(string email, string plainPassword)
        {
            var user = _userRepo.GetUserByEmail(email);
            if (user == null)
                return LoginResult.UserNotExit;

            return PasswordUtil.Instance.VerifyPassword(plainPassword, user.PasswordHash)
                   ? LoginResult.Success
                   : LoginResult.InvalidEmailOrPassword;
        }

        RegistrationResult IUserService.RegisterUser(User user, string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(plainPassword))
                return RegistrationResult.MissingFields;

            if (_userRepo.IsEmailTaken(user.Email))
                return RegistrationResult.EmailAlreadyExists;

            user.PasswordHash = PasswordUtil.Instance.HashPassword(plainPassword);
            _userRepo.CreateUser(user);

            return RegistrationResult.Success;
        }

        public User GetUserDetails(string email)
        {
            return _userRepo.GetUserByEmail(email);
        }

        public CustomerUpdateResult UpdateCustomerProfile(User updatedUser)
        {
            if (string.IsNullOrWhiteSpace(updatedUser.Email) ||
         string.IsNullOrWhiteSpace(updatedUser.FirstName) ||
         string.IsNullOrWhiteSpace(updatedUser.LastName) ||
         string.IsNullOrWhiteSpace(updatedUser.Telephone) ||
         string.IsNullOrWhiteSpace(updatedUser.Address))
                return CustomerUpdateResult.ValidationError;

            if (_userRepo.IsEmailTakenByAnother(updatedUser.Email, updatedUser.UserId))
                return CustomerUpdateResult.EmailInUse;

            bool updated = _userRepo.UpdateUser(updatedUser);
            return updated ? CustomerUpdateResult.Success : CustomerUpdateResult.Failure;
        }


        public List<CustomerSelection> GetAllCustomerSelection()
        {
            return _userRepo.GetAllUsers().Select(u => new CustomerSelection
            {
                UserId = u.UserId,
                FullName = $"{u.FirstName} {u.LastName}"
            })
                .ToList();
        }


    }
}
