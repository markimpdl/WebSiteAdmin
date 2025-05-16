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
    private readonly IImageUploadService _imageUploadService;

    public AdminController(
        IGenericService<Slide> slideService,
        IGenericService<Course> courseService,
        IGenericService<School> schoolService,
        IGenericService<Logo> logoService,
        IImageUploadService imageUploadService)
    {
        _slideService = slideService;
        _courseService = courseService;
        _schoolService = schoolService;
        _logoService = logoService;
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

    [HttpPut("slide/{id}")]
    public async Task<IActionResult> UpdateSlide(int id, [FromForm] SlideUploadDto dto)
    {
        var slide = await _slideService.GetByIdAsync(id);
        if (slide == null) return NotFound();

        slide.ImageUrl = await _imageUploadService.UploadImageAsync(dto.Image);
        slide.Title = dto.Title;
        slide.ButtonText = dto.ButtonText;
        slide.ButtonLink = dto.ButtonLink;

        await _slideService.UpdateAsync(slide);
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

    [HttpPut("course/{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromForm] CourseUploadDto dto)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null) return NotFound();

        course.Name = dto.Name;
        course.Description = dto.Description;
        course.Image1 = await _imageUploadService.UploadImageAsync(dto.Image1);
        course.Image2 = await _imageUploadService.UploadImageAsync(dto.Image2);
        course.Image3 = await _imageUploadService.UploadImageAsync(dto.Image3);
        course.Image4 = await _imageUploadService.UploadImageAsync(dto.Image4);

        await _courseService.UpdateAsync(course);
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

    [HttpPut("school/{id}")]
    public async Task<IActionResult> UpdateSchool(int id, [FromForm] SchoolUploadDto dto)
    {
        var school = await _schoolService.GetByIdAsync(id);
        if (school == null) return NotFound();

        school.Name = dto.Name;
        school.Phone = dto.Phone;
        school.Email = dto.Email;
        school.Coordinates = dto.Coordinates;
        school.Address = dto.Address;
        school.ImageUrl = await _imageUploadService.UploadImageAsync(dto.Image);

        await _schoolService.UpdateAsync(school);
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
        var imageUrl = await _imageUploadService.UploadImageAsync(dto.Image);
        var logo = new Logo { ImageUrl = imageUrl };
        await _logoService.AddAsync(logo);
        return Ok(logo);
    }

    [HttpPut("logo/{id}")]
    public async Task<IActionResult> UpdateLogo(int id, [FromForm] LogoUploadDto dto)
    {
        var logo = await _logoService.GetByIdAsync(id);
        if (logo == null) return NotFound();
        logo.ImageUrl = await _imageUploadService.UploadImageAsync(dto.Image);
        await _logoService.UpdateAsync(logo);
        return Ok(logo);
    }
    
}
