using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models;
using Utils;
using Utils.Helpers;

namespace Application.Users;

public class UserService : IUserService
{
    private readonly DaisyStudyDbContext _context;

    public UserService(DaisyStudyDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<int>> ConfirmEmail(string? Email, int Code)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
        if (user == null) return new ApiErrorResult<int>("Tài khoản không tồn tại");
        if (user.Code == Code)
        {
            user.EmailConfirm = true;
            var result = await _context.SaveChangesAsync();
            return new ApiSuccessResult<int>(result);
        }
        return new ApiErrorResult<int>("Mã xác nhận không đúng");
    }

    public async Task<ApiResult<int>> Delete(string? Email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
        if (user == null) return new ApiErrorResult<int>("Tài khoản không tồn tại");

        _context.Users.Remove(user);
        var result = await _context.SaveChangesAsync();
        return new ApiSuccessResult<int>(result);
    }

    public async Task<ApiResult<IEnumerable<UserViewModel>>> GetAll()
    {
        var userViewModels = await _context.Users.Select(x => new UserViewModel()
        {
            UserId = x.UserId,
            FullName = x.FullName,
            Email = x.Email,
            Dob = x.Dob
        }).ToListAsync();
        return new ApiSuccessResult<IEnumerable<UserViewModel>>(userViewModels);
    }

    public async Task<ApiResult<UserViewModel>> GetByEmail(string? Email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
        if (user == null) throw new Exception("Tài khoản không tồn tại");
        var userViewModel = new UserViewModel()
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Email = user.Email,
            Dob = user.Dob
        };
        return new ApiSuccessResult<UserViewModel>(userViewModel);
    }

    public async Task<ApiResult<UserViewModel>> GetById(int Id)
    {
        var user = await _context.Users.FindAsync(Id);
        if (user == null) throw new Exception("Tài khoản không tồn tại");
        var userViewModel = new UserViewModel()
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Email = user.Email,
            Dob = user.Dob
        };
        return new ApiSuccessResult<UserViewModel>(userViewModel);
    }

    public async Task<ApiResult<UserViewModel>> Login(LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Password) || string.IsNullOrWhiteSpace(request.Password)) return new ApiErrorResult<UserViewModel>("Thông tin đăng nhập không được bỏ trống");
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user == null) return new ApiErrorResult<UserViewModel>("Tài khoản không tồn tại");
        if (MD5Encrypt.Encrypt(request.Password) == user.Password)
        {
            var userViewModel = new UserViewModel()
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Dob = user.Dob
            };
            return new ApiSuccessResult<UserViewModel>(userViewModel);
        }
        else return new ApiErrorResult<UserViewModel>("Mật khẩu không chính xác");

    }

    public async Task<ApiResult<int>> Register(RegisterRequest request)
    {
        var _user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (_user != null) return new ApiErrorResult<int>("Địa chỉ email đã tồn tại");
        if (!Validate.IsNullOrEmptyOrWhiteSpace(request.FullName)) return new ApiErrorResult<int>("Vui lòng nhập họ tên");
        if (!Validate.IsNullOrEmptyOrWhiteSpace(request.Dob.ToString())) return new ApiErrorResult<int>("Vui lòng nhập ngày sinh");
        if (!Validate.IsNullOrEmptyOrWhiteSpace(request.Email)) return new ApiErrorResult<int>("Vui lòng nhập địa chỉ email");
        if (!Validate.IsNullOrEmptyOrWhiteSpace(request.Password)) return new ApiErrorResult<int>("Vui lòng nhập mật khẩu");
        if (!Validate.IsNullOrEmptyOrWhiteSpace(request.PasswordCF)) return new ApiErrorResult<int>("Vui lòng nhập mật khẩu xác nhận");

        if (request.Password != request.PasswordCF) return new ApiErrorResult<int>("Mật khẩu xác nhận không trùng khớp");
        if (request.Password == null) return new ApiErrorResult<int>("Vui lòng nhập mật khẩu");
        if (request.Email == null) return new ApiErrorResult<int>("Vui lòng nhập địa chỉ email");

        String Code = SystemVariable.GetRanDomCodeInt(6);
        User user = new User()
        {
            FullName = request.FullName,
            Dob = request.Dob,
            Email = request.Email,
            Password = MD5Encrypt.Encrypt(request.Password),
            Code = Int32.Parse(Code),
            EmailConfirm = false
        };
        await _context.Users.AddAsync(user);
        var result = await _context.SaveChangesAsync();
        SendMail.SendEmail(request.Email, "Daisy Study",
            $"<!DOCTYPE html>\n" +
            $"<html lang=\"en\">\n" +
            $"<head>\n" +
            $"<meta charset=\"utf-8\" />\n" +
            $"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />\n" +
            $"<link\n" +
            $"href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css\"\n" +
            $"rel=\"stylesheet\"\n      " +
            $"integrity=\"sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65\"\n" +
            $"crossorigin=\"anonymous\"\n/>\n" +
            $"</head>\n" +
            $"<body class=\"container\">\n\n" +
            $"<h1>Xác nhận tài khoản</h1>\n" +
            $"<p>Bạn vừa gửi yêu cầu đăng ký tài khoản.</p>\n" +
            $"<p>Bạn vui lòng xác nhận tài khoản trước khi tiến hành đăng nhập.</p>\n" +
            $"<b class=\"text-danger\">Code: {Code}</b>\n" +
            $"</body>\n" +
            $"</html>\n", "");
        return new ApiSuccessResult<int>(result);
    }
}