using DoMain.ApiResponse;

namespace Infrastructure.Interfaces;

public interface IStatisticsService
{
    Task<Response<int>> GetTotalStudentsCountAsync();
    Task<Response<int>> GetTotalGroupsCountAsync();
    Task<Response<int>> GetTotalCoursesCountAsync();
    Task<Response<int>> GetTotalMentorsCountAsync();
    Task<Response<List<DateTime>>> GetAllStartDatesAsync();
}
