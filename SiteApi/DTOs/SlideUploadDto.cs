namespace SiteApi.DTOs
{
    public class SlideUploadDto
    {
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }
    }
}
