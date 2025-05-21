using DoMain.ApiResponse;
using DoMain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentGroupController(IStudentGroupService stGrServ) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<StudenGroup>>> GetStudenGroupsAsync()
    {
        return await stGrServ.GetGroupStudentsAsync();
    }

    [HttpPost]
    public async Task<Response<string>> AddStudentToGroupAsync(StudenGroup studenGroup)
    {
        return await stGrServ.AddStudentToGroupAsync(studenGroup);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateStudentInGroupAsync(StudenGroup studenGroup)
    {
        return await stGrServ.UpdateStudentInGroupAsync(studenGroup);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteStudentFromGroupAsync(StudenGroup studenGroup)
    {
        return await stGrServ.DeleteStudentFromGroupAsync(studenGroup);
    }
}
