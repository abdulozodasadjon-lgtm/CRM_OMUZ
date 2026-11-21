using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs;

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
    [Required, StringLength(200, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Required, Phone, StringLength(32, MinimumLength = 3)]
    public string Phone { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 2)]
    public string Specialization { get; set; } = string.Empty;
}

public sealed class UpdateMentorDto : CreateMentorDto;
