using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(CourseService cServ) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<Course>>> GetAllCoursesAsync()
    {
        return await cServ.GetAllCoursesAsync();
    }

    [HttpGet("{courseId:int}")]
    public async Task<Response<Course>> GetCourseByIdAsync(int courseId)
    {
        return await cServ.GetCourseByIdAsync(courseId);
    }

    [HttpGet("Get course with students count")]
    public async Task<Response<List<StudentPerCourse>>> GetStudentsPerCourseAsync()
    {
        return await cServ.GetStudentsPerCourseAsync();
    }

    [HttpGet("Get least popular courses")]
    public async Task<Response<Course>> GetLeastPopularCourses()
    {
        return await cServ.GetLeastPopularCourses();
    }
    [HttpGet("Get top three popular courses")]
    public async Task<Response<Course>> GetTopThreeCourses()
    {
        return await cServ.GetTopThreeCourses();
    }

    [HttpPost]
    public async Task<Response<string>> CreateCorseAsync(Course course)
    {
        return await cServ.AddCourseAsync(course);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateCorseAsync(Course course)
    {
        return await cServ.UpdateCourseAsync(course);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteCorseAsync(int courseId)
    {
        return await cServ.DeleteCourseAsync(courseId);
    }

}
