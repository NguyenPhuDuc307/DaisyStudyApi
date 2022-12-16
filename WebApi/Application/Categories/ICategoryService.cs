using Models;

namespace Application.Categories;

public interface ICategoryService
{
    Task<ApiResult<int>> Create(CategoryRequest request);
    Task<ApiResult<int>> Update(int Id, CategoryRequest request);
    Task<ApiResult<int>> Delete(int Id);
    Task<ApiResult<IEnumerable<CategoryViewModel>>> GetAll();
    Task<ApiResult<CategoryViewModel>> GetById(int Id);
}