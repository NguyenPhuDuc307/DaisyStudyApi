using Application.News;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly INewService _NewService;

    public NewsController(INewService NewService)
    {
        _NewService = NewService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] NewRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _NewService.Create(request);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _NewService.GetAll();
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        var result = await _NewService.Delete(Id);
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(int Id, [FromForm] NewRequest request)
    {
        var result = await _NewService.Update(Id, request);
        return Ok(result);
    }
}