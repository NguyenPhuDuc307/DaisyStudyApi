using Data.Entities;
using Models;

namespace Application.News;

public interface INewService
{
    Task<ApiResult<int>> Create(NewRequest request);
    Task<ApiResult<int>> Delete(int Id);
    Task<ApiResult<IEnumerable<New>>> GetAll();
}