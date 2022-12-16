namespace Models;

public class CommentCreateRequest
{
    public int LessonId { get; set; }
    public int UserId { get; set; }
    public string? Content { get; set; }
}

public class CommentUpdateRequest
{
    public string? Content { get; set; }
}

public class CommentViewModel
{
    public int CommentId { get; set; }
    public int LessonId { get; set; }
    public string? LessonName { get; set; }
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Content { get; set; }
    public DateTime DateTimeCreated { get; set; }
}