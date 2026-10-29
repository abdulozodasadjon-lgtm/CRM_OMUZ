using System.Data;
using Dapper;
using Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CourseRepository
{
    public async Task<bool> AddCourse(Course course)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "insert into courses (Title, Description, DurationWeek) values (@Title, @Description, @DurationWeek)";
        var add = await conn.ExecuteAsync(query, new { course.Title, course.Description, course.DurationWeek });
        return add > 0;
    }

    public async Task<bool> UpdateCourse(Course course)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query =  "update courses set Title = @title, Description = @description, DurationWeek = @durationweek where courseid = @courseid";
        var res = await conn.ExecuteAsync(query, new {course.CourseId, course.Title, course.Description, course.DurationWeek});
        return res > 0;
    }

    public async Task<bool> DeleteCourse(int cid)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "delete from courses where courseid = @cid";
        var res = await conn.ExecuteAsync(query, new {cid});
        return res > 0;
    }

    public async Task<List<Course>> GetListOfCourses()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select courseid, title, description, durationweek from courses";
        var courses = await conn.QueryAsync<Course>(query);
        return courses.ToList();
    }


    public async Task<Course> GetCourseById(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select courseid, title, description, durationweek from courses where courseid = @id";
        var course = await conn.QuerySingleAsync<Course>(query, new {id});
        return course;
    }
}
