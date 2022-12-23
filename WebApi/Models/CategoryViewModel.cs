namespace Models;

public class CategoryRequest
{
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
}

public class CategoryViewModel
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
}