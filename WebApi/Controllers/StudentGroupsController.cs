using System.Net;
using Application.Responses;
using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StudentGroupsController(
    StudentGroupRepository repository,
    ILogger<StudentGroupsController> logger) : ControllerBase
{
    private readonly ILogger _logger = logger;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<StudentGroupDto>>>> GetAll()
    {
        var memberships = await repository.GetListOfStudentGroups();
        _logger.LogInformation("GET /api/StudentGroups was called.");
        return Ok(new ApiResponse<List<StudentGroupDto>>(
            HttpStatusCode.OK,
            "Student groups retrieved",
            memberships.Select(ToDto).ToList()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<StudentGroupDto>>> GetById(int id)
    {
        var membership = await repository.GetStudentGroupById(id);
        if (membership is null)
        {
            _logger.LogInformation("GET {id} /api/StudentGroups was not found.", id);
            return NotFound();
        }

        _logger.LogInformation("GET {id} /api/StudentGroups succeeded.", id);
        return Ok(new ApiResponse<StudentGroupDto>(
            HttpStatusCode.OK,
            "Student group retrieved",
            ToDto(membership)));
    }

    [HttpGet("count")]
    public async Task<ActionResult<ApiResponse<int>>> GetCount()
    {
        var count = await repository.GetTotalStudentGroupsCount();
        _logger.LogInformation("GET /api/StudentGroups/count was called.");
        return Ok(new ApiResponse<int>(
            HttpStatusCode.OK,
            "Student group count retrieved",
            count));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> Create(CreateStudentGroupDto dto)
    {
        var created = await repository.AddStudentGroup(new StudentGroup
        {
            StudentId = dto.StudentId,
            GroupId = dto.GroupId,
            Status = dto.Status
        });

        if (!created)
        {
            _logger.LogInformation("POST /api/StudentGroups was not created.");
            return BadRequest(new ApiResponse<bool>(
                HttpStatusCode.BadRequest,
                "Student group was not created",
                false));
        }

        _logger.LogInformation("POST /api/StudentGroups was created.");
        return StatusCode(
            StatusCodes.Status201Created,
            new ApiResponse<bool>(HttpStatusCode.Created, "Student group created", true));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, UpdateStudentGroupDto dto)
    {
        var updated = await repository.UpdateStudentGroup(new StudentGroup
        {
            StudentGroupId = id,
            StudentId = dto.StudentId,
            GroupId = dto.GroupId,
            Status = dto.Status
        });

        if (!updated)
        {
            _logger.LogInformation("PUT {id} /api/StudentGroups was not updated.", id);
            return NotFound();
        }

        _logger.LogInformation("PUT {id} /api/StudentGroups was updated.", id);
        return Ok(new ApiResponse<bool>(
            HttpStatusCode.OK,
            "Student group updated",
            true));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("DELETE {id} /api/StudentGroups was called.", id);
        return await repository.DeleteStudentGroup(id) ? NoContent() : NotFound();
    }

    private static StudentGroupDto ToDto(StudentGroup membership) => new()
    {
        StudentGroupId = membership.StudentGroupId,
        StudentId = membership.StudentId,
        GroupId = membership.GroupId,
        Status = membership.Status
    };
}