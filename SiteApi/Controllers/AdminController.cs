using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteApi.DTOs;
using SiteApi.Models;
using SiteApi.Services;


[ApiController]
[Authorize]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IGenericService<Slide> _slideService;
    private readonly IGenericService<Course> _courseService;
    private readonly IGenericService<School> _schoolService;
    private readonly IGenericService<Logo> _logoService;
    private readonly IGenericService<Video> _videoService;
    private readonly IImageUploadService _imageUploadService;

    public AdminController(
        IGenericService<Slide> slideService,
        IGenericService<Course> courseService,
        IGenericService<School> schoolService,
        IGenericService<Logo> logoService,
        IGenericService<Video> videoService,
        IImageUploadService imageUploadService)
    {
        _slideService = slideService;
        _courseService = courseService;
        _schoolService = schoolService;
        _logoService = logoService;
        _videoService = videoService;
        _imageUploadService = imageUploadService;
    }

    [HttpPost("slide")]
    public async Task<IActionResult> AddSlide([FromForm] SlideUploadDto dto)
    {
        var imageUrl = await _imageUploadService.UploadImageAsync(dto.Image);

        var slide = new Slide
        {
            ImageUrl = imageUrl,
            Title = dto.Title,
            ButtonText = dto.ButtonText,
            ButtonLink = dto.ButtonLink
        };

        await _slideService.AddAsync(slide);
        return Ok(slide);
    }

    [HttpPatch("slide/{id}")]
    public async Task<IActionResult> PatchSlide(int id, [FromForm] SlideUploadDto dto)
    {
        var slide = await _slideService.GetByIdAsync(id);
        if (slide == null) return NotFound();

        if (dto.Image != null)
            slide.ImageUrl = await _imageUploadService.UploadImageAsync(dto.Image);

        if (!string.IsNullOrWhiteSpace(dto.Title))
            slide.Title = dto.Title;

        if (!string.IsNullOrWhiteSpace(dto.ButtonText))
            slide.ButtonText = dto.ButtonText;

        if (!string.IsNullOrWhiteSpace(dto.ButtonLink))
            slide.ButtonLink = dto.ButtonLink;

        await _slideService.UpdateAsync(slide);
        return Ok(slide);
    }

    [HttpGet("slide")]
    public async Task<IActionResult> GetSlides()
    {
        var result = await _slideService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("slide/{id}")]
    public async Task<IActionResult> GetSlideById(int id)
    {
        var slide = await _slideService.GetByIdAsync(id);
        if (slide == null) return NotFound();
        return Ok(slide);
    }


    [HttpDelete("slide/{id}")]
    public async Task<IActionResult> DeleteSlide(int id)
    {
        await _slideService.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("course")]
    public async Task<IActionResult> AddCourse([FromForm] CourseUploadDto dto)
    {
        var course = new Course
        {
            Name = dto.Name,
            Description = dto.Description,
            Image1 = await _imageUploadService.UploadImageAsync(dto.Image1),
            Image2 = await _imageUploadService.UploadImageAsync(dto.Image2),
            Image3 = await _imageUploadService.UploadImageAsync(dto.Image3),
            Image4 = await _imageUploadService.UploadImageAsync(dto.Image4)
        };

        await _courseService.AddAsync(course);
        return Ok(course);
    }

    [HttpPatch("course/{id}")]
    public async Task<IActionResult> PatchCourse(int id, [FromForm] CourseUploadDto dto)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null) return NotFound();

        // Só atualiza se não for nulo
        if (!string.IsNullOrWhiteSpace(dto.Name))
            course.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Description))
            course.Description = dto.Description;

        if (dto.Image1 != null)
            course.Image1 = await _imageUploadService.UploadImageAsync(dto.Image1);

        if (dto.Image2 != null)
            course.Image2 = await _imageUploadService.UploadImageAsync(dto.Image2);

        if (dto.Image3 != null)
            course.Image3 = await _imageUploadService.UploadImageAsync(dto.Image3);

        if (dto.Image4 != null)
            course.Image4 = await _imageUploadService.UploadImageAsync(dto.Image4);

        await _courseService.UpdateAsync(course);
        return Ok(course);
    }

    [HttpGet("course")]
    public async Task<IActionResult> GetCourses()
    {
        var result = await _courseService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("course/{id}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null) return NotFound();
        return Ok(course);
    }


    [HttpDelete("course/{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        await _courseService.DeleteAsync(id);
        return Ok();
    }


    [HttpPost("school")]
    public async Task<IActionResult> AddSchool([FromForm] SchoolUploadDto dto)
    {
        var imageUrl = await _imageUploadService.UploadImageAsync(dto.Image);
        var school = new School
        {
            Name = dto.Name,
            Phone = dto.Phone,
            Email = dto.Email,
            Coordinates = dto.Coordinates,
            Address = dto.Address,
            ImageUrl = imageUrl
        };
        await _schoolService.AddAsync(school);
        return Ok(school);
    }

    [HttpPatch("school/{id}")]
    public async Task<IActionResult> PatchSchool(int id, [FromForm] SchoolUploadDto dto)
    {
        var school = await _schoolService.GetByIdAsync(id);
        if (school == null) return NotFound();

        if (!string.IsNullOrWhiteSpace(dto.Name))
            school.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Phone))
            school.Phone = dto.Phone;

        if (!string.IsNullOrWhiteSpace(dto.Email))
            school.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.Coordinates))
            school.Coordinates = dto.Coordinates;

        if (!string.IsNullOrWhiteSpace(dto.Address))
            school.Address = dto.Address;

        if (dto.Image != null)
            school.ImageUrl = await _imageUploadService.UploadImageAsync(dto.Image);

        await _schoolService.UpdateAsync(school);
        return Ok(school);
    }

    [HttpGet("school")]
    public async Task<IActionResult> GetSchools()
    {
        var result = await _schoolService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("school/{id}")]
    public async Task<IActionResult> GetSchoolById(int id)
    {
        var school = await _schoolService.GetByIdAsync(id);
        if (school == null) return NotFound();
        return Ok(school);
    }

    [HttpDelete("school/{id}")]
    public async Task<IActionResult> DeleteSchool(int id)
    {
        await _schoolService.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("logo")]
    public async Task<IActionResult> SetLogo([FromForm] LogoUploadDto dto)
    {
        var logos = await _logoService.GetAllAsync();
        var existing = logos.FirstOrDefault();

        var imageUrl = await _imageUploadService.UploadImageAsync(dto.Image);

        if (existing != null)
        {
            existing.ImageUrl = imageUrl;
            await _logoService.UpdateAsync(existing);
            return Ok(existing);
        }

        var newLogo = new Logo { ImageUrl = imageUrl };
        await _logoService.AddAsync(newLogo);
        return Ok(newLogo);
    }

    [HttpGet("logo")]
    public async Task<IActionResult> GetLogo()
    {
        var logo = (await _logoService.GetAllAsync()).FirstOrDefault();
        if (logo == null) return NotFound();
        return Ok(logo);
    }

    [HttpPost("video")]
    public async Task<IActionResult> SetVideo([FromBody] VideoDto dto)
    {
        var videos = await _videoService.GetAllAsync();
        var existing = videos.FirstOrDefault();

        if (existing != null)
        {
            existing.Url = dto.Url;
            await _videoService.UpdateAsync(existing);
            return Ok(existing);
        }

        var newVideo = new Video { Url = dto.Url };
        await _videoService.AddAsync(newVideo);
        return Ok(newVideo);
    }

    [HttpGet("video")]
    public async Task<IActionResult> GetVideo()
    {
        var video = (await _videoService.GetAllAsync()).FirstOrDefault();
        if (video == null) return NotFound();
        return Ok(video);
    }

    [HttpDelete("video/{id}")]
    public async Task<IActionResult> DeleteVideo(int id)
    {
        await _videoService.DeleteAsync(id);
        return Ok();
    }


}
