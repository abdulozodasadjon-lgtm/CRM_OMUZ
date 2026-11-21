using Domain;

namespace Application.Interfaces;

public interface IGroupService
{
    Task<List<Groups>> GetAllAsync();
    Task<Groups?> GetByIdAsync(int id);
    Task<bool> CreateAsync(Groups group);
    Task<bool> UpdateAsync(Groups group);
    Task<bool> DeleteAsync(int id);
}
