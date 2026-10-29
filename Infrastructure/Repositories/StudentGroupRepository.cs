using Dapper;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class StudentGroupRepository
{
    public async Task<bool> AddStudentGroup(StudentGroup studentGroup)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "insert into studentgroups (studentid, groupid, status) values (@StudentId, @GroupId, @Status)";
        var add = await conn.ExecuteAsync(query, new{ studentGroup.StudentId, studentGroup.GroupId, Status = studentGroup.Status.ToString()});
        return add > 0;
    }

    public async Task < bool > UpdateStudentGroup(StudentGroup studentGroup)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "update studentgroups set studentid = @StudentId, groupid = @GroupId, status = @Status where studentgroupid = @StudentGroupId";
        var update = conn.Execute(query, new { studentGroup.StudentGroupId, studentGroup.StudentId, studentGroup.GroupId, Status = studentGroup.Status.ToString() });
        return update > 0;
    }

    public async Task< bool > DeleteStudentGroup(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "delete from studentgroups where studentgroupid = @id";
        var delete = await conn.ExecuteAsync(query, new { id });
        return delete > 0;
    }

    public async Task<List<StudentGroup>> GetListOfStudentGroups()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select studentgroupid, studentid, groupid, status from studentgroups";
        var list = await conn.QueryAsync<StudentGroup>(query);
         return list.ToList();
    }

    public async Task<StudentGroup> GetStudentGroupById(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select studentgroupid, studentid, groupid, status from studentgroups where studentgroupid = @id";
        var groupstudent = await  conn.QuerySingleAsync<StudentGroup>(query, new { id });
        return groupstudent;
    }
}
