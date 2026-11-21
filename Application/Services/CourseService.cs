using Application.Interfaces;
using Domain;
using Infrastructure.Repositories;

namespace Application.Services;

public sealed class CourseService(CourseRepository repository) : ICourseService
{
    public Task<List<Course>> GetAllAsync() => repository.GetListOfCourses();
    public Task<Course?> GetByIdAsync(int id) => repository.GetCourseById(id);
    public Task<int> GetCountAsync() => repository.GetTotalCoursesCount();
    public Task<bool> CreateAsync(Course course) => repository.AddCourse(course);
    public Task<bool> UpdateAsync(Course course) => repository.UpdateCourse(course);
    public Task<bool> DeleteAsync(int id) => repository.DeleteCourse(id);
}
