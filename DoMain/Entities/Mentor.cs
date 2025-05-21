using DoMain.Enums;

namespace DoMain.Entities;

public class Mentor
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Specialization Specialization { get; set; }
}
