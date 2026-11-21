using Domain;

namespace Application.Interfaces;

public interface IStudentService
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<int> GetCountAsync();
    Task<List<Student>> GetWithGroupsAsync();
    Task<List<Student>> GetWithoutGroupsAsync();
    Task<List<Student>> GetDroppedAsync();
    Task<List<Student>> GetGraduatedAsync();
    Task<bool> CreateAsync(Student student);
    Task<bool> UpdateAsync(Student student);
    Task<bool> DeleteAsync(int id);
}
