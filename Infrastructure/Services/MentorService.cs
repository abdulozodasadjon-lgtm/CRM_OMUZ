using Domain;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class MentorService
{
    private readonly MentorRepository _mentorRepository;
    private readonly GroupRepository _groupRepository;
    private readonly StudentGroupRepository _studentGroupRepository;

    public MentorService(
        MentorRepository mentorRepository,
        GroupRepository groupRepository,
        StudentGroupRepository studentGroupRepository)
    {
        _mentorRepository = mentorRepository;
        _groupRepository = groupRepository;
        _studentGroupRepository = studentGroupRepository;
    }

    public async Task<(Mentor Mentor, int StudentCount)?> GetMentorWithMostStudents()
    {
        var mentors = await _mentorRepository.GetListMentors();
        var groups = await _groupRepository.GetListOfGroups();
        var studentGroups = await _studentGroupRepository.GetListOfStudentGroups();
        var groupIdsByMentor = groups
            .GroupBy(group => group.MentorId)
            .ToDictionary(group => group.Key, group => group.Select(item => item.GroupId).ToHashSet());

        var mentorsByStudentCount = mentors
            .Select(mentor =>
            {
                var groupIds = groupIdsByMentor.GetValueOrDefault(mentor.MentorId) ?? [];
                var studentCount = studentGroups
                    .Where(studentGroup => groupIds.Contains(studentGroup.GroupId))
                    .Select(studentGroup => studentGroup.StudentId)
                    .Distinct()
                    .Count();

                return (Mentor: mentor, StudentCount: studentCount);
            })
            .OrderByDescending(mentor => mentor.StudentCount)
            .ThenBy(mentor => mentor.Mentor.MentorId)
            .Take(1)
            .ToList();

        return mentorsByStudentCount.Count == 0 ? null : mentorsByStudentCount[0];
    }

    public async Task<List<Mentor>> GetMentorsWithMultipleCourses()
    {
        var mentors = await _mentorRepository.GetListMentors();
        var groups = await _groupRepository.GetListOfGroups();
        var mentorIdsWithMultipleCourses = groups
            .GroupBy(group => group.MentorId)
            .Where(group => group.Select(item => item.CourseId).Distinct().Count() > 1)
            .Select(group => group.Key)
            .ToHashSet();

        return mentors
            .Where(mentor => mentorIdsWithMultipleCourses.Contains(mentor.MentorId))
            .ToList();
    }
}
