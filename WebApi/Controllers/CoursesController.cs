using System.Net;
using Application.Responses;
using Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CoursesController(CourseRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<CourseDto>>>> GetAll()
    {
        var courses = await repository.GetListOfCourses();
        return Ok(new ApiResponse<List<CourseDto>>(HttpStatusCode.OK, "Courses retrieved", courses.Select(ToDto).ToList()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<CourseDto>>> GetById(int id)
    {
        var course = await repository.GetCourseById(id);
        if (course is null) return NotFound();
        return Ok(new ApiResponse<CourseDto>(HttpStatusCode.OK, "Course retrieved", ToDto(course)));
    }

    [HttpGet("count")]
    public async Task<ActionResult<ApiResponse<int>>> GetCount()
    {
        var count = await repository.GetTotalCoursesCount();
        return Ok(new ApiResponse<int>(HttpStatusCode.OK, "Course count retrieved", count));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> Create(CreateCourseDto dto)
    {
        var created = await repository.AddCourse(new Course { Title = dto.Title, Description = dto.Description, DurationWeek = dto.DurationWeek });
        if (!created) return BadRequest(new ApiResponse<bool>(HttpStatusCode.BadRequest, "Course was not created", false));
        return StatusCode(StatusCodes.Status201Created, new ApiResponse<bool>(HttpStatusCode.Created, "Course created", true));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, UpdateCourseDto dto)
    {
        var updated = await repository.UpdateCourse(new Course { CourseId = id, Title = dto.Title, Description = dto.Description, DurationWeek = dto.DurationWeek });
        if (!updated) return NotFound();
        return Ok(new ApiResponse<bool>(HttpStatusCode.OK, "Course updated", true));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
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