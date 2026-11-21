using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace WebApi.DTOs;

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
