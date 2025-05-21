using DoMain.ApiResponse;
using DoMain.Entities; 


namespace Infrastructure.Interfaces;

public interface IGroupService
{
    Task<Response<string>> AddGroupAsync(Group group);
    Task<Response<List<Group>>> GetAllGroupsAsync();
    Task<Response<Group>> GetGroupByIdAsync(int groupId);
    Task<Response<string>> UpdateGroupAsync(Group group);
    Task<Response<string>> DeleteGroupAsync(int groupId);
}
