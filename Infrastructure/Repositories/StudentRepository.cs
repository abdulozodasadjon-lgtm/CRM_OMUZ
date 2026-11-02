
using System.Formats.Asn1;
using Dapper;
using Domain;
using Domain.Enums;
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

    public async Task<int> GetTotalStudentsCount()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = " select count(*) from students ";
        var result = await conn.QuerySingleAsync<int> (query);
        return result;
    }
    public async Task<List<Student>> GetStudentsWithGroups()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select s.studentid, s.fullname, s.email, s.phone, s.enrollmentdate from studentgroups sg join students s by s.studentid = sg.studentid where sg.groupid is not null";
        var result = conn.QueryAsync<Student> (query);
        var students = await conn.QueryAsync<Student>(query);
        return students.ToList();
    } 


    public async Task<List<Student>> GetStudentsWithoutGroups()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select s.studentid, s.fullname, s.email, s.phone, s.enrollmentdate from studentgroups sg join students s by s.studentid = sg.studentid where sg.groupid is null";
        var result = conn.QueryAsync<Student> (query);
        var students = await conn.QueryAsync<Student>(query);
        return students.ToList();
    } 

    public async Task<List<Student>> GetDroppedStudents()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select s.studentid, s.fullname, s.email, s.phone, s.enrollmentdate from studentgroups sg join students s by s.studentid = sg.studentid where sg.status = @expelled";
        var students = await conn.QueryAsync<Student>(query, new {StatusEnum.Expelled});
        return students.ToList();
    }

    public async Task<List<Student>> GetGraduatedStudents()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select s.studentid, s.fullname, s.email, s.phone, s.enrollmentdate from studentgroups sg join students s by s.studentid = sg.studentid where sg.status = @completed";
        var students = await conn.QueryAsync<Student>(query, new {StatusEnum.Completed});
        return students.ToList();
    }
    
}
