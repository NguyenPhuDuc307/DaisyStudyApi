using Models;
namespace Application.Ratings
{
    public interface IRatingService
    {
        Task<ApiResult<int>> CreateOrUpdate(RatingRequest request);
        Task<ApiResult<int>> Delete(int CourseId, int UserId);
        Task<ApiResult<IEnumerable<RatingViewModel>>> GetAll();
        Task<ApiResult<IEnumerable<RatingViewModel>>> GetAllByCourseId(int Id);
        Task<ApiResult<RatingViewModel>> GetById(int CourseId, int UserId);
    }
}