using Microsoft.AspNetCore.Http;

namespace Models;

public class LessonRequest
{
    public int CourseId { get; set; }
    public string? LessonName { get; set; }
    public string? Content { get; set; }
    public IFormFile? ImageFile { get; set; }
}

public class LessonViewModel
{
    public int LessonId { get; set; }
    public int CourseId { get; set; }
    public string? LessonName { get; set; }
    public string? CourseName { get; set; }
    public string? Content { get; set; }
    public DateTime DateTimeCreated { get; set; }
    public string? ImagePath { get; set; }
}