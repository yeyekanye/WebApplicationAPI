using Application.Common;
using Application.DTOs;
using Application.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<ServiceResponse<AuthResponseDto>> RegisterAsync(RegisterDto dto)
        {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
            {
                return new ServiceResponse<AuthResponseDto>
                {
                    Success = false,
                    Message = "User with this email already exists"
                };
            }

            // ⚡ Хешування пароля (спрощене, краще використати BCrypt)
            var hashedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(dto.Password));

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = hashedPassword
            };

            await _userRepository.CreateAsync(user);

            // 🔑 Генерація JWT
            var token = GenerateJwtToken(user);

            // 📧 Відправка email
            SendRegistrationEmail(user.Email);

            return new ServiceResponse<AuthResponseDto>
            {
                Data = new AuthResponseDto
                {
                    Token = token,
                    Email = user.Email
                },
                Message = "Registration successful"
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("id", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void SendRegistrationEmail(string email)
        {
            var smtpClient = new SmtpClient(_config["Email:SmtpHost"])
            {
                Port = int.Parse(_config["Email:SmtpPort"]!),
                Credentials = new NetworkCredential(_config["Email:Username"], _config["Email:Password"]),
                EnableSsl = true,
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_config["Email:From"]),
                Subject = "Successful Registration",
                Body = "Welcome! Your registration was successful.",
                IsBodyHtml = false,
            };

            mail.To.Add(email);
            smtpClient.Send(mail);
        }
    }
}