using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IStudentService
{
    Task<Response<string>> AddStudentAsync(Student student);
    Task<Response<List<StudentWithImage>>> GetAllStudentsAsync();
    Task<Response<Student>> GetStudentByIdAsync(int studentId);
    Task<Response<string>> UpdateStudentAsync(Student student);
    Task<Response<string>> DeleteStudentAsync(int studentId);
    Task<Response<List<StudentWithGroup>>> GetStudentsWithGroupsAsync();
    Task<Response<List<Student>>> GetStudentsWithoutGroupsAsync();
    Task<Response<List<Student>>> GetDroppedOutStudentsAsync();
    Task<Response<List<Student>>> GetGraduatedStudentsAsync();

}
