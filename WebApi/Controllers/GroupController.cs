using DoMain.ApiResponse;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using DoMain.Entities;
using DoMain.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController(GroupService grServ) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<Group>>> GetAllGroupsAsync()
    {
        return await grServ.GetAllGroupsAsync();
    }

    [HttpGet("{groupId:int}")]
    public async Task<Response<Group>> GetGroupByIdAsync(int groupId)
    {
        return await grServ.GetGroupByIdAsync(groupId);
    }

    [HttpGet("Get student per group")]
    public async Task<Response<List<GroupStudentCount>>> GetStudentsPerGroupAsync()
    {
        return await grServ.GetStudentsPerGroupAsync();
    }

    [HttpGet("Get groups without students")]
    public async Task<Response<List<Group>>> GetEmptyGroupsAsync()
    {
        return await grServ.GetEmptyGroupsAsync();
    }

    [HttpPost]
    public async Task<Response<string>> CreateCorseAsync(Group group)
    {
        return await grServ.AddGroupAsync(group);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateCorseAsync(Group group)
    {
        return await grServ.UpdateGroupAsync(group);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteCorseAsync(int groupId)
    {
        return await grServ.DeleteGroupAsync(groupId);
    }
}
