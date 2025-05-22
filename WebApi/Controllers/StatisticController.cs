using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using DoMain.ApiResponse;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticController(StatisticsService statServ) : ControllerBase
{   
    [HttpGet("Get Count of students")]
    public async Task<Response<int>> GeTotalStudentsCountAsync()
    {
        return await statServ.GetTotalStudentsCountAsync();
    }

    [HttpGet("Get Count of Mentors")]
    public async Task<Response<int>> GeTotalMentorsCountAsync()
    {
        return await statServ.GetTotalMentorsCountAsync();
    }

    [HttpGet("Get Count of Groups")]
    public async Task<Response<int>> GeTotalGroupsCountAsync()
    {
        return await statServ.GetTotalGroupsCountAsync();
    }

    [HttpGet("Get Count of courses")]
    public async Task<Response<int>> GetTotalCoursesCountAsync()
    {
        return await statServ.GetTotalCoursesCountAsync();
    }

    [HttpGet("Get unique start dates")]
    public async Task<Response<List<DateTime>>> GetAllStartDatesAsync()
    {
        return await statServ.GetAllStartDatesAsync();
    }
}
