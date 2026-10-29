using Domain.Enums;

namespace Domain.Models;

public class StudentGroup
{
    public int StudentGroupId {get; set; }
    public int StudentId {get; set; }
    public int GroupId {get; set; }
    public  StatusEnum Status {get; set; }
}
