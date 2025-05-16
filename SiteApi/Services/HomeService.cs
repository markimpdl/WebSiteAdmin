using Microsoft.AspNetCore.Cors.Infrastructure;
using SiteApi.DTOs;
using SiteApi.Models;

namespace SiteApi.Services
{
    public class HomeService : IHomeService
    {
        private readonly IGenericService<Slide> _slideService;
        private readonly IGenericService<Logo> _logoService;
        private readonly IGenericService<School> _schoolService;
        private readonly IGenericService<Course> _courseService;
        private readonly IGenericService<Video> _videoService;
        private readonly IGenericService<Assessment> _assessmentService;

        public HomeService(
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
        public async Task<HomeDto> GetHomeDataAsync()
        {
            return new HomeDto
            {
                Logos = await _logoService.GetAllAsync(),
                Slides = await _slideService.GetAllAsync(),
                Videos = await _videoService.GetAllAsync(),
                Schools = await _schoolService.GetAllAsync(),
                Courses = await _courseService.GetAllAsync(),
                Assessments = await _assessmentService.GetAllAsync()
            };
        }
    }
}
