namespace SiteApi.DTOs
{
    public class CourseUploadDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IFormFile Image1 { get; set; }
        public IFormFile Image2 { get; set; }
        public IFormFile Image3 { get; set; }
        public IFormFile Image4 { get; set; }
    }
}
