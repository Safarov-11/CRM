using System.Net;
using Dapper;
using DoMain.ApiResponse;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using DoMain.Entities;
using DoMain.DTOs;





namespace Infrastructure.Services;

public class GroupService(DataContext context) : IGroupService
{
    public async Task<Response<string>> AddGroupAsync(Group group)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"insert into groups(groupName,courseId, mentorId, startDate, emdDate)
                        values(@groupName, @courseId, @mentorId, @startDate, @emdDate)";
            var res = await connection.ExecuteAsync(cmd, group);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully added group");
        }
    }

    public async Task<Response<string>> DeleteGroupAsync(int groupId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from groups where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Group>(cmd1, new { id = groupId });
            if (res1 == null)
            {
                return new Response<string>(null, "group not founded");
            }

            var cmd = @"delete from groups where id = @id";
            var res = await connection.ExecuteAsync(cmd, new { id = groupId });
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "Successfully deleted from group");
        }
    }

    public async Task<Response<List<Group>>> GetAllGroupsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from groups";
            var res = await connection.QueryAsync<Group>(cmd);
            return res == null
            ? new Response<List<Group>>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<List<Group>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<Group>> GetGroupByIdAsync(int groupId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from groups where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Group>(cmd1, new { id = groupId });
            if (res1 == null)
            {
                return new Response<Group>(null, "group not founded");
            }

            var cmd = @"select * from groups where id = @id";
            var res = await connection.QueryFirstOrDefaultAsync<Group>(cmd, new { id = groupId });
            return res == null
            ? new Response<Group>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<Group>(res, "Success");
        }
        throw new NotImplementedException();
    }

    public async Task<Response<string>> UpdateGroupAsync(Group group)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from groups where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Group>(cmd1, new { id = group.Id });
            if (res1 == null)
            {
                return new Response<string>(null, "group not founded");
            }

            var cmd = @"update groups 
                        set groupName = @groupName ,courseId = @courseId, 
                        mentorId = @mentorId, startDate = @startDate, endDate = @endDate
                        where id = @id";
            var res = await connection.ExecuteAsync(cmd, group);
            return res == null
            ? new Response<string>("Some thing goes wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null, "group successfully updated");
        }
    }

    public async Task<Response<List<GroupStudentCount>>> GetStudentsPerGroupAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select g.groupname, count(s.id) as studentsCount from studentgroups sg
join groups g on g.id = sg.groupId
join students s on s.id = sg.studentId
group by g.groupname";

            var res = await connection.QueryAsync<GroupStudentCount>(cmd);
            return res == null
            ? new Response<List<GroupStudentCount>>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<List<GroupStudentCount>>(res.ToList(), "Success");
        }
    }
    
    public async Task<Response<List<Group>>> GetEmptyGroupsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
select g.* from groups g
join studentgroups sg on sg.groupId = g.id
where sg.studentId is null";

            var res = await connection.QueryAsync<Group>(cmd);
            return res == null
            ? new Response<List<Group>>("something went wrong", HttpStatusCode.InternalServerError)
            : new Response<List<Group>>(res.ToList(), "Success");
        }

    }
}
