using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Fluxivo.Infrastructure.Models;

namespace Fluxivo.Infrastructure.Services
{
    public class UserService
    {
        private readonly FluxivoDbContext _context;
        private readonly IPasswordHasher<string> _hasher = new PasswordHasher<string>();

        public UserService(FluxivoDbContext context) => _context = context;

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
                return null;

            var result = _hasher.VerifyHashedPassword(username, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                return user;
            }
            return null;
        }

        public string HashPassword(string username, string password) =>
            _hasher.HashPassword(username, password);

        // New registration method
        public async Task<User?> RegisterUserAsync(string username, string email, string password)
        {
            // Check if the user already exists by username or email
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (existingUser != null)
                return null;

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = username,
                Email = email, 
                PasswordHash = HashPassword(username, password)
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }


    }
}