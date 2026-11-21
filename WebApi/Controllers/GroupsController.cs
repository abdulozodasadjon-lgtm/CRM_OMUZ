using System.Net;
using Application.Interfaces;
using Application.Responses;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GroupsController(IGroupService service) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<GroupDto>>>> GetAll()
    {
        var groups = await service.GetAllAsync();
        return Ok(new ApiResponse<List<GroupDto>>(HttpStatusCode.OK, "Groups retrieved", groups.Select(ToDto).ToList()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<GroupDto>>> GetById(int id)
    {
        var group = await service.GetByIdAsync(id);
        if (group is null) {
            return NotFound();
            }
        return Ok(new ApiResponse<GroupDto>(HttpStatusCode.OK, "Group retrieved", ToDto(group)));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> Create(CreateGroupDto dto)
    {
        var created = await service.CreateAsync(new Groups { GroupName = dto.GroupName, CourseId = dto.CourseId, MentorId = dto.MentorId, StartDate = dto.StartDate, EndDate = dto.EndDate });
        if (!created) {
            return BadRequest(new ApiResponse<bool>(HttpStatusCode.BadRequest, "Group was not created", false));
            }
        return StatusCode(StatusCodes.Status201Created, new ApiResponse<bool>(HttpStatusCode.Created, "Group created", true));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, UpdateGroupDto dto)
    {
        var updated = await service.UpdateAsync(new Groups { GroupId = id, GroupName = dto.GroupName, CourseId = dto.CourseId, MentorId = dto.MentorId, StartDate = dto.StartDate, EndDate = dto.EndDate });
        if (!updated) {
            return NotFound();
            }
        return Ok(new ApiResponse<bool>(HttpStatusCode.OK, "Group updated", true));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await service.DeleteAsync(id) ? NoContent() : NotFound();
    }

    private static GroupDto ToDto(Groups group) => new()
    {
        GroupId = group.GroupId,
        GroupName = group.GroupName,
        CourseId = group.CourseId,
        MentorId = group.MentorId,
        StartDate = group.StartDate,
        EndDate = group.EndDate
    };
}