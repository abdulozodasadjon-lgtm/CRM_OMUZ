using System.Net;
using Application.Responses;
using Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StudentsController(StudentRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<StudentDto>>>> GetAll()
    {
        var students = await repository.GetListOfStudents();
        return Ok(new ApiResponse<List<StudentDto>>(HttpStatusCode.OK, "Students retrieved", students.Select(ToDto).ToList()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<StudentDto>>> GetById(int id)
    {
        var student = await repository.GetStudentById(id);
        if (student is null) return NotFound();
        return Ok(new ApiResponse<StudentDto>(HttpStatusCode.OK, "Student retrieved", ToDto(student)));
    }

    [HttpGet("count")]
    public async Task<ActionResult<ApiResponse<int>>> GetCount()
    {
        var count = await repository.GetTotalStudentsCount();
        return Ok(new ApiResponse<int>(HttpStatusCode.OK, "Student count retrieved", count));
    }

    [HttpGet("with-groups")]
    public async Task<ActionResult<ApiResponse<List<StudentDto>>>> GetWithGroups()
    {
        var students = await repository.GetStudentsWithGroups();
        return Ok(new ApiResponse<List<StudentDto>>(HttpStatusCode.OK, "Students with groups retrieved", students.Select(ToDto).ToList()));
    }

    [HttpGet("without-groups")]
    public async Task<ActionResult<ApiResponse<List<StudentDto>>>> GetWithoutGroups()
    {
        var students = await repository.GetStudentsWithoutGroups();
        return Ok(new ApiResponse<List<StudentDto>>(HttpStatusCode.OK, "Students without groups retrieved", students.Select(ToDto).ToList()));
    }

    [HttpGet("dropped")]
    public async Task<ActionResult<ApiResponse<List<StudentDto>>>> GetDropped()
    {
        var students = await repository.GetDroppedStudents();
        return Ok(new ApiResponse<List<StudentDto>>(HttpStatusCode.OK, "Dropped students retrieved", students.Select(ToDto).ToList()));
    }

    [HttpGet("graduated")]
    public async Task<ActionResult<ApiResponse<List<StudentDto>>>> GetGraduated()
    {
        var students = await repository.GetGraduatedStudents();
        return Ok(new ApiResponse<List<StudentDto>>(HttpStatusCode.OK, "Graduated students retrieved", students.Select(ToDto).ToList()));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> Create(CreateStudentDto dto)
    {
        var created = await repository.AddStudent(new Student { FullName = dto.FullName, Email = dto.Email, Phone = dto.Phone, EnrollmentDate = dto.EnrollmentDate });
        if (!created) return BadRequest(new ApiResponse<bool>(HttpStatusCode.BadRequest, "Student was not created", false));
        return StatusCode(StatusCodes.Status201Created, new ApiResponse<bool>(HttpStatusCode.Created, "Student created", true));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(int id, UpdateStudentDto dto)
    {
        var updated = await repository.UpdateStudent(new Student { StudentId = id, FullName = dto.FullName, Email = dto.Email, Phone = dto.Phone, EnrollmentDate = dto.EnrollmentDate });
        if (!updated) return NotFound();
        return Ok(new ApiResponse<bool>(HttpStatusCode.OK, "Student updated", true));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await repository.DeleteStudent(id) ? NoContent() : NotFound();
    }

    private static StudentDto ToDto(Student student) => new()
    {
        StudentId = student.StudentId,
        FullName = student.FullName,
        Email = student.Email,
        Phone = student.Phone,
        EnrollmentDate = student.EnrollmentDate
    };
}