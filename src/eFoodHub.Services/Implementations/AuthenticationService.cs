using eFoodHub.Entities;
using eFoodHub.Services.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace eFoodHub.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        protected SignInManager<User> signInManager;
        protected UserManager<User> userManager;

        public User AuthenticateUser(string UserName, string Password)
        {
            throw new NotImplementedException();
        }

        public bool CreateUser(User User, string Password)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string UserName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SignOut()
        {
            throw new NotImplementedException();
        }
    }
}