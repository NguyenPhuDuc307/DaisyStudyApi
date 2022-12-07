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
        if (result == 0)
        {
            return BadRequest();
        }
        return Ok();
    }
}