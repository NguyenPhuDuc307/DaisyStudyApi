using Microsoft.AspNetCore.Http;

namespace Models;

public class CourseRequest
{
    public int CategoryId { get; set; }
    public string? CourseName { get; set; }
    public string? Description { get; set; }
    public IFormFile? ImageFile { get; set; }
}

public class CourseViewModel
{
    public int CourseId { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? CourseName { get; set; }
    public int Members { get; set; }
    public string? Description { get; set; }
    public DateTime DateTimeCreated { get; set; }
    public string? ImagePath { get; set; }
}