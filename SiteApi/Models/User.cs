using Microsoft.AspNetCore.Identity;

namespace SiteApi.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

    }
}
