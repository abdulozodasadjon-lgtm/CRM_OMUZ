using Application.Interfaces;
using Domain;
using Infrastructure.Repositories;

namespace Application.Services;

public sealed class GroupService(GroupRepository repository) : IGroupService
{
    public Task<List<Groups>> GetAllAsync() => repository.GetListOfGroups();
    public Task<Groups?> GetByIdAsync(int id) => repository.GetGroupById(id);
    public Task<bool> CreateAsync(Groups group) => repository.AddGroup(group);
    public Task<bool> UpdateAsync(Groups group) => repository.UpdateGroup(group);
    public Task<bool> DeleteAsync(int id) => repository.DeleteGroup(id);
}
