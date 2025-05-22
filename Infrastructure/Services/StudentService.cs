using System.Net;
using Dapper;
using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class StudentService(DataContext context) : IStudentService
{
    public async Task<Response<string>> AddStudentAsync(Student student)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"insert into students(FullName, Email, Phone, EnrollmentDate)
                        values(@FullName, @Email, @Phone, @Specialization)";
            var res = await connection.ExecuteAsync(cmd, student);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully added student");
        }
    }

    public async Task<Response<string>> DeleteStudentAsync(int studentId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from students where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Student>(cmd1, new { id = studentId });
            if (res1 == null)
            {
                return new Response<string>(null, "student not founded");
            }

            var cmd = @"delete from students where id = @id";
            var res = await connection.ExecuteAsync(cmd, new { id = studentId });
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully deleted from student");
        }
    }

    public async Task<Response<List<Student>>> GetAllStudentsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from students";
            var res = await connection.QueryAsync<Student>(cmd);
            return res == null
            ? new Response<List<Student>>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<List<Student>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<Student>> GetStudentByIdAsync(int studentId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from students where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Student>(cmd1, new { id = studentId });
            if (res1 == null)
            {
                return new Response<Student>(null, "student not founded");
            }

            var cmd = @"select * from students where id = @id";
            var res = await connection.QueryFirstOrDefaultAsync<Student>(cmd, new { id = studentId });
            return res == null
            ? new Response<Student>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<Student>(res, "Success");
        }
    }

    public async Task<Response<string>> UpdateStudentAsync(Student student)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from students where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Student>(cmd1, new { id = student.Id });
            if (res1 == null)
            {
                return new Response<string>(null, "student not founded");
            }

            var cmd = @"update students 
                        set FullName = @FullName, Email = @email, 
                        Phone = @phone, EnrollmentDate = @EnrollmentDate 
                        where id = @id";
            var res = await connection.ExecuteAsync(cmd, student);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "student successfully updated");
        }
    }

    public async Task<Response<List<StudentWithGroup>>> GetStudentsWithGroupsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select s.fullname, s.email,g.id, g.groupname from studentgroups sg
join students s on s.id = sg.studentId 
join groups g on g.id = sg.groupId 
group by s.fullname, s.email,g.id, g.groupname";
            var res = await connection.QueryAsync<StudentWithGroup>(cmd);

            return res == null
            ? new Response<List<StudentWithGroup>>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<List<StudentWithGroup>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<List<Student>>> GetStudentsWithoutGroupsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select s.* from students s
left join studentgroups sg on s.id = sg.studentId  
where sg.groupId is null";
            var res = await connection.QueryAsync<Student>(cmd);

            return res == null
            ? new Response<List<Student>>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<List<Student>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<List<Student>>> GetDroppedOutStudentsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select s.* from students s 
left join studentgroups sg on s.id = sg.studentId 
where status = 3";
            var res = await connection.QueryAsync<Student>(cmd);

            return res == null
            ? new Response<List<Student>>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<List<Student>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<List<Student>>> GetGraduatedStudentsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select s.* from students s 
left join studentgroups sg on s.id = sg.studentId 
where status = 2";
            var res = await connection.QueryAsync<Student>(cmd);

            return res == null
            ? new Response<List<Student>>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<List<Student>>(res.ToList(), "Success");
        }
    }
}
