using System.ComponentModel.DataAnnotations;

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
    [Required, StringLength(200, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(2000, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int DurationWeek { get; set; }
}

public sealed class UpdateCourseDto : CreateCourseDto;
