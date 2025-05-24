using System.Net;
using Dapper;
using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class StudentService(DataContext context, IWebHostEnviroment webHostEnviroment ) : IStudentService
{
    public async Task<Response<string>> AddStudentAsync(Student student)
    {
            var wwwRootPath = webHostEnviroment.WevRootPath;
            var folderPath = Path.Combine(wwwRootPath, "StudentsImages");
            var fileName = car.Photo.fileNamee;

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        
            var fullPath = Path.Combine(folderPath, fileName);
        using (var connection = await context.GetDbConnectionAsync())
        {

            await using (var connection = await context.GetConnectionAsync())
            {
            await using (var stream = File.Create(fullPath))
            {
                await student.Photo.CopyToAsync(stream);
            }


            var cmd = @"insert into students(FullName, Email, Phone, EnrollmentDate, photo)
                        values(@FullName, @Email, @Phone, @Specialization, @photo)";
            var anonymObject = new {
                FullName = student.FullName,
                Email = student.Email,
                Phone = student.phone,
                EnrollmentDate = student.EnrollmentDate,
                Photo = student.Photo.fileName,
            };
            var res = await connection.ExecuteAsync(cmd, anonymObject);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully added student");
            }
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
                return new Response<string>("student not founded", HttpStatusCode.n);
            }

            var cmd = @"delete from students where id = @id";
            var res = await connection.ExecuteAsync(cmd, new { id = studentId });
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully deleted from student");
        }
    }

    public async Task<Response<List<StudentwWithImage>>> GetAllStudentsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from students";
            var res = await connection.QueryAsync<StudentWithImage>(cmd);
            return res == null
            ? new Response<List<StudentWithImage>>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<List<StudentWithImage>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<StudentWithImage>> GetStudentByIdAsync(int studentId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from students where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<StudentWithImage>(cmd1, new { id = studentId });
            if (res1 == null)
            {
                return new Response<StudentWithImage>(null, "student not founded");
            }

            var cmd = @"select * from students where id = @id";
            var res = await connection.QueryFirstOrDefaultAsync<Student>(cmd, new { id = studentId });
            return res == null
            ? new Response<StudentWithImage>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<StudentWithImage>(res, "Success");
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
