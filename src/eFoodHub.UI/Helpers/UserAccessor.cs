using eFoodHub.Entities;
using eFoodHub.UI.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace eFoodHub.UI.Helpers
{
    public class UserAccessor : IUserAccessor
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserAccessor(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        /// <summary>
        /// A method to provide the details of currently logged in object user.
        /// HttpContext.User only provides the user name but we used usermanager to get userobjact which return the full user details
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            if (_contextAccessor.HttpContext.User != null)
                return _userManager.GetUserAsync(_contextAccessor.HttpContext.User).Result;
            else
                return null;
        }
    }
}