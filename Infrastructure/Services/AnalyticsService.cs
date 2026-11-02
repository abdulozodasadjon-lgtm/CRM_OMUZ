using Domain.Enums;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class AnalyticsService
{
    private readonly StudentRepository _studentRepository;
    private readonly StudentGroupRepository _studentGroupRepository;

    public AnalyticsService(
        StudentRepository studentRepository,
        StudentGroupRepository studentGroupRepository)
    {
        _studentRepository = studentRepository;
        _studentGroupRepository = studentGroupRepository;
    }

    public async Task<decimal> GetCompletionRate()
    {
        var students = await _studentRepository.GetListOfStudents();
        if (students.Count == 0)
        {
            return 0m;
        }

        var completedStudentIds = (await _studentGroupRepository.GetListOfStudentGroups())
            .Where(studentGroup => studentGroup.Status == StatusEnum.Completed)
            .Select(studentGroup => studentGroup.StudentId)
            .ToHashSet();
        var completedStudentCount = students.Count(student => completedStudentIds.Contains(student.StudentId));

        return completedStudentCount * 100m / students.Count;
    }
}
