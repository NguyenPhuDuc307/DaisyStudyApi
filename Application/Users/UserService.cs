using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models;
using Utils;

namespace Application.Users
{
    public class UserService : IUserService
    {
        private readonly DaisyStudyDbContext _context;

        public UserService(DaisyStudyDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmail(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
        }

        public async Task<User?> GetById(Guid Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<User?> Login(LoginRequest request)
        {
            if(request.Password == null) throw new Exception("Vui lòng nhập Password");
            var user = await _context.Users.FindAsync(request.Email);
            if (user == null) throw new Exception("Tài khoản không tồn tại");
            if (MD5Encrypt.Encrypt(request.Password) == user.Password) return user;
            else throw new Exception("Mật khẩu không chính xác");

        }

        public async Task<int> Register(RegisterRequest request)
        {
            var _user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (_user != null) throw new Exception("Địa chỉ email đã tồn tại");
            if(request.Password == null) throw new Exception("Vui lòng nhập Password");
            User user = new User()
            {
                FullName = request.FullName,
                Dob = request.Dob,
                Email = request.Email,
                Password = MD5Encrypt.Encrypt(request.Password)
            };
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync();
        }
    }
}