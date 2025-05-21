using System.Net;
using Dapper;
using DoMain.ApiResponse;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class MentorService(DataContext context) : IMentorService
{
   public async Task<Response<string>> AddMentorAsync(Mentor mentor)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"insert into mentors(FullName, Email, Phone, Specialization),
                        values(@FullName, @Email, @Phone, @Specialization)";
            var res = await connection.ExecuteAsync(cmd, mentor);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully added mentor");
        }
    }

    public async Task<Response<string>> DeleteMentorAsync(int mentorId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from mentors where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Mentor>(cmd1, new { id = mentorId });
            if (res1 == null)
            {
                return new Response<string>(null, "mentor not founded");
            }

            var cmd = @"delete from mentors where id = @id";
            var res = await connection.ExecuteAsync(cmd, new { id = mentorId });
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully deleted from mentor");
        }
    }

    public async Task<Response<List<Mentor>>> GetAllMentorsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from mentors";
            var res = await connection.QueryAsync<Mentor>(cmd);
            return res == null
            ? new Response<List<Mentor>>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<List<Mentor>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<Mentor>> GetMentorByIdAsync(int mentorId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from mentors where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Mentor>(cmd1, new { id = mentorId });
            if (res1 == null)
            {
                return new Response<Mentor>(null, "mentor not founded");
            }

            var cmd = @"select * from mentors where id = @id";
            var res = await connection.QueryFirstOrDefaultAsync<Mentor>(cmd, new { id = mentorId });
            return res == null
            ? new Response<Mentor>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<Mentor>(res, "Success");
        }
        throw new NotImplementedException();
    }

    public async Task<Response<string>> UpdateMentorAsync(Mentor mentor)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from mentors where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Mentor>(cmd1, new { id = mentor.Id });
            if (res1 == null)
            {
                return new Response<string>(null, "mentor not founded");
            }

            var cmd = @"update mentors 
                        set FullName = @FullName, Email = @email, 
                        Phone = @phone, Specialization = @Specialization
                        where id = @id";
            var res = await connection.ExecuteAsync(cmd, mentor);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "mentor successfully updated");
        }
    }
}
