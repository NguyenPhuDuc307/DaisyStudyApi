using Data.Entities;
using Models;

namespace Application.Users
{
    public interface IUserService
    {
        Task<int> Register (RegisterRequest request);
        Task<User?> Login (LoginRequest request);
        Task<User?> GetByEmail (string Email);
        Task<User?> GetById (Guid Id);
    }
}