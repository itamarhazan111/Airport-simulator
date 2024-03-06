using UserLogin.Models;

namespace UserLogin.Data
{
    public interface IUserRepository
    {
        
        Task <User> CreateAsync(User user);
        Task<User?> FindByEmailAsync(string email);
        Task<User?> FindByIdAsync(int id);
        Task UpdateAsync(User user);
        Task UpdatePassworrd(string email, string password);
        Task<string?>FindCodeForPassword(string email);
        Task<bool> SendCodeForPassword(string? code, string email);

    }
}
