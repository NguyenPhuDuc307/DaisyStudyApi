using Data.EF;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Comments
{
    public class CommentService : ICommentService
    {
        private readonly DaisyStudyDbContext _context;

        public CommentService(DaisyStudyDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<int>> Create(CommentCreateRequest request)
        {
            if (string.IsNullOrEmpty(request.Content) || string.IsNullOrWhiteSpace(request.Content)) return new ApiErrorResult<int>("Vui lòng nhập nội dung");

            var lesson = await _context.Lessons.FindAsync(request.LessonId);
            if (lesson == null) return new ApiErrorResult<int>("Bài học không tồn tại");
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null) return new ApiErrorResult<int>("Người dùng không tồn tại");

            Data.Entities.Comment comment = new Data.Entities.Comment()
            {
                UserId = request.UserId,
                LessonId = request.LessonId,
                Content = request.Content,
                DateTimeCreated = DateTime.Now
            };

            await _context.Comments.AddAsync(comment);
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }

        public async Task<ApiResult<int>> Delete(int Id)
        {
            var comment = await _context.Comments.FindAsync(Id);
            if (comment == null) return new ApiErrorResult<int>("Bình luận không tồn tại");

            _context.Comments.Remove(comment);
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }

        public async Task<ApiResult<IEnumerable<CommentViewModel>>> GetAll()
        {
            var commentViewModels = await _context.Comments.Select(x => new CommentViewModel()
            {
                CommentId = x.CommentId,
                LessonId = x.LessonId,
                LessonName = x.Lesson != null ? x.Lesson.LessonName : null,
                UserId = x.UserId,
                FullName = x.AppUser != null ? x.AppUser.FullName : null,
                Email = x.AppUser != null ? x.AppUser.Email : null,
                Content = x.Content,
                DateTimeCreated = x.DateTimeCreated
            }).ToListAsync();
            return new ApiSuccessResult<IEnumerable<CommentViewModel>>(commentViewModels);
        }

        public async Task<ApiResult<IEnumerable<CommentViewModel>>> GetAllByLessonId(int Id)
        {
            var commentViewModels = await _context.Comments.Where(x => x.LessonId == Id).Select(x => new CommentViewModel()
            {
                CommentId = x.CommentId,
                LessonId = x.LessonId,
                LessonName = x.Lesson != null ? x.Lesson.LessonName : null,
                UserId = x.UserId,
                FullName = x.AppUser != null ? x.AppUser.FullName : null,
                Email = x.AppUser != null ? x.AppUser.Email : null,
                Content = x.Content,
                DateTimeCreated = x.DateTimeCreated
            }).ToListAsync();
            return new ApiSuccessResult<IEnumerable<CommentViewModel>>(commentViewModels);
        }

        public async Task<ApiResult<CommentViewModel>> GetById(int Id)
        {
            var comment = await _context.Comments.Include(x => x.Lesson).Include(x=> x.AppUser).FirstOrDefaultAsync(x => x.CommentId == Id);
            if (comment == null) return new ApiErrorResult<CommentViewModel>("Bình luận không tồn tại");
            var commentViewModel = new CommentViewModel()
            {
                CommentId = comment.CommentId,
                LessonId = comment.LessonId,
                LessonName = comment.Lesson != null ? comment.Lesson.LessonName : null,
                UserId = comment.UserId,
                FullName = comment.AppUser != null ? comment.AppUser.FullName : null,
                Email = comment.AppUser != null ? comment.AppUser.Email : null,
                Content = comment.Content,
                DateTimeCreated = comment.DateTimeCreated
            };
            return new ApiSuccessResult<CommentViewModel>(commentViewModel);
        }

        public async Task<ApiResult<int>> Edit(int Id, CommentUpdateRequest request)
        {
            var comment = await _context.Comments.FindAsync(Id);
            if (comment == null) return new ApiErrorResult<int>("Bình luận không tồn tại");
            if (string.IsNullOrEmpty(request.Content) || string.IsNullOrWhiteSpace(request.Content)) return new ApiErrorResult<int>("Vui lòng nhập nội dung");

            comment.Content = request.Content;
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }
    }
}