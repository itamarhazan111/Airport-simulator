using Microsoft.EntityFrameworkCore;
using UserLogin.Models;

namespace UserLogin.Data
{
    public class UserRepository : IUserRepository
    { 
        private readonly UserContext _context;
        public UserRepository(UserContext context) {
            _context = context;
        }
        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            user.Id = await _context.SaveChangesAsync();
            
            return user;  
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
              return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> FindByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<string?> FindCodeForPassword(string email)
        {
            var objFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (objFromDb != null)
            {
                return objFromDb.CodeForPassword;
            }
            return null;
        }

        public async Task<bool> SendCodeForPassword(string? code,string email)
        {
            var objFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (objFromDb != null)
            {
                objFromDb.CodeForPassword=code;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
           
        }

        public async Task UpdateAsync(User user)
        {
            var objFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if(objFromDb != null)
            {
                objFromDb.Email = user.Email;
                objFromDb.Name = user.Name;
                objFromDb.Password = user.Password;
            }
            await _context.SaveChangesAsync();
           
        }

        public async Task UpdatePassworrd(string email,string password)
        {
            var objFromDb = await FindByEmailAsync(email);
            if (objFromDb != null)
            {
                objFromDb.Password = password;
            }
            await _context.SaveChangesAsync();
        }
    }
}
