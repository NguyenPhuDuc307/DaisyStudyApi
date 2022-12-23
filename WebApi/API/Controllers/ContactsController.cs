using Application.Contacts;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ContactRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _contactService.Create(request);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(int Id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _contactService.GetById(Id);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _contactService.GetAll();
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        var result = await _contactService.Delete(Id);
        return Ok(result);
    }

    [HttpPost("{Id}")]
    public async Task<IActionResult> Reply(int Id, [FromForm] string ReplyMessage)
    {
        var result = await _contactService.Reply(Id, ReplyMessage);
        return Ok(result);
    }
}