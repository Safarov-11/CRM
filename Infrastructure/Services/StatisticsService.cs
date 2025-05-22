using System.Net;
using Dapper;
using DoMain.ApiResponse;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class StatisticsService(DataContext context) : IStatisticsService
{
    public async Task<Response<List<DateTime>>> GetAllStartDatesAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select distinct(startDate) from groups ";
            var res = await connection.QueryAsync<DateTime>(cmd);

            return res == null
            ? new Response<List<DateTime>>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<List<DateTime>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<int>> GetTotalCoursesCountAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select count(*) from courses";
            var res = await connection.ExecuteScalarAsync<int>(cmd);

            return res == 0
            ? new Response<int>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<int>(res, "Success");
        }
    }

    public async Task<Response<int>> GetTotalGroupsCountAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select count(*) from groups";
            var res = await connection.ExecuteScalarAsync<int>(cmd);

            return res == 0
            ? new Response<int>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<int>(res, "Success");
        }
    }

    public async Task<Response<int>> GetTotalMentorsCountAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select count(*) from mentors";
            var res = await connection.ExecuteScalarAsync<int>(cmd);

            return res == 0
            ? new Response<int>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<int>(res, "Success");
        }
    }

    public async Task<Response<int>> GetTotalStudentsCountAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select count(*) from students";
            var res = await connection.ExecuteScalarAsync<int>(cmd);

            return res == 0
            ? new Response<int>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<int>(res, "Success");
        }
    }

}
