using System.Net;
using Dapper;
using DoMain.ApiResponse;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class StudentGroupService(DataContext context) : IStudentGroupService
{
    public async Task<Response<string>> AddStudentToGroupAsync(StudenGroup studenGroup)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from students where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Group>(cmd1, new { id = studenGroup.StudentId });
            if (res1 == null)
            {
                return new Response<string>(null, "student not founded");
            }
            var cmd2 = @"select * from grpu[s] where id = @id";
            var res2 = await connection.QueryFirstOrDefaultAsync<Group>(cmd2, new { id = studenGroup.GroupId });
            if (res2 == null)
            {
                return new Response<string>(null, "group not founded");
            }           
           
            var cmd = @"insert into studentGroups(studentId, groupId, status),
                        values(@studentId, @groupId, @status)";
            var res = await connection.ExecuteAsync(cmd, studenGroup);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully added group");
        }
    }

    public async Task<Response<string>> DeleteStudentFromGroupAsync(StudenGroup studenGroup)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {

            var cmd1 = @"select * from students where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Group>(cmd1, new { id = studenGroup.StudentId });
            if (res1 == null)
            {
                return new Response<string>(null, "student not founded");
            }
            var cmd2 = @"select * from grpu[s] where id = @id";
            var res2 = await connection.QueryFirstOrDefaultAsync<Group>(cmd2, new { id = studenGroup.GroupId });
            if (res2 == null)
            {
                return new Response<string>(null, "group not founded");
            }

            var cmd = @"delete from studentGroups where id = @id";
            var res = await connection.ExecuteAsync(cmd, new { id = studenGroup.Id });
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully deleted student from group");
        }
    }

    public async Task<Response<List<StudenGroup>>> GetGroupStudentsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from studentgroups";
            var res = await connection.QueryAsync<StudenGroup>(cmd);
            return res == null
            ? new Response<List<StudenGroup>>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<List<StudenGroup>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<string>> UpdateStudentInGroupAsync(StudenGroup studenGroup)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from students where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Group>(cmd1, new { id = studenGroup.StudentId });
            if (res1 == null)
            {
                return new Response<string>(null, "student not founded");
            }
            var cmd2 = @"select * from grpu[s] where id = @id";
            var res2 = await connection.QueryFirstOrDefaultAsync<Group>(cmd2, new { id = studenGroup.GroupId });
            if (res2 == null)
            {
                return new Response<string>(null, "group not founded");
            }

            var cmd = @"update studentgroups 
                        set studentId = @studentId, 
                        groupId = @groupId, status = @status
                        where id = @id";
            var res = await connection.ExecuteAsync(cmd, studenGroup);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "student's group successfully updated");
        }
    }

}
