using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Loyalty.Data.Entities;
using Loyalty.Configuration;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

using AutoMapper;
using Loyalty.Models.Dtos.Requests.Auth;

namespace Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly IMapper _mapper;

        public AuthManagementController(
            UserManager<User> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IMapper mapper)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid payload"
                    },
                    Success = false
                });
            }

            var existingUser = await _userManager.FindByNameAsync(req.Username);
            if (existingUser == null)
            {
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid login request"
                    },
                    Success = false
                });
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, req.Password);
            if (!isCorrect)
            {
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid login request"
                    },
                    Success = false
                });


            }

            var jwtToken = GenerateJwtToken(existingUser);
            return Ok(new AuthResult()
            {
                Success = true,
                Token = jwtToken
            });


        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid payload"
                    },
                    Success = false
                });
            }
            var existingEmail = await _userManager.FindByEmailAsync(req.Email);
            if (existingEmail != null)
            {
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Existing Email"
                    },
                    Success = false
                });
            }

            var newUser = _mapper.Map<User>(req);

            var isCreated = await _userManager.CreateAsync(newUser, req.Password);

            if (isCreated.Succeeded)
            {
                return Ok(new AuthResult()
                {
                    Success = true,
                    Token = GenerateJwtToken(newUser)
                });
            }

            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                    {
                        "Existing Email"
                    },
                Success = false
            });
        }
        private string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                // user infor
                Subject = new ClaimsIdentity(new[]
                {
                    //1 claim = 1 user infor 
                    new Claim("Id",user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                    // Role
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
