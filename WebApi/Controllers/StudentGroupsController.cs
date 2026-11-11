using System.Net;
using Application.Responses;
using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StudentGroupsController(StudentGroupRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<StudentGroupDto>>>> GetAll()
    {
        var memberships = await repository.GetListOfStudentGroups();
        return Ok(new ApiResponse<List<StudentGroupDto>>(HttpStatusCode.OK, "Student groups retrieved", memberships.Select(ToDto).ToList()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<StudentGroupDto>>> GetById(int id)
    {
        var membership = await repository.GetStudentGroupById(id);
        if (membership is null) return NotFound();
        return Ok(new ApiResponse<StudentGroupDto>(HttpStatusCode.OK, "Student group retrieved", ToDto(membership)));
    }

    [HttpGet("count")]
    public async Task<ActionResult<ApiResponse<int>>> GetCount()
    {
        var count = await repository.GetTotalStudentGroupsCount();
        return Ok(new ApiResponse<int>(HttpStatusCode.OK, "Student group count retrieved", count));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> Create(CreateStudentGroupDto dto)
    {
        var created = await repository.AddStudentGroup(new StudentGroup { StudentId = dto.StudentId, GroupId = dto.GroupId, Status = dto.Status });
        if (!created) return BadRequest(new ApiResponse<bool>(HttpStatusCode.BadRequest, "Student group was not created", false));
        return StatusCode(StatusCodes.Status201Created, new ApiResponse<bool>(HttpStatusCode.Created, "Student group created", true));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, UpdateStudentGroupDto dto)
    {
        var updated = await repository.UpdateStudentGroup(new StudentGroup { StudentGroupId = id, StudentId = dto.StudentId, GroupId = dto.GroupId, Status = dto.Status });
        if (!updated) return NotFound();
        return Ok(new ApiResponse<bool>(HttpStatusCode.OK, "Student group updated", true));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
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