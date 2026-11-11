using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace WebApi.DTOs;

public sealed class CourseDto
{
    public int CourseId { get; set; }
    public string? Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public int DurationWeek { get; set; }
}

public class CreateCourseDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int DurationWeek { get; set; }
}

public sealed class UpdateCourseDto : CreateCourseDto;

public sealed class MentorDto
{
    public int MentorId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
}

public class CreateMentorDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;

    [Required]
    public string Specialization { get; set; } = string.Empty;
}

public sealed class UpdateMentorDto : CreateMentorDto;

public sealed class GroupDto
{
    public int GroupId { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public int MentorId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}

public class CreateGroupDto
{
    [Required]
    public string GroupName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int CourseId { get; set; }

    [Range(1, int.MaxValue)]
    public int MentorId { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}

public sealed class UpdateGroupDto : CreateGroupDto;

public sealed class StudentDto
{
    public int StudentId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateOnly EnrollmentDate { get; set; }
}

public class CreateStudentDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;

    public DateOnly EnrollmentDate { get; set; }
}

public sealed class UpdateStudentDto : CreateStudentDto;

public sealed class StudentGroupDto
{
    public int StudentGroupId { get; set; }
    public int StudentId { get; set; }
    public int GroupId { get; set; }
    public StatusEnum Status { get; set; }
}

public class CreateStudentGroupDto
{
    [Range(1, int.MaxValue)]
    public int StudentId { get; set; }

    [Range(1, int.MaxValue)]
    public int GroupId { get; set; }

    [EnumDataType(typeof(StatusEnum))]
    public StatusEnum Status { get; set; }
}

public sealed class UpdateStudentGroupDto : CreateStudentGroupDto;