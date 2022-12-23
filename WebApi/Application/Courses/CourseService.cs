using Application.FileStorages;
using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Courses
{
    public class CourseService : ICourseService
    {
        private readonly DaisyStudyDbContext _context;
        private readonly IFileStorageService _fileService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public CourseService(DaisyStudyDbContext context, IFileStorageService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<ApiResult<int>> Create(CourseRequest request)
        {
            if (string.IsNullOrEmpty(request.CourseName) || string.IsNullOrWhiteSpace(request.CourseName)) return new ApiErrorResult<int>("Vui lòng nhập tên khoá học");
            if (request.ImageFile == null) return new ApiErrorResult<int>("Vui lòng thêm hình ảnh");

            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category == null) return new ApiErrorResult<int>("Danh mục không tồn tại");
            Course course = new Course()
            {
                CategoryId = request.CategoryId,
                CourseName = request.CourseName,
                Description = request.Description,
                DateCreated = DateTime.Now
            };
            if (request.ImageFile != null)
            {
                course.ImagePath = await _fileService.SaveFile(request.ImageFile);
            }
            await _context.Courses.AddAsync(course);
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }

        public async Task<ApiResult<int>> Delete(int Id)
        {
            var course = await _context.Courses.FindAsync(Id);
            if (course == null) return new ApiErrorResult<int>("Danh mục không tồn tại");
            if (!string.IsNullOrEmpty(course.ImagePath))
                await _fileService.DeleteFileAsync(course.ImagePath.Replace("/user-content/", ""));
            _context.Courses.Remove(course);
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }

        public async Task<ApiResult<IEnumerable<CourseViewModel>>> GetAll()
        {
            var courseViewModels = await _context.Courses.Include(x => x.CourseMembers).Select(x => new CourseViewModel()
            {
                CourseId = x.CourseId,
                CategoryId = x.CategoryId,
                CategoryName = x.Category != null ? x.Category.CategoryName : null,
                CourseName = x.CourseName,
                Members = x.CourseMembers != null ? x.CourseMembers.Count() : 0,
                DateTimeCreated = x.DateCreated,
                ImagePath = x.ImagePath
            }).ToListAsync();
            return new ApiSuccessResult<IEnumerable<CourseViewModel>>(courseViewModels);
        }

        public async Task<ApiResult<IEnumerable<MemberViewModel>>> GetAllMembers(int Id)
        {
            var memberViewModels = await _context.CourseMembers.Where(x => x.CourseId == Id).Select(x => new MemberViewModel()
            {
                UserId = x.UserId,
                CourseId = x.CourseId,
                CourseName = x.Course != null ? x.Course.CourseName : null,
                FullName = x.User != null ? x.User.FullName : null,
                Email = x.User != null ? x.User.Email : null
            }).ToListAsync();
            return new ApiSuccessResult<IEnumerable<MemberViewModel>>(memberViewModels);
        }

        public async Task<ApiResult<CourseViewModel>> GetById(int Id)
        {
            var course = await _context.Courses.Include(x => x.Category).FirstOrDefaultAsync(x => x.CourseId == Id);
            if (course == null) return new ApiErrorResult<CourseViewModel>("Khoá học không tồn tại");
            var courseViewModels = new CourseViewModel()
            {
                CourseId = course.CourseId,
                CategoryId = course.CategoryId,
                CategoryName = course.Category != null ? course.Category.CategoryName : null,
                CourseName = course.CourseName,
                Members = course.CourseMembers != null ? course.CourseMembers.Count() : 0,
                Description = course.Description,
                DateTimeCreated = course.DateCreated,
                ImagePath = course.ImagePath
            };
            return new ApiSuccessResult<CourseViewModel>(courseViewModels);
        }

        public async Task<ApiResult<int>> Update(int Id, CourseRequest request)
        {
            var course = await _context.Courses.FindAsync(Id);
            if (course == null) return new ApiErrorResult<int>("Khoá học không tồn tại");
            if (string.IsNullOrEmpty(request.CourseName) || string.IsNullOrWhiteSpace(request.CourseName)) return new ApiErrorResult<int>("Vui lòng nhập tên khoá học");

            course.CategoryId = request.CategoryId;
            course.CourseName = request.CourseName;
            course.Description = request.Description;
            if (request.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(course.ImagePath))
                    await _fileService.DeleteFileAsync(course.ImagePath.Replace("/user-content/", ""));
                course.ImagePath = await _fileService.SaveFile(request.ImageFile);
            }
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }

        public async Task<ApiResult<int>> JoinCourse(int CourseId, int UserId)
        {
            if (UserId == 0) return new ApiErrorResult<int>("Vui lòng nhập mã khoá học");
            if (CourseId == 0) return new ApiErrorResult<int>("Vui lòng nhập mã người dùng");

            var user = await _context.Users.FindAsync(UserId);
            if (user == null) return new ApiErrorResult<int>("Người dùng không tồn tại");

            var course = await _context.Courses.FindAsync(CourseId);
            if (course == null) return new ApiErrorResult<int>("Khoá học không tồn tại");
            CourseMember courseMember = new CourseMember()
            {
                UserId = UserId,
                CourseId = CourseId
            };
            await _context.CourseMembers.AddAsync(courseMember);
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }
    }
}