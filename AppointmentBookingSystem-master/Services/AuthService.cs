using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingSystem.Models;
using AppointmentBookingSystem.Models.ViewModels;
using AppointmentBookingSystem.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AppointmentBookingSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(RegisterViewModel model)
        {

            var existingUser = await _userRepository.GetUserByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return "User already exists";
            }

            var passwordHasher = new PasswordHasher<User>();
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = model.Email,
                PasswordHash = passwordHasher.HashPassword(null, model.Password)
            };

            await _userRepository.CreateUserAsync(user);
            return "Registration successful";
        }

        public async Task<string> LoginAsync(LoginViewModel model)
        {
            var user = await _userRepository.GetUserByEmailAsync(model.Email);
            if (user == null)
            {
                return "Invalid login attempt";
            }

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return "Invalid login attempt";
            }

            var token = GenerateJwtToken(user);
            return token;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
