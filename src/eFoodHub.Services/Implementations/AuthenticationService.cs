using eFoodHub.Entities;
using eFoodHub.Services.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace eFoodHub.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        protected SignInManager<User> _signInManager;
        protected UserManager<User> _userManager;
        protected RoleManager<Role> _roleManager;

        public AuthenticationService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public User AuthenticateUser(string UserName, string Password)
        {
            var result = _signInManager.PasswordSignInAsync(UserName, Password, false, lockoutOnFailure: false).Result;

            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(UserName).Result;
                var roles = _userManager.GetRolesAsync(user).Result;
                user.Roles = roles.ToArray();

                return user;
            }
            return null;
        }

        public bool CreateUser(User User, string Password)
        {
            var result = _userManager.CreateAsync(User, Password).Result;
            if (result.Succeeded)
            {
                //Admin, User
                string role = "Admin";
                var res = _userManager.AddToRoleAsync(User, role).Result;
                if (res.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public User GetUser(string UserName)
        {
            return _userManager.FindByNameAsync(UserName).Result;
        }

        public async Task<bool> SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}