using Models;

namespace Application.Courses
{
    public interface ICourseService
    {
        Task<ApiResult<int>> Create(CourseRequest request);
        Task<ApiResult<int>> JoinCourse(int CourseId, int UserId);
        Task<ApiResult<int>> Update(int Id, CourseRequest request);
        Task<ApiResult<int>> Delete(int Id);
        Task<ApiResult<IEnumerable<CourseViewModel>>> GetAll();
        Task<ApiResult<IEnumerable<MemberViewModel>>> GetAllMembers(int Id);
        Task<ApiResult<CourseViewModel>> GetById(int Id);
    }
}