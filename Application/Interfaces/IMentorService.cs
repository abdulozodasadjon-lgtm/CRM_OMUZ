using Domain;

namespace Application.Interfaces;

public interface IMentorService
{
    Task<List<Mentor>> GetAllAsync();
    Task<Mentor?> GetByIdAsync(int id);
    Task<int> GetCountAsync();
    Task<bool> CreateAsync(Mentor mentor);
    Task<bool> UpdateAsync(Mentor mentor);
    Task<bool> DeleteAsync(int id);
}
