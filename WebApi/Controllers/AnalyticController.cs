using DoMain.ApiResponse;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticController(AnalyticsService aServ) : ControllerBase
{
    [HttpGet]
    public async Task<Response<int>> GetCompletionRateAsync()
    {
        return await aServ.GetCompletionRateAsync();
    }
}
