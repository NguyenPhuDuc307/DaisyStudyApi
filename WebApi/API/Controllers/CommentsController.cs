using Application.Comments;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _CommentService;

    public CommentsController(ICommentService CommentService)
    {
        _CommentService = CommentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CommentCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _CommentService.Create(request);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(int Id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _CommentService.GetById(Id);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _CommentService.GetAll();
        return Ok(result);
    }

    [HttpGet("getAll/{Id}")]
    public async Task<IActionResult> GetAllByLessonId(int Id)
    {
        var result = await _CommentService.GetAllByLessonId(Id);
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        var result = await _CommentService.Delete(Id);
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Edit(int Id, [FromForm]CommentUpdateRequest request)
    {
        var result = await _CommentService.Edit(Id, request);
        return Ok(result);
    }
}