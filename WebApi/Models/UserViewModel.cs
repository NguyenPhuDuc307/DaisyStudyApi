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
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class UserViewModel
{
    public int UserId { get; set; }
    public string? FullName { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Dob { get; set; }
    public string? Email { get; set; }
}