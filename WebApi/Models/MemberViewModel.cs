using Microsoft.AspNetCore.Http;

namespace Models;

public class MemberViewModel
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
}