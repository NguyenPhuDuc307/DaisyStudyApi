using System.ComponentModel.DataAnnotations;

namespace Models;

public class RegisterRequest
{
    public string? FullName { get; set; }
    public DateTime Dob { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class LoginRequest
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}