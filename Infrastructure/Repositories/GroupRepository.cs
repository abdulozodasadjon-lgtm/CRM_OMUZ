using Dapper;
using Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class GroupRepository
{
    public async Task<bool> AddGroup(Groups group)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "insert into groups (groupname, courseid, mentorid, startdate, enddate) values (@GroupName, @CourseId, @MentorId, @StartDate, @EndDate)";
        var add =await conn.ExecuteAsync(query, group);
        return add > 0;
    }

    public async Task<bool> UpdateGroup(Groups group)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "update groups set groupname = @GroupName, courseid = @CourseId, mentorid = @MentorId, startdate = @StartDate, enddate = @EndDate where groupid = @GroupId";
        var update = await conn.ExecuteAsync(query, group);
        return update > 0;
    }

    public async Task<bool> DeleteGroup(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "delete from groups where groupid = @id";
        var delete = await conn.ExecuteAsync(query, new { id });
        return delete > 0;
    }

    public async Task<List<Groups>> GetListOfGroups()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select groupid, groupname, courseid, mentorid, startdate, enddate from groups";
        var groups = await conn.QueryAsync<Groups>(query);
        return groups.ToList();
    }

    public async Task<Groups> GetGroupById(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select groupid, groupname, courseid, mentorid, startdate, enddate from groups where groupid = @id";
        var group = await conn.QuerySingleAsync<Groups>(query, new { id });
        return group;
    }
}
