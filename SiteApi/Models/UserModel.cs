using Microsoft.AspNetCore.Identity;

namespace SiteApi.Models
{
    public class UserModel : IdentityUser
    {
        public string Name { get; set; }

    }
}
