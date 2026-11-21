using Application.Interfaces;
using Domain;
using Infrastructure.Repositories;

namespace Application.Services;

public sealed class MentorService(MentorRepository repository) : IMentorService
{
    public Task<List<Mentor>> GetAllAsync() => repository.GetListMentors();
    public Task<Mentor?> GetByIdAsync(int id) => repository.GetMentorById(id);
    public Task<int> GetCountAsync() => repository.GetTotalMentorsCount();
    public Task<bool> CreateAsync(Mentor mentor) => repository.AddMentor(mentor);
    public Task<bool> UpdateAsync(Mentor mentor) => repository.UpdateMentor(mentor);
    public Task<bool> DeleteAsync(int id) => repository.DeleteMentor(id);
}
