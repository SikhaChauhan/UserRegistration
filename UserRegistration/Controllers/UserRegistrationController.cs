using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interface;
using ModelLayer.DTO;
using Microsoft.Extensions.Logging;

namespace UserRegistration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUserRergistrationBL _userRegistrationBL;
        private readonly ILogger<UserRegistrationController> _logger;

        public UserRegistrationController(IUserRergistrationBL userRegistrationBL, ILogger<UserRegistrationController> logger)
        {
            _userRegistrationBL = userRegistrationBL;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
                _logger.LogInformation("Fetching user data...");
                string user = _userRegistrationBL.GetUserBL();
                return Ok(new Response<string> { Success = true, Message = "User data retrieved", data = user });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting user: {ex.Message}");
                return StatusCode(500, new Response<string> { Success = false, Message = "Internal Server Error", data = null });
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser([FromBody] RegistrationDTO registration)
        {
            if (registration == null)
            {
                _logger.LogWarning("Registration request received with null data.");
                return BadRequest(new Response<string> { Success = false, Message = "Invalid request data", data = null });
            }

            try
            {
                _logger.LogInformation($"Registering user: {registration.Email}");
                bool registered = _userRegistrationBL.RegisterUserBL(registration);
                Console.WriteLine(registered);

                if (registered)
                {
                    _logger.LogInformation("User registered successfully.");
                    return Ok(new Response<string>
                    {
                        Success = true,
                        Message = "User added successfully",
                        data = registration.ToString()
                    });
                }

                _logger.LogWarning("User already exists.");
                return Conflict(new Response<string>
                {
                    Success = false,
                    Message = "User already exists",
                    data = "User is already registered"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while registering user: {ex.Message}");
                return StatusCode(500, new Response<string>
                {
                    Success = false,
                    Message = "Something went wrong, please try again",
                    data = null
                });
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser([FromBody] LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest(new Response<string> { Success = false, Message = "Invalid request data", data = null });
            }

            try
            {
                string token = _userRegistrationBL.LoginUserBL(login);
                if (token == null)
                {
                    return BadRequest(new Response<string> { Success = false, Message = "Invalid Credentials", data = null });
                }

                return Ok(new Response<string> { Success = true, Message = "Logged In Successfully", data = $"Welcome, {login.UserName}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<string> { Success = false, Message = ex.Message, data = null });
            }
        }
    }
}
