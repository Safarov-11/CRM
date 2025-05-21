namespace DoMain.Entities;

public class Group
{
    public int Id { get; set; }
    public string GroupName { get; set; }
    public int CourseId { get; set; }
    public int MentorId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
