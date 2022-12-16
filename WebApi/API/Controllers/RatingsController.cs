using Application.Ratings;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RatingsController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingsController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] RatingRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _ratingService.CreateOrUpdate(request);
        return Ok(result);
    }

    [HttpGet("{CourseId}/{UserId}")]
    public async Task<IActionResult> GetById(int CourseId, int UserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _ratingService.GetById(CourseId, UserId);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _ratingService.GetAll();
        return Ok(result);
    }

    [HttpGet("getAll/{Id}")]
    public async Task<IActionResult> GetAllByCourseId(int Id)
    {
        var result = await _ratingService.GetAllByCourseId(Id);
        return Ok(result);
    }

    [HttpDelete("{CourseId}/{UserId}")]
    public async Task<IActionResult> Delete(int CourseId, int UserId)
    {
        var result = await _ratingService.Delete(CourseId, UserId);
        return Ok(result);
    }
}