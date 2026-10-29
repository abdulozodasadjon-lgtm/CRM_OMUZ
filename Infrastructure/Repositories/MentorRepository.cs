using Dapper;
using Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class MentorRepository
{
    public async Task<bool> AddMentor(Mentor mentor)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "insert into mentors (FullName, Email, Phone, Specialization) values (@FullName, @Email, @Phone, @Specialization)";
        var add = await conn.ExecuteAsync(query, new { mentor.FullName, mentor.Email, mentor.Phone, mentor.Specialization });
        return add > 0;
    }
    
    public async Task<bool> UpdateMentor(Mentor m)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "update mentors set FullName = @fullname, Email = @email, Phone = @phone, Specialization = @specialization where mentorid = @mentorid";
        var res = await conn.ExecuteAsync(query, new {m.FullName, m.Email, m.Phone, m.Specialization, m.MentorId});
        return res > 0;
    }
    public async Task<bool> DeleteMentor(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "delete from mentors where mentorid = @mid";
        var res = await conn.ExecuteAsync(query, new {mid = id});
        return res > 0;
    }

    public async Task<List<Mentor>> GetListMentors()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select mentorid, fullname, email, phone, specialization from mentors";
        var mentors = await conn.QueryAsync<Mentor>(query);
        return mentors.ToList();
    }

    public async Task<Mentor> GetMentorById(int mid)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select mentorid, fullname, email, phone, specialization from mentors where mentorid = @mid";
        var mentors = await conn.QuerySingleAsync<Mentor>(query, new {mid});
        return mentors;
    }
}
