using System.Net;
using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StudentGroupsController(
    IStudentGroupService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<StudentGroupDto>>>> GetAll()
    {
        var memberships = await service.GetAllAsync();
        return Ok(new ApiResponse<List<StudentGroupDto>>(
            HttpStatusCode.OK,
            "Student groups retrieved",
            memberships.Select(ToDto).ToList()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<StudentGroupDto>>> GetById(int id)
    {
        var membership = await service.GetByIdAsync(id);
        if (membership is null)
        {
            return NotFound();
        }

        return Ok(new ApiResponse<StudentGroupDto>(
            HttpStatusCode.OK,
            "Student group retrieved",
            ToDto(membership)));
    }

    [HttpGet("count")]
    public async Task<ActionResult<ApiResponse<int>>> GetCount()
    {
        var count = await service.GetCountAsync();
        return Ok(new ApiResponse<int>(
            HttpStatusCode.OK,
            "Student group count retrieved",
            count));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> Create(CreateStudentGroupDto dto)
    {
        var created = await service.CreateAsync(new StudentGroup
        {
            StudentId = dto.StudentId,
            GroupId = dto.GroupId,
            Status = dto.Status
        });

        if (!created)
        {
            return BadRequest(new ApiResponse<bool>(
                HttpStatusCode.BadRequest,
                "Student group was not created",
                false));
        }

        return StatusCode(
            StatusCodes.Status201Created,
            new ApiResponse<bool>(HttpStatusCode.Created, "Student group created", true));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, UpdateStudentGroupDto dto)
    {
        var updated = await service.UpdateAsync(new StudentGroup
        {
            StudentGroupId = id,
            StudentId = dto.StudentId,
            GroupId = dto.GroupId,
            Status = dto.Status
        });

        if (!updated)
        {
            return NotFound();
        }

        return Ok(new ApiResponse<bool>(
            HttpStatusCode.OK,
            "Student group updated",
            true));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await service.DeleteAsync(id) ? NoContent() : NotFound();
    }

    private static StudentGroupDto ToDto(StudentGroup membership) => new()
    {
        StudentGroupId = membership.StudentGroupId,
        StudentId = membership.StudentId,
        GroupId = membership.GroupId,
        Status = membership.Status
    };
}