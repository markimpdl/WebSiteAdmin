using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteApi.Models;
using SiteApi.Services;

namespace SiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly IGenericService<Slide> _slideService;
        private readonly IGenericService<Logo> _logoService;
        private readonly IGenericService<School> _schoolService;
        private readonly IGenericService<Course> _courseService;
        private readonly IGenericService<Video> _videoService;
        private readonly IGenericService<Assessment> _assessmentService;

        public PublicController(
            IGenericService<Slide> slideService,
            IGenericService<Logo> logoService,
            IGenericService<School> schoolService,
            IGenericService<Course> courseService,
            IGenericService<Video> videoService,
            IGenericService<Assessment> assessmentService)
        {
            _slideService = slideService;
            _logoService = logoService;
            _schoolService = schoolService;
            _courseService = courseService;
            _videoService = videoService;   
            _assessmentService = assessmentService;  
        }

        [HttpGet("home-info")]
        public async Task<IActionResult> GetHome([FromServices] IHomeService homeService)
        {
            var result = await homeService.GetHomeDataAsync();
            return Ok(result);
        }
    }
}
