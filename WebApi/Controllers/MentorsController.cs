using System.Net;
using Application.Responses;
using Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MentorsController(MentorRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<MentorDto>>>> GetAll()
    {
        var mentors = await repository.GetListMentors();
        return Ok(new ApiResponse<List<MentorDto>>(HttpStatusCode.OK, "Mentors retrieved", mentors.Select(ToDto).ToList()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<MentorDto>>> GetById(int id)
    {
        var mentor = await repository.GetMentorById(id);
        if (mentor is null) return NotFound();
        return Ok(new ApiResponse<MentorDto>(HttpStatusCode.OK, "Mentor retrieved", ToDto(mentor)));
    }

    [HttpGet("count")]
    public async Task<ActionResult<ApiResponse<int>>> GetCount()
    {
        var count = await repository.GetTotalMentorsCount();
        return Ok(new ApiResponse<int>(HttpStatusCode.OK, "Mentor count retrieved", count));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> Create(CreateMentorDto dto)
    {
        var created = await repository.AddMentor(new Mentor { FullName = dto.FullName, Email = dto.Email, Phone = dto.Phone, Specialization = dto.Specialization });
        if (!created) return BadRequest(new ApiResponse<bool>(HttpStatusCode.BadRequest, "Mentor was not created", false));
        return StatusCode(StatusCodes.Status201Created, new ApiResponse<bool>(HttpStatusCode.Created, "Mentor created", true));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, UpdateMentorDto dto)
    {
        var updated = await repository.UpdateMentor(new Mentor { MentorId = id, FullName = dto.FullName, Email = dto.Email, Phone = dto.Phone, Specialization = dto.Specialization });
        if (!updated) return NotFound();
        return Ok(new ApiResponse<bool>(HttpStatusCode.OK, "Mentor updated", true));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await repository.DeleteMentor(id) ? NoContent() : NotFound();
    }

    private static MentorDto ToDto(Mentor mentor) => new()
    {
        MentorId = mentor.MentorId,
        FullName = mentor.FullName,
        Email = mentor.Email,
        Phone = mentor.Phone,
        Specialization = mentor.Specialization
    };
}