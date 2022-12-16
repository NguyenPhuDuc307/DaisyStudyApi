using System.Security.Cryptography.X509Certificates;
using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Ratings;

public class RatingService : IRatingService
{
    private readonly DaisyStudyDbContext _context;

    public RatingService(DaisyStudyDbContext context)
    {
        _context = context;
    }
    public async Task<ApiResult<int>> CreateOrUpdate(RatingRequest request)
    {
        if (string.IsNullOrEmpty(request.Message) || string.IsNullOrWhiteSpace(request.Message)) return new ApiErrorResult<int>("Vui lòng nhập nội dung");
        if (request.Stars < 0 || request.Stars > 5) return new ApiErrorResult<int>("Điểm đánh giá không hợp lệ (0-5)");

        var course = await _context.Courses.FindAsync(request.CourseId);
        if (course == null) return new ApiErrorResult<int>("Khoá học không tồn tại");
        var user = await _context.Users.FindAsync(request.UserId);
        if (user == null) return new ApiErrorResult<int>("Người dùng không tồn tại");

        var rating = await _context.Ratings.FirstOrDefaultAsync(x => x.CourseId == request.CourseId && x.UserId == request.UserId);
        if (rating != null)
        {
            rating.Message = request.Message;
            rating.Stars = request.Stars;
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }
        else
        {
            Rating newRating = new Rating()
            {
                UserId = request.UserId,
                CourseId = request.CourseId,
                Stars = request.Stars,
                Message = request.Message,
                DateCreated = DateTime.Now
            };

            await _context.Ratings.AddAsync(newRating);
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }
    }

    public async Task<ApiResult<int>> Delete(int CourseId, int UserId)
    {
        var rating = await _context.Ratings.FirstOrDefaultAsync(x => x.CourseId == CourseId && x.UserId == UserId);
        if (rating == null) return new ApiErrorResult<int>("Đánh giá không tồn tại");

        _context.Ratings.Remove(rating);
        var result = await _context.SaveChangesAsync();
        return new ApiSuccessResult<int>(result);
    }

    public async Task<ApiResult<IEnumerable<RatingViewModel>>> GetAll()
    {
        var ratingViewModels = await _context.Ratings.Select(x => new RatingViewModel()
        {
            CourseId = x.CourseId,
            CourseName = x.Course != null ? x.Course.CourseName : null,
            UserId = x.UserId,
            FullName = x.User != null ? x.User.FullName : null,
            Email = x.User != null ? x.User.Email : null,
            Stars = x.Stars,
            Message = x.Message,
            DateTimeCreated = x.DateCreated

        }).ToListAsync();
        return new ApiSuccessResult<IEnumerable<RatingViewModel>>(ratingViewModels);
    }

    public async Task<ApiResult<IEnumerable<RatingViewModel>>> GetAllByCourseId(int Id)
    {
        var ratingViewModels = await _context.Ratings.Where(x => x.CourseId == Id).Select(x => new RatingViewModel()
        {
            CourseId = x.CourseId,
            CourseName = x.Course != null ? x.Course.CourseName : null,
            UserId = x.UserId,
            FullName = x.User != null ? x.User.FullName : null,
            Email = x.User != null ? x.User.Email : null,
            Stars = x.Stars,
            Message = x.Message,
            DateTimeCreated = x.DateCreated

        }).ToListAsync();
        return new ApiSuccessResult<IEnumerable<RatingViewModel>>(ratingViewModels);
    }

    public async Task<ApiResult<RatingViewModel>> GetById(int CourseId, int UserId)
    {
        var rating = await _context.Ratings.Include(x => x.User).Include(x => x.Course).FirstOrDefaultAsync(x => x.CourseId == CourseId && x.UserId == UserId);
        if (rating == null) return new ApiErrorResult<RatingViewModel>("Đánh giá không tồn tại");
        var ratingViewModel = new RatingViewModel()
        {
            CourseId = rating.CourseId,
            CourseName = rating.Course != null ? rating.Course.CourseName : null,
            UserId = rating.UserId,
            FullName = rating.User != null ? rating.User.FullName : null,
            Email = rating.User != null ? rating.User.Email : null,
            Stars = rating.Stars,
            Message = rating.Message,
            DateTimeCreated = rating.DateCreated
        };
        return new ApiSuccessResult<RatingViewModel>(ratingViewModel);
    }
}