using DoMain.ApiResponse;
using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IStudentService
{
    Task<Response<string>> AddStudentAsync(Student student);
    Task<Response<List<Student>>> GetAllStudentsAsync();
    Task<Response<Student>> GetStudentByIdAsync(int studentId);
    Task<Response<string>> UpdateStudentAsync(Student student);
    Task<Response<string>> DeleteStudentAsync(int studentId);
}
