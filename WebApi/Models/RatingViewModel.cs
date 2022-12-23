namespace Models;

public class RatingRequest
{
    public int CourseId { get; set; }
    public int UserId { get; set; }
    public int Stars { get; set; }
    public string? Message { get; set; }
}

public class RatingViewModel
{
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public int Stars { get; set; }
    public string? Message { get; set; }
    public DateTime DateTimeCreated { get; set; }
}