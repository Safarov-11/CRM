using System.Net;
using Dapper;
using DoMain.ApiResponse;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class AnalyticsService(DataContext context) : IAnalyticsService
{
    public async Task<Response<int>> GetCompletionRateAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select (((select count(id) from studentgroups where status = 2) * 100) / count(studentId)) as protsentOtchislennix from studentgroups";

            var res = connection.ExecuteScalar<int>(cmd);
            return res <= 0
            ? new Response<int>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<int>(res, "Success");
        }
    }
}
