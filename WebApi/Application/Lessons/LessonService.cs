using Application.FileStorages;
using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Lessons
{
    public class LessonService : ILessonService
    {
        private readonly DaisyStudyDbContext _context;
        private readonly IFileStorageService _fileService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public LessonService(DaisyStudyDbContext context, IFileStorageService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<ApiResult<int>> Create(LessonRequest request)
        {
            if (string.IsNullOrEmpty(request.LessonName) || string.IsNullOrWhiteSpace(request.LessonName)) return new ApiErrorResult<int>("Vui lòng nhập tên bài học");
            if (request.ImageFile == null) return new ApiErrorResult<int>("Vui lòng thêm hình ảnh");

            var course = await _context.Courses.FindAsync(request.CourseId);
            if (course == null) return new ApiErrorResult<int>("Khoá học không tồn tại");
            Lesson Lesson = new Lesson()
            {
                CourseId = request.CourseId,
                LessonName = request.LessonName,
                Content = request.Content,
                DateCreated = DateTime.Now
            };
            if (request.ImageFile != null)
            {
                Lesson.ImagePath = await _fileService.SaveFile(request.ImageFile);
            }
            await _context.Lessons.AddAsync(Lesson);
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }

        public async Task<ApiResult<int>> Delete(int Id)
        {
            var lesson = await _context.Lessons.FindAsync(Id);
            if (lesson == null) return new ApiErrorResult<int>("Khoá học không tồn tại");
            if (!string.IsNullOrEmpty(lesson.ImagePath))
                await _fileService.DeleteFileAsync(lesson.ImagePath.Replace("/user-content/", ""));
            _context.Lessons.Remove(lesson);
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }

        public async Task<ApiResult<IEnumerable<LessonViewModel>>> GetAll()
        {
            var lessonViewModels = await _context.Lessons.Select(x => new LessonViewModel()
            {
                LessonId = x.LessonId,
                CourseId = x.CourseId,
                CourseName = x.Course != null ? x.Course.CourseName : null,
                LessonName = x.LessonName,
                DateTimeCreated = x.DateCreated,
                ImagePath = x.ImagePath
            }).ToListAsync();
            return new ApiSuccessResult<IEnumerable<LessonViewModel>>(lessonViewModels);
        }

        public async Task<ApiResult<IEnumerable<LessonViewModel>>> GetAllByCourseId(int Id)
        {
            var lessonViewModels = await _context.Lessons.Where(x=> x.CourseId == Id).Select(x => new LessonViewModel()
            {
                LessonId = x.LessonId,
                CourseId = x.CourseId,
                CourseName = x.Course != null ? x.Course.CourseName : null,
                LessonName = x.LessonName,
                DateTimeCreated = x.DateCreated,
                ImagePath = x.ImagePath
            }).ToListAsync();
            return new ApiSuccessResult<IEnumerable<LessonViewModel>>(lessonViewModels);
        }

        public async Task<ApiResult<LessonViewModel>> GetById(int Id)
        {
            var lesson = await _context.Lessons.Include(x=> x.Course).FirstOrDefaultAsync(x=> x.LessonId ==Id);
            if (lesson == null) return new ApiErrorResult<LessonViewModel>("Khoá học không tồn tại");
            var lessonViewModels = new LessonViewModel()
            {
                LessonId = lesson.LessonId,
                CourseId = lesson.CourseId,
                CourseName = lesson.Course != null ? lesson.Course.CourseName : null,
                LessonName = lesson.LessonName,
                Content = lesson.Content,
                DateTimeCreated = lesson.DateCreated,
                ImagePath = lesson.ImagePath
            };
            return new ApiSuccessResult<LessonViewModel>(lessonViewModels);
        }

        public async Task<ApiResult<int>> Update(int Id, LessonRequest request)
        {
            var lesson = await _context.Lessons.FindAsync(Id);
            if (lesson == null) return new ApiErrorResult<int>("Khoá học không tồn tại");
            if (string.IsNullOrEmpty(request.LessonName) || string.IsNullOrWhiteSpace(request.LessonName)) return new ApiErrorResult<int>("Vui lòng nhập tên bài học");

            lesson.CourseId = request.CourseId;
            lesson.LessonName = request.LessonName;
            lesson.Content = request.Content;
            if (request.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(lesson.ImagePath))
                await _fileService.DeleteFileAsync(lesson.ImagePath.Replace("/user-content/", ""));
                lesson.ImagePath = await _fileService.SaveFile(request.ImageFile);
            }
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }
    }
}