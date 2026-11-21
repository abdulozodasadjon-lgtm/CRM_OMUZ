using Domain.Models;

namespace Application.Interfaces;

public interface IStudentGroupService
{
    Task<List<StudentGroup>> GetAllAsync();
    Task<StudentGroup?> GetByIdAsync(int id);
    Task<int> GetCountAsync();
    Task<bool> CreateAsync(StudentGroup studentGroup);
    Task<bool> UpdateAsync(StudentGroup studentGroup);
    Task<bool> DeleteAsync(int id);
}
