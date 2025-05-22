using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IMentorService
{
    Task<Response<string>> AddMentorAsync(Mentor mentor);
    Task<Response<List<Mentor>>> GetAllMentorsAsync();
    Task<Response<Mentor>> GetMentorByIdAsync(int mentorId);
    Task<Response<string>> UpdateMentorAsync(Mentor mentor);
    Task<Response<string>> DeleteMentorAsync(int mentorId);
    Task<Response<Mentor>> GetMentorWithMostStudentsAsync();
    Task<Response<List<MentorWithMaxCourses>>> GetMentorsWithMultipleCoursesAsync();
}
