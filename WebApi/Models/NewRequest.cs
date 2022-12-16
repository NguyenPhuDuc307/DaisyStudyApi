using Microsoft.AspNetCore.Http;

namespace Models;

public class NewRequest
{
    public string? Title { set; get; }
    public string? Image { set; get; }
    public string? NewUrl { set; get; }
}