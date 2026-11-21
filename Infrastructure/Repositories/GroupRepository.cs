using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GroupRepository(DatabaseContext context)
{
    public async Task<bool> AddGroup(Groups group)
    {
        context.Groups.Add(group);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateGroup(Groups group)
    {
        if (!await context.Groups.AnyAsync(existing => existing.GroupId == group.GroupId))
            return false;

        context.Groups.Update(group);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteGroup(int id)
    {
        var group = await context.Groups.FindAsync(id);
        if (group is null)
            return false;

        context.Groups.Remove(group);
        return await context.SaveChangesAsync() > 0;
    }

    public Task<List<Groups>> GetListOfGroups() =>
        context.Groups.AsNoTracking().ToListAsync();

    public Task<Groups?> GetGroupById(int id) =>
        context.Groups.AsNoTracking().FirstOrDefaultAsync(group => group.GroupId == id);

}
