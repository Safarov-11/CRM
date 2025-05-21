using System.Net;
using Dapper;
using DoMain.ApiResponse;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class CourseService(DataContext context) : ICourseService
{
    public async Task<Response<string>> AddCourseAsync(Course course)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"insert into courses(title, description, durationWeeks),
                        values(@title, @description, @durationWeeks)";
            var res = await connection.ExecuteAsync(cmd, course);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully added course");
        }
    }

    public async Task<Response<string>> DeleteCourseAsync(int courseId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from courses where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Course>(cmd1,  new { id = courseId});
            if (res1 == null)
            {
                return new Response<string>(null, "course not founded");
            }

            var cmd = @"delete from courses where id = @id";
            var res = await connection.ExecuteAsync(cmd,  new { id = courseId});
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully deleted from course");
        } 
    }

    public async Task<Response<List<Course>>> GetAllCoursesAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from courses";
            var res = await connection.QueryAsync<Course>(cmd);
            return res == null
            ? new Response<List<Course>>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<List<Course>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<Course>> GetCourseByIdAsync(int courseId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from courses where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Course>(cmd1,  new { id = courseId});
            if (res1 == null)
            {
                return new Response<Course>(null, "course not founded");
            }

            var cmd = @"select * from courses where id = @id";
            var res = await connection.QueryFirstOrDefaultAsync<Course>(cmd, new { id = courseId});
            return res == null
            ? new Response<Course>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<Course>(res, "Success");
        }
        throw new NotImplementedException();
    }

    public async Task<Response<string>> UpdateCourseAsync(Course course)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from courses where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Course>(cmd1, new { id = course.Id});
            if (res1 == null)
            {
                return new Response<string>(null, "course not founded");
            }

            var cmd = @"update courses 
                        set title = @title, description = @description,
                        durationWeeks = @durationWeeks
                        where id = @id";
            var res = await connection.ExecuteAsync(cmd, course);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Course successfully updated");
        } 
        
    }

}
