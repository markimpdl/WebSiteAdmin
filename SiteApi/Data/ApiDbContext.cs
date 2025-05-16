using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteApi.Models;

namespace SiteApi.Data
{
    public class ApiDbContext: IdentityDbContext<UserModel>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<LogoModel> Logos { get; set; }
        public DbSet<SlideModel> Slides { get; set; }
        public DbSet<VideoModel> Videos { get; set; }
        public DbSet<SchoolModel> Schools { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<AssessmentModel> Assessments { get; set; }

    }
}
