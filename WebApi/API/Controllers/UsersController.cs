using Application.Users;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.Register(request);
        return Ok(result);
    }

    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string? Email, int Code)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.ConfirmEmail(Email, Code);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.Login(request);
        return Ok(result);
    }

    [HttpDelete("{Email}")]
    public async Task<IActionResult> Delete(string Email)
    {
        var result = await _userService.Delete(Email);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetAll();
        return Ok(result);
    }
}