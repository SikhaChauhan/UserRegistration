using Microsoft.Extensions.Logging;
using RepositoryLayer.Interface;
using ModelLayer.DTO;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace RepositoryLayer
{
    public class UserRegistrationRL : IUserRegistrationRL
    {
        private readonly RegistrationAppContext _context;
        private readonly ILogger<UserRegistrationRL> _logger;

        public UserRegistrationRL(RegistrationAppContext context, ILogger<UserRegistrationRL> logger)
        {
            _context = context;
            _logger = logger;
        }

        public string GetHello(string name)
        {
            return $"{name}, Hello from Repository Layer";
        }

        public string GetUserRL()
        {
            try
            {
                var lastUser = _context.Users.OrderByDescending(u => u.UserId).FirstOrDefault();
                return lastUser != null ? lastUser.Email : "No users found.";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching user: {ex.Message}");
                return "Error retrieving user.";
            }
        }

        public bool RegisterUserRL(RegistrationDTO data)
        {
            try
            {
                _logger.LogInformation("Checking if user already exists...");
                var existingUser = _context.Users
                    .FirstOrDefault(u => u.Email == data.Email);

                if (existingUser != null)
                {
                    _logger.LogWarning($"User with email {data.Email} already exists.");
                    return false;
                }

                _logger.LogInformation("Creating new user...");
                UserEntity newUser = new UserEntity
                {
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(data.Password),
                    Phone = data.Phone,
                    UserName = data.UserName
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();
                _logger.LogInformation("User successfully registered.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error registering user: {ex.Message}");
                return false;
            }
        }

        public string LoginUserRL(LoginDTO Data)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == Data.Email);

            if(user == null || !BCrypt.Net.BCrypt.Verify(Data.Password, user.PasswordHash))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
            new Claim("UserId", user.UserId.ToString())
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDiscriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
