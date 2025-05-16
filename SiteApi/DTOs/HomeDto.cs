using SiteApi.Models;

namespace SiteApi.DTOs
{
    public class HomeDto
    {
        public IEnumerable<Logo>  Logos{ get; set; }
        public IEnumerable<Slide> Slides { get; set; }
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<School> Schools { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Assessment> Assessments { get; set; }
    }
}
