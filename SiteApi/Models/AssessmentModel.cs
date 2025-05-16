namespace SiteApi.Models
{
    public class AssessmentModel
    {
        public int Id { get; set; } 
        public string Password { get; set; }
        public string Title { get; set; }
        public bool Actived { get; set; }
        public string Name { get; set; }
        public string CourseId { get; set; }
        public CourseModel Course { get; set; }

    }
}
