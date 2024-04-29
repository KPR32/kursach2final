using FurnitureStore3.Domain.Entities;

namespace FurnitureStore3.Domain.Services
{
    public interface IUserService
    {
        Task<bool> IsUserExistsAsync(string username);
        Task<User> RegistrationAsync(string fullname, string username, string phone, string password);
        Task<User?> GetUserAsync(string username, string password);
    }
}
