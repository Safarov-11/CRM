using DoMain.ApiResponse;
using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IStudentGroupService
{
    Task<Response<string>> AddStudentToGroupAsync(StudenGroup studenGroup);
    Task<Response<List<StudenGroup>>> GetGroupStudentsAsync();
    Task<Response<string>> UpdateStudentInGroupAsync(StudenGroup studenGroup);
    Task<Response<string>> DeleteStudentFromGroupAsync(StudenGroup StudenGroup);
}
