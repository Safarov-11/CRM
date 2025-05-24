using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MentorController(MentorService mServ) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<Mentor>>> GetAllMentorsAsync()
    {
        return await mServ.GetAllMentorsAsync();
    }

    [HttpGet("{mentorid:int}")]
    public async Task<Response<Mentor>> GetMentorByIdAsync(int mentorId)
    {
        return await mServ.GetMentorByIdAsync(mentorId);
    }

    [HttpGet("Get Mentor with most students")]
    public async Task<Response<MentorWithMostStudents>> GetMentorWithMostStudents()
    {
        return await mServ.GetMentorWithMostStudentsAsync();
    }

    [HttpGet("Get Mentor with multiple courses")]
    public async Task<Response<List<Mentor>>> GetMentorsWithMultipleCoursesAsync()
    {
        return await mServ.GetMentorsWithMultipleCoursesAsync();
    }

    [HttpPost]
    public async Task<Response<string>> CreateCorseAsync(Mentor mentor)
    {
        return await mServ.AddMentorAsync(mentor);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateCorseAsync(Mentor mentor)
    {
        return await mServ.UpdateMentorAsync(mentor);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteCorseAsync(int mentorId)
    {
        return await mServ.DeleteMentorAsync(mentorId);
    }
}
