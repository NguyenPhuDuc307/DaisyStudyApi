using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Categories;

public class CategoryService : ICategoryService
{

        private readonly DaisyStudyDbContext _context;

        public CategoryService(DaisyStudyDbContext context)
        {
            _context = context;
        }

    public async Task<ApiResult<int>> Create(CategoryRequest request)
    {
        if (string.IsNullOrEmpty(request.CategoryName) || string.IsNullOrWhiteSpace(request.CategoryName)) return new ApiErrorResult<int>("Vui lòng nhập tên danh mục");

        Category category = new Category()
        {
            CategoryName = request.CategoryName,
            Description = request.Description
        };
        await _context.Categories.AddAsync(category);
        var result = await _context.SaveChangesAsync();
        return new ApiSuccessResult<int>(result);
    }

    public async Task<ApiResult<int>> Delete(int Id)
    {
        var category = await _context.Categories.FindAsync(Id);
        if (category == null) return new ApiErrorResult<int>("Danh mục không tồn tại");

        _context.Remove(category);
        var result = await _context.SaveChangesAsync();
        return new ApiSuccessResult<int>(result);
    }

    public async Task<ApiResult<IEnumerable<CategoryViewModel>>> GetAll()
    {
        var categoryViewModels = await _context.Categories.Select(x => new CategoryViewModel()
        {
            CategoryId = x.CategoryId,
            CategoryName = x.CategoryName,
            Description = x.Description
        }).ToListAsync();
        return new ApiSuccessResult<IEnumerable<CategoryViewModel>>(categoryViewModels);
    }

    public async Task<ApiResult<CategoryViewModel>> GetById(int Id)
    {
        var category = await _context.Categories.FindAsync(Id);
        if (category == null) return new ApiErrorResult<CategoryViewModel>("Danh mục không tồn tại");
        var categoryViewModel = new CategoryViewModel()
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description
        };
        return new ApiSuccessResult<CategoryViewModel>(categoryViewModel);
    }

    public async Task<ApiResult<int>> Update(int Id, CategoryRequest request)
    {
        var category = await _context.Categories.FindAsync(Id);
        if (category == null) return new ApiErrorResult<int>("Danh mục không tồn tại");
        if (string.IsNullOrEmpty(request.CategoryName) || string.IsNullOrWhiteSpace(request.CategoryName)) return new ApiErrorResult<int>("Vui lòng nhập tên danh mục");

        category.CategoryName = request.CategoryName;
        category.Description = request.Description;
        var result = await _context.SaveChangesAsync();
        return new ApiSuccessResult<int>(result);
    }
}