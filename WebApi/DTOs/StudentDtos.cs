using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs;

public sealed class StudentDto
{
    public int StudentId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateOnly EnrollmentDate { get; set; }
}

public class CreateStudentDto : IValidatableObject
{
    [Required, StringLength(200, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Required, Phone, StringLength(32, MinimumLength = 3)]
    public string Phone { get; set; } = string.Empty;

    public DateOnly EnrollmentDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EnrollmentDate == default)
            yield return new ValidationResult("Enrollment date is required.", [nameof(EnrollmentDate)]);
    }
}

public sealed class UpdateStudentDto : CreateStudentDto;
