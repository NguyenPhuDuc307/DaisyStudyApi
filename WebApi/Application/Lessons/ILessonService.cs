using Models;

namespace Application.Lessons
{
    public interface ILessonService
    {
        Task<ApiResult<int>> Create(LessonRequest request);
        Task<ApiResult<int>> Update(int Id, LessonRequest request);
        Task<ApiResult<int>> Delete(int Id);
        Task<ApiResult<IEnumerable<LessonViewModel>>> GetAll();
        Task<ApiResult<IEnumerable<LessonViewModel>>> GetAllByCourseId(int Id);
        Task<ApiResult<LessonViewModel>> GetById(int Id);
    }
}