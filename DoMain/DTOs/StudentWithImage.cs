using Microsoft.AspNetCore.Http;

namespace DoMain.Entities;

public class StudentWithImage
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public IFormFile Photo { get; set; }
}
