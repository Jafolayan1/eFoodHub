using Microsoft.AspNetCore.Identity;

namespace eFoodHub.Entities
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}