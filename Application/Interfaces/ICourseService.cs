using Application.Responses;
using Domain;

namespace Application.Interfaces;

public interface ICourseService
{
    Task<ApiResponse<Course>> AddCourse(Course course);
}
