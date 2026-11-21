using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MentorRepository(DatabaseContext context)
{
    public async Task<bool> AddMentor(Mentor mentor)
    {
        context.Mentors.Add(mentor);
        return await context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> UpdateMentor(Mentor mentor)
    {
        if (!await context.Mentors.AnyAsync(existing => existing.MentorId == mentor.MentorId))
            return false;

        context.Mentors.Update(mentor);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteMentor(int id)
    {
        var mentor = await context.Mentors.FindAsync(id);
        if (mentor is null)
            return false;

        context.Mentors.Remove(mentor);
        return await context.SaveChangesAsync() > 0;
    }

    public Task<List<Mentor>> GetListMentors() =>
        context.Mentors.AsNoTracking().ToListAsync();

    public Task<Mentor?> GetMentorById(int id) =>
        context.Mentors.AsNoTracking().FirstOrDefaultAsync(mentor => mentor.MentorId == id);

    public Task<int> GetTotalMentorsCount() => context.Mentors.CountAsync();
}
