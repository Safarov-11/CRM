using DoMain.ApiResponse;
using DoMain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService stServ) : ControllerBase
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
