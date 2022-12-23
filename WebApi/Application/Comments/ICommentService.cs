using Models;

namespace Application.Comments
{
    public interface ICommentService
    {
        Task<ApiResult<int>> Create(CommentCreateRequest request);
        Task<ApiResult<int>> Edit(int Id, CommentUpdateRequest request);
        Task<ApiResult<int>> Delete(int Id);
        Task<ApiResult<IEnumerable<CommentViewModel>>> GetAll();
        Task<ApiResult<IEnumerable<CommentViewModel>>> GetAllByLessonId(int Id);
        Task<ApiResult<CommentViewModel>> GetById(int Id);
    }
}