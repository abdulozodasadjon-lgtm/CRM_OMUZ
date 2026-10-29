
using System.Formats.Asn1;
using Dapper;
using Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class StudentRepository
{
    public async Task<bool> AddStudent(Student student)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "insert into students (Fullname, Email, Phone, EnrollmentDate) values (@fullname, @email, @phone, @enrollmentdate)";
        var add = await conn.ExecuteAsync(query, new {student.FullName, student.Email, student.Phone, student.EnrollmentDate});
        return add > 0;
    }
    
    public async Task<bool> UpdateStudent(Student student)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "update students set fullname = @fullname, email = @email, phone = @phone, enrollmentdate = @enrollmentdate where studentid = @studentid";
        var update = await conn.ExecuteAsync(query, new {student.StudentId, student.FullName, student.Email, student.Phone, student.EnrollmentDate});
        return update > 0;
    }

    public async Task <bool> DeleteStudent (int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "delete from students where studentid = @studentid";
        var update = await conn.ExecuteAsync(query, new { studentid = id });
        return update > 0;
    }

    public async Task<List<Student>> GetListOfStudents()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select studentid, fullname, email, phone, enrollmentdate from students";
        var students = await conn.QueryAsync<Student>(query);
        return students.ToList();
    }

    public async Task<Student> GetStudentById(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select studentid, fullname, email, phone, enrollmentdate from students where studentid = @studentid";
        var student = await conn.QuerySingleAsync<Student>(query, new { studentid = id });
        return student;
    }
}
