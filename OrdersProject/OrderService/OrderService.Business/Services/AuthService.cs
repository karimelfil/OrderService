using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OrderService.Business.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Model;
using OrderService.Domain.Model.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OrderService.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IAuthRepository authRepository,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest request)
        {
            Console.WriteLine($"Authentication attempt for user: {request.Username}");

            var login = await _authRepository.GetByUsername(request.Username);

            if (login == null)
            {
                Console.WriteLine("User not found in database");
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            Console.WriteLine($"User found. Active: {login.IsActive}, Deleted: {login.IsDeleted}");
            Console.WriteLine($"Stored salt: {login.Salt}");
            Console.WriteLine($"Stored hash: {login.Password}");

            if (!login.IsActive || login.IsDeleted)
            {
                Console.WriteLine("User is inactive or deleted");
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var computedHash = HashPassword(request.Password, login.Salt);
            Console.WriteLine($"Computed hash: {computedHash}");

            if (computedHash != login.Password)
            {
                Console.WriteLine("Password hashes don't match");
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            Console.WriteLine("Authentication successful, generating token...");
            var token = GenerateJwtToken(login.Username, login.Role);

            return new LoginResponse
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpiryInMinutes")),
                Role = login.Role,
                Username = login.Username
            };
        }

        public async Task<bool> ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GenerateJwtToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]); 

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
        new Claim(ClaimTypes.Name, username),
        
        new Claim(ClaimTypes.Role, role), 
        new Claim("role", role)            
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpiryInMinutes")),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public static (string Hash, string Salt) CreatePasswordHash(string password)
        {
            var saltBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            using var sha256 = SHA256.Create();
            var saltedPassword = Encoding.UTF8.GetBytes(password + salt); 
            var hashBytes = sha256.ComputeHash(saltedPassword);
            return (Convert.ToBase64String(hashBytes), salt);
        }

        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var saltedPassword = Encoding.UTF8.GetBytes(password + salt); 
            var hashBytes = sha256.ComputeHash(saltedPassword);
            return Convert.ToBase64String(hashBytes);
        }
        public async Task<RegisterResponse> Register(RegisterRequest request)
        {

            var existingUser = await _authRepository.GetByUsername(request.Username);
            if (existingUser != null)
            {
                throw new ArgumentException("Username already exists");
            }


            var (hash, salt) = CreatePasswordHash(request.Password);


            var newLogin = new Login
            {
                Username = request.Username.ToLower(),
                Password = hash,
                Salt = salt,
                Role = request.Role,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };


            await _authRepository.CreateAsync(newLogin);

            return new RegisterResponse
            {
                Username = newLogin.Username,
                Role = newLogin.Role,
                CreatedDate = newLogin.CreatedDate
            };
        }


    }
}
