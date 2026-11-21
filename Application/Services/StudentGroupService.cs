using Application.Interfaces;
using Domain.Models;
using Infrastructure.Repositories;

namespace Application.Services;

public sealed class StudentGroupService(StudentGroupRepository repository) : IStudentGroupService
{
    public Task<List<StudentGroup>> GetAllAsync() => repository.GetListOfStudentGroups();
    public Task<StudentGroup?> GetByIdAsync(int id) => repository.GetStudentGroupById(id);
    public Task<int> GetCountAsync() => repository.GetTotalStudentGroupsCount();
    public Task<bool> CreateAsync(StudentGroup studentGroup) => repository.AddStudentGroup(studentGroup);
    public Task<bool> UpdateAsync(StudentGroup studentGroup) => repository.UpdateStudentGroup(studentGroup);
    public Task<bool> DeleteAsync(int id) => repository.DeleteStudentGroup(id);
}
