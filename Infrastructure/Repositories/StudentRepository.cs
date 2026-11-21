
using Domain;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StudentRepository(DatabaseContext context)
{
    public async Task<bool> AddStudent(Student student)
    {
        context.Students.Add(student);
        return await context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> UpdateStudent(Student student)
    {
        if (!await context.Students.AnyAsync(existing => existing.StudentId == student.StudentId))
            return false;

        context.Students.Update(student);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteStudent(int id)
    {
        var student = await context.Students.FindAsync(id);
        if (student is null)
            return false;

        context.Students.Remove(student);
        return await context.SaveChangesAsync() > 0;
    }

    public Task<List<Student>> GetListOfStudents() =>
        context.Students.AsNoTracking().ToListAsync();

    public Task<Student?> GetStudentById(int id) =>
        context.Students.AsNoTracking().FirstOrDefaultAsync(student => student.StudentId == id);

    public Task<int> GetTotalStudentsCount() => context.Students.CountAsync();

    public async Task<List<Student>> GetStudentsWithGroups()
        => await context.Students
            .AsNoTracking()
            .Where(student => student.StudentGroups.Any())
            .ToListAsync();

    public async Task<List<Student>> GetStudentsWithoutGroups()
        => await context.Students
            .AsNoTracking()
            .Where(student => !student.StudentGroups.Any())
            .ToListAsync();

    public async Task<List<Student>> GetDroppedStudents()
        => await context.Students
            .AsNoTracking()
            .Where(student => student.StudentGroups.Any(membership => membership.Status == StatusEnum.Expelled))
            .ToListAsync();

    public async Task<List<Student>> GetGraduatedStudents()
        => await context.Students
            .AsNoTracking()
            .Where(student => student.StudentGroups.Any(membership => membership.Status == StatusEnum.Completed))
            .ToListAsync();
    
}
