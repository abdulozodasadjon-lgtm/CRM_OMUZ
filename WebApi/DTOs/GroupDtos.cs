using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs;

public sealed class GroupDto
{
    public int GroupId { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public int MentorId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}

public class CreateGroupDto : IValidatableObject
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string GroupName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int CourseId { get; set; }

    [Range(1, int.MaxValue)]
    public int MentorId { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate == default)
            yield return new ValidationResult("Start date is required.", [nameof(StartDate)]);

        if (EndDate == default)
            yield return new ValidationResult("End date is required.", [nameof(EndDate)]);
        else if (StartDate != default && EndDate < StartDate)
            yield return new ValidationResult("End date must be on or after the start date.", [nameof(EndDate)]);
    }
}

public sealed class UpdateGroupDto : CreateGroupDto;
