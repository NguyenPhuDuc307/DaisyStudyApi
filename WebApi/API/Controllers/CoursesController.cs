using Application.Courses;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService CourseService)
    {
        _courseService = CourseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CourseRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _courseService.Create(request);
        return Ok(result);
    }

    [HttpPost("join/{CourseId}/{UserId}")]
    public async Task<IActionResult> Join(int CourseId, int UserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _courseService.JoinCourse(CourseId, UserId);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(int Id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _courseService.GetById(Id);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _courseService.GetAll();
        return Ok(result);
    }

    [HttpGet("getAllMembers/{Id}")]
    public async Task<IActionResult> GetAllMembers(int Id)
    {
        var result = await _courseService.GetAllMembers(Id);
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        var result = await _courseService.Delete(Id);
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(int Id, [FromForm]CourseRequest request)
    {
        var result = await _courseService.Update(Id, request);
        return Ok(result);
    }
}