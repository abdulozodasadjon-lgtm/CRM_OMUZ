using Application.Interfaces;
using Domain;
using Infrastructure.Repositories;

namespace Application.Services;

public sealed class StudentService(StudentRepository repository) : IStudentService
{
    public Task<List<Student>> GetAllAsync() => repository.GetListOfStudents();
    public Task<Student?> GetByIdAsync(int id) => repository.GetStudentById(id);
    public Task<int> GetCountAsync() => repository.GetTotalStudentsCount();
    public Task<List<Student>> GetWithGroupsAsync() => repository.GetStudentsWithGroups();
    public Task<List<Student>> GetWithoutGroupsAsync() => repository.GetStudentsWithoutGroups();
    public Task<List<Student>> GetDroppedAsync() => repository.GetDroppedStudents();
    public Task<List<Student>> GetGraduatedAsync() => repository.GetGraduatedStudents();
    public Task<bool> CreateAsync(Student student) => repository.AddStudent(student);
    public Task<bool> UpdateAsync(Student student) => repository.UpdateStudent(student);
    public Task<bool> DeleteAsync(int id) => repository.DeleteStudent(id);
}
