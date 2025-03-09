using MyApi.Domain.Entities;

namespace MyApi.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetUsersAsync();
        Task<UserEntity?> GetUserByIdAsync(int id);
        Task<UserEntity?> GetUserByEmailAsync(string email);
        Task AddUserDefaultAsync(UserEntity user);
        Task UpdateUserAsync(UserEntity user);
        Task DeleteUserAsync(int id);
    }
}
