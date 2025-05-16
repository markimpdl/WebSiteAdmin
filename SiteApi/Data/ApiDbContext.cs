using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteApi.Models;

namespace SiteApi.Data
{
    public class ApiDbContext: IdentityDbContext<User>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Logo> Logos { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assessment> Assessments { get; set; }

    }
}
