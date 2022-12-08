using Data.Entities;
using Models;

namespace Application.Users
{
    public interface IUserService
    {
        Task<int> Register(RegisterRequest request);
        Task<UserViewModel?> Login(LoginRequest request);
        Task<IEnumerable<UserViewModel>> GetAll();
        Task<UserViewModel?> GetByEmail(string Email);
        Task<UserViewModel?> GetById(int Id);
    }
}