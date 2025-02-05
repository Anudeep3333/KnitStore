using Ecommerce.Models;
using BCrypt.Net;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class UserService : IUserService
    {
        private readonly EcommerceDbContext _context;  // Replace with your actual DbContext

        public UserService(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUser(RegisterRequest registerRequest)
        {
            // Validate passwords
            if (registerRequest.Password != registerRequest.ConfirmPassword)
                throw new ArgumentException("Passwords do not match.");

            // Check for duplicate email or username
            if (await _context.Users.AnyAsync(u => u.Username == registerRequest.Username))
                throw new ArgumentException("Username is already taken.");
            if (await _context.Users.AnyAsync(u => u.Email == registerRequest.Email))
                throw new ArgumentException("Email is already taken.");

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            // Create the user object
            var newUser = new User
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                PasswordHash = hashedPassword,
                DateCreated = DateTime.UtcNow,
                //Role = "Customer"
            };

            // Save to database
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> GetUser(int userId)
        {
            var usr = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return usr;
        }
        public async Task<User> LoginUser(LoginRequest loginRequest)
        {
            if (!await _context.Users.AnyAsync(u => u.Email == loginRequest.Email))
                throw new ArgumentException("No user found on this email.Please try again.");
            
            var usr = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginRequest.Email);
            if (usr != null && BCrypt.Net.BCrypt.Verify(loginRequest.Password,usr.PasswordHash))
            {
                return usr;
            }
            else
            {
                throw new ArgumentException("Email or Password is incorrect.");
            }
        }

    }
}