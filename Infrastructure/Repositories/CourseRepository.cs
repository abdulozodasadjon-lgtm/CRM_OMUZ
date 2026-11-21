using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CourseRepository(DatabaseContext context)
{
    public async Task<bool> AddCourse(Course course)
    {
        context.Courses.Add(course);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateCourse(Course course)
    {
        if (!await context.Courses.AnyAsync(existing => existing.CourseId == course.CourseId))
            return false;

        context.Courses.Update(course);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCourse(int cid)
    {
        var course = await context.Courses.FindAsync(cid);
        if (course is null)
            return false;

        context.Courses.Remove(course);
        return await context.SaveChangesAsync() > 0;
    }

    public Task<List<Course>> GetListOfCourses() =>
        context.Courses.AsNoTracking().ToListAsync();

    public Task<Course?> GetCourseById(int id) =>
        context.Courses.AsNoTracking().FirstOrDefaultAsync(course => course.CourseId == id);

    public Task<int> GetTotalCoursesCount() => context.Courses.CountAsync();
}
