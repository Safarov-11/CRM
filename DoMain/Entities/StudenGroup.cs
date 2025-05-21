using DoMain.Enums;

namespace DoMain.Entities;

public class StudenGroup
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int GroupId { get; set; }
    public Status Status { get; set; }
}