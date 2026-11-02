using Domain;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class GroupService
{
    private readonly GroupRepository _groupRepository;
    private readonly StudentGroupRepository _studentGroupRepository;

    public GroupService(GroupRepository groupRepository, StudentGroupRepository studentGroupRepository)
    {
        _groupRepository = groupRepository;
        _studentGroupRepository = studentGroupRepository;
    }

    public async Task<List<(Groups Group, int StudentCount)>> GetStudentsPerGroup()
    {
        var groups = await _groupRepository.GetListOfGroups();
        var studentGroups = await _studentGroupRepository.GetListOfStudentGroups();
        var countsByGroup = studentGroups
            .GroupBy(studentGroup => studentGroup.GroupId)
            .ToDictionary(
                group => group.Key,
                group => group.Select(studentGroup => studentGroup.StudentId).Distinct().Count());

        return groups
            .Select(group => (
                Group: group,
                StudentCount: countsByGroup.GetValueOrDefault(group.GroupId)))
            .ToList();
    }

    public async Task<List<Groups>> GetEmptyGroups()
    {
        var groups = await _groupRepository.GetListOfGroups();
        var studentGroups = await _studentGroupRepository.GetListOfStudentGroups();
        var occupiedGroupIds = studentGroups
            .Select(studentGroup => studentGroup.GroupId)
            .ToHashSet();

        return groups
            .Where(group => !occupiedGroupIds.Contains(group.GroupId))
            .ToList();
    }
}
