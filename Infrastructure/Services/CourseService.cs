using Domain;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CourseService
{
    private readonly CourseRepository _courseRepository;
    private readonly GroupRepository _groupRepository;
    private readonly StudentGroupRepository _studentGroupRepository;

    public CourseService(
        CourseRepository courseRepository,
        GroupRepository groupRepository,
        StudentGroupRepository studentGroupRepository)
    {
        _courseRepository = courseRepository;
        _groupRepository = groupRepository;
        _studentGroupRepository = studentGroupRepository;
    }

    public Task<List<(Course Course, int StudentCount)>> GetStudentsPerCourse() =>
        GetCourseStudentCounts();

    public async Task<(Course Course, int StudentCount)?> GetMostPopularCourse()
    {
        var courses = await GetCourseStudentCounts();
        var mostPopular = courses
            .OrderByDescending(course => course.StudentCount)
            .ThenBy(course => course.Course.CourseId)
            .Take(1)
            .ToList();

        return mostPopular.Count == 0 ? null : mostPopular[0];
    }

    public async Task<List<(Course Course, int StudentCount)>> GetLeastPopularCourses()
    {
        var courses = await GetCourseStudentCounts();
        return courses
            .OrderBy(course => course.StudentCount)
            .ThenBy(course => course.Course.CourseId)
            .Take(3)
            .ToList();
    }

    public async Task<List<(Course Course, int StudentCount)>> GetTopThreeCourses()
    {
        var courses = await GetCourseStudentCounts();
        return courses
            .OrderByDescending(course => course.StudentCount)
            .ThenBy(course => course.Course.CourseId)
            .Take(3)
            .ToList();
    }

    private async Task<List<(Course Course, int StudentCount)>> GetCourseStudentCounts()
    {
        var courses = await _courseRepository.GetListOfCourses();
        var groups = await _groupRepository.GetListOfGroups();
        var studentGroups = await _studentGroupRepository.GetListOfStudentGroups();
        var groupIdsByCourse = groups
            .GroupBy(group => group.CourseId)
            .ToDictionary(group => group.Key, group => group.Select(item => item.GroupId).ToHashSet());

        return courses
            .Select(course =>
            {
                var groupIds = groupIdsByCourse.GetValueOrDefault(course.CourseId) ?? [];
                var studentCount = studentGroups
                    .Where(studentGroup => groupIds.Contains(studentGroup.GroupId))
                    .Select(studentGroup => studentGroup.StudentId)
                    .Distinct()
                    .Count();

                return (Course: course, StudentCount: studentCount);
            })
            .ToList();
    }
}
