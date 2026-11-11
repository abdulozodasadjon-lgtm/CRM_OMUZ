namespace Domain;

public class Groups
{
    public int GroupId {get; set; }
    public string GroupName {get; set; } = string.Empty;
    public int CourseId {get; set; }
    public int MentorId {get; set; }
    public DateOnly StartDate {get; set; }
    public DateOnly EndDate {get; set; }
}
