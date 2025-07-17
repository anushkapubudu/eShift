using eShift.Business.Interface;
using eShift.Model;
using eShift.Repository.Interface;
using eShift.Utilities;

namespace eShift.Business.Services
{
    class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        User IUserService.Login(string email, string plainPassword)
        {
            var user = _userRepo.GetUserByEmail(email);
            if (user == null)
                return null;

            return PasswordUtil.Instance.VerifyPassword(plainPassword, user.PasswordHash)
                   ? user
                   : null;
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
    }
}
