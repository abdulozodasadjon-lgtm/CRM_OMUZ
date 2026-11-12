using System.Net;
using Application.Responses;
using Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(
    CourseRepository repository,
    ILogger<CoursesController> logger) : ControllerBase
    {


    private readonly CourseRepository _courserepo = repository;
    private readonly ILogger _logger = logger;


    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<CourseDto>>>> GetAll()
    {
        var courses = await repository.GetListOfCourses();
        _logger.LogInformation($"GET /api/Courses/courses was given.");
        return Ok(new ApiResponse<List<CourseDto>>(HttpStatusCode.OK, "Courses retrieved", courses.Select(ToDto).ToList()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<CourseDto>>> GetById(int id)
    {
        var course = await repository.GetCourseById(id);
        if (course is null){ 
            _logger.LogInformation("GET {id} /api/Courses/course is null.");
            return NotFound();
        }
        _logger.LogError("GET {id} /api/Courses/course finded.");
        return Ok(new ApiResponse<CourseDto>(HttpStatusCode.OK, "Course retrieved", ToDto(course)));
    }

    [HttpGet("count")]
    public async Task<ActionResult<ApiResponse<int>>> GetCount()
    {
        var count = await repository.GetTotalCoursesCount();
        _logger.LogInformation("Get {count} /api/Courses/counted.");
        return Ok(new ApiResponse<int>(HttpStatusCode.OK, "Course count retrieved", count));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> Create(CreateCourseDto dto)
    {
        var created = await repository.AddCourse(new Course { Title = dto.Title, Description = dto.Description, DurationWeek = dto.DurationWeek });
        if (!created){
            _logger.LogInformation("Post /api/Courses/not posted.");
            return BadRequest(new ApiResponse<bool>(HttpStatusCode.BadRequest, "Course was not created", false));
        }
        _logger.LogInformation("Post /api/Courses/posted");
        return StatusCode(StatusCodes.Status201Created, new ApiResponse<bool>(HttpStatusCode.Created, "Course created", true));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, UpdateCourseDto dto)
    {
        var updated = await repository.UpdateCourse(new Course { CourseId = id, Title = dto.Title, Description = dto.Description, DurationWeek = dto.DurationWeek });
        if (!updated) {
            _logger.LogInformation("PUT {id} /api/Courses/not putted.");
            return NotFound();
            }
        _logger.LogInformation("PUT {id} /api/Courses/putted.");
        return Ok(new ApiResponse<bool>(HttpStatusCode.OK, "Course updated", true));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Delete {id} /api/Courses/ delete.");
        return await repository.DeleteCourse(id) ? NoContent() : NotFound();
    }

    private static CourseDto ToDto(Course course) => new()
    {
        CourseId = course.CourseId,
        Title = course.Title,
        Description = course.Description,
        DurationWeek = course.DurationWeek
    };
}