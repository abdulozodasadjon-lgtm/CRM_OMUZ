using System.Net;
using Application.Responses;
using Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GroupsController(GroupRepository repository, ILogger logger) : ControllerBase
{

    private readonly ILogger _logger = logger;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<GroupDto>>>> GetAll()
    {
        var groups = await repository.GetListOfGroups();
        _logger.LogInformation("GET /api/Groups/ success.");
        return Ok(new ApiResponse<List<GroupDto>>(HttpStatusCode.OK, "Groups retrieved", groups.Select(ToDto).ToList()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<GroupDto>>> GetById(int id)
    {
        var group = await repository.GetGroupById(id);
        if (group is null) {
            _logger.LogInformation("GET /id /api/Groups/ group is null.");
            return NotFound();
            }
            _logger.LogInformation("GET /id /api/Groups/ success.");
        return Ok(new ApiResponse<GroupDto>(HttpStatusCode.OK, "Group retrieved", ToDto(group)));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> Create(CreateGroupDto dto)
    {
        var created = await repository.AddGroup(new Groups { GroupName = dto.GroupName, CourseId = dto.CourseId, MentorId = dto.MentorId, StartDate = dto.StartDate, EndDate = dto.EndDate });
        if (!created) {
            _logger.LogInformation("POST /api/Groups/ not created.");
            return BadRequest(new ApiResponse<bool>(HttpStatusCode.BadRequest, "Group was not created", false));
            }
        _logger.LogInformation("POST /api/Groups/ success.");
        return StatusCode(StatusCodes.Status201Created, new ApiResponse<bool>(HttpStatusCode.Created, "Group created", true));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, UpdateGroupDto dto)
    {
        var updated = await repository.UpdateGroup(new Groups { GroupId = id, GroupName = dto.GroupName, CourseId = dto.CourseId, MentorId = dto.MentorId, StartDate = dto.StartDate, EndDate = dto.EndDate });
        if (!updated) {
            _logger.LogInformation("PUT / id /api/Groups/ not updated.");
            return NotFound();
            }
        _logger.LogInformation("PUT /api/Groups/ success.");
        return Ok(new ApiResponse<bool>(HttpStatusCode.OK, "Group updated", true));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Delete /api/Groups.");
        return await repository.DeleteGroup(id) ? NoContent() : NotFound();
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