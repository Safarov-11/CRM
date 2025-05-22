using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface ICourseService
{
    Task<Response<string>> AddCourseAsync(Course course);
    Task<Response<List<Course>>> GetAllCoursesAsync();
    Task<Response<Course>> GetCourseByIdAsync(int courseId);
    Task<Response<string>> UpdateCourseAsync(Course course);
    Task<Response<string>> DeleteCourseAsync(int courseId);
    Task<Response<List<StudentPerCourse>>> GetStudentsPerCourseAsync();
    Task<Response<Course>> GetMostPopularCourse();
    Task<Response<Course>> GetLeastPopularCourses();
    Task<Response<Course>> GetTopThreeCourses();
}
