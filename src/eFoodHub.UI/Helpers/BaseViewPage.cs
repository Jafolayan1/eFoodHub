using eFoodHub.Entities;
using eFoodHub.UI.Interfaces;

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace eFoodHub.UI.Helpers
{
    public abstract class BaseViewPage<TModel> : RazorPage<TModel>
    {
        [RazorInject]
        public IUserAccessor UserAccessor { get; set; }

        public User CurrenUser
        {
            get
            {
                if (User != null)
                    return UserAccessor.GetUser();
                else
                    return null;
            }
        }
    }
}
