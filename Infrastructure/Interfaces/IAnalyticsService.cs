using DoMain.ApiResponse;

namespace Infrastructure.Interfaces;

public interface IAnalyticsService
{
    Task<Response<int>> GetCompletionRateAsync();
}
