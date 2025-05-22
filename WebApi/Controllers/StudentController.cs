using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(StudentService stServ) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<Student>>> GetAllStudentsAsync()
    {
        return await stServ.GetAllStudentsAsync();
    }

    [HttpGet("{studentid:int}")]
    public async Task<Response<Student>> GetStudentByIdAsync(int studentId)
    {
        return await stServ.GetStudentByIdAsync(studentId);
    }

    [HttpGet("Get students with their groups")]
    public async Task<Response<List<StudentWithGroup>>> GetStudentsWithGroupsAsync()
    {
        return await stServ.GetStudentsWithGroupsAsync();
    }

    [HttpGet("Get students without any group")]
    public async Task<Response<List<Student>>> GetStudentsWithoutGroupsAsync()
    {
        return await stServ.GetStudentsWithoutGroupsAsync();
    }

    [HttpGet("Get students who are dropped")]
    public async Task<Response<List<Student>>> GetDroppedOutStudentsAsync()
    {
        return await stServ.GetDroppedOutStudentsAsync();
    }

    [HttpGet("Get students who are graduated")]
    public async Task<Response<List<Student>>> GetGraduatedStudentsAsync()
    {
        return await stServ.GetGraduatedStudentsAsync();
    }

    [HttpPost]
    public async Task<Response<string>> CreateCorseAsync(Student student)
    {
        return await stServ.AddStudentAsync(student);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateCorseAsync(Student student)
    {
        return await stServ.UpdateStudentAsync(student);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteCorseAsync(int studentId)
    {
        return await stServ.DeleteStudentAsync(studentId);
    }
}
