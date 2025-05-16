namespace SiteApi.DTOs
{
    public class SchoolUploadDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Coordinates { get; set; }
        public string Address { get; set; }
        public IFormFile Image { get; set; }
    }
}
