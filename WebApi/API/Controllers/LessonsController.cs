using Application.Lessons;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonsController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] LessonRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _lessonService.Create(request);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(int Id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _lessonService.GetById(Id);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _lessonService.GetAll();
        return Ok(result);
    }

    [HttpGet("getAll/{Id}")]
    public async Task<IActionResult> GetAllByCourseId(int Id)
    {
        var result = await _lessonService.GetAllByCourseId(Id);
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        var result = await _lessonService.Delete(Id);
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(int Id, [FromForm]LessonRequest request)
    {
        var result = await _lessonService.Update(Id, request);
        return Ok(result);
    }
}