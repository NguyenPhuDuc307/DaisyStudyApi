using Application.News;
using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Categories;

public class NewService : INewService
{

    private readonly DaisyStudyDbContext _context;

    public NewService(DaisyStudyDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<int>> Create(NewRequest request)
    {
        if (string.IsNullOrEmpty(request.Title) || string.IsNullOrWhiteSpace(request.Title)) return new ApiErrorResult<int>("Vui lòng nhập tiêu đề");
        if (string.IsNullOrEmpty(request.Image) || string.IsNullOrWhiteSpace(request.Image)) return new ApiErrorResult<int>("Vui lòng nhập link hình ảnh");
        if (string.IsNullOrEmpty(request.NewUrl) || string.IsNullOrWhiteSpace(request.NewUrl)) return new ApiErrorResult<int>("Vui lòng nhập link bài viết");

        var _new = new New()
        {
            Title = request.Title,
            Image = request.Image,
            NewUrl = request.NewUrl
        };
        await _context.News.AddAsync(_new);
        var result = await _context.SaveChangesAsync();
        return new ApiSuccessResult<int>(result);
    }

    public async Task<ApiResult<int>> Delete(int Id)
    {
        var _new = await _context.News.FindAsync(Id);
        if (_new == null) return new ApiErrorResult<int>("Tin tức không tồn tại");

        _context.Remove(_new);
        var result = await _context.SaveChangesAsync();
        return new ApiSuccessResult<int>(result);
    }

    public async Task<ApiResult<IEnumerable<New>>> GetAll()
    {
        var news = await _context.News.ToListAsync();
        return new ApiSuccessResult<IEnumerable<New>>(news);
    }
}