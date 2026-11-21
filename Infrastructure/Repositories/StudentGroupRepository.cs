using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StudentGroupRepository(DatabaseContext context)
{
    public async Task<bool> AddStudentGroup(StudentGroup studentGroup)
    {
        context.StudentGroups.Add(studentGroup);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateStudentGroup(StudentGroup studentGroup)
    {
        if (!await context.StudentGroups.AnyAsync(existing => existing.StudentGroupId == studentGroup.StudentGroupId))
            return false;

        context.StudentGroups.Update(studentGroup);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteStudentGroup(int id)
    {
        var studentGroup = await context.StudentGroups.FindAsync(id);
        if (studentGroup is null)
            return false;

        context.StudentGroups.Remove(studentGroup);
        return await context.SaveChangesAsync() > 0;
    }

    public Task<List<StudentGroup>> GetListOfStudentGroups() =>
        context.StudentGroups.AsNoTracking().ToListAsync();

    public Task<StudentGroup?> GetStudentGroupById(int id) =>
        context.StudentGroups.AsNoTracking().FirstOrDefaultAsync(studentGroup => studentGroup.StudentGroupId == id);

    public Task<int> GetTotalStudentGroupsCount() => context.StudentGroups.CountAsync();
}
