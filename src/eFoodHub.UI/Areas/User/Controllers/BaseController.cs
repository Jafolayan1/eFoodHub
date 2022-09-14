using eFoodHub.UI.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace eFoodHub.UI.Areas.User.Controllers
{
    //[CustomAuthorize(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
        public Entities.User CurrentUser
        {
            get
            {
                if (User != null)
                    return _userAccessor.GetUser();
                else
                    return null;
            }
        }

        private readonly IUserAccessor _userAccessor;

        public BaseController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }
    }
}