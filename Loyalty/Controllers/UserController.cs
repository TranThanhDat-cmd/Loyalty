using AutoMapper;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests;
using Loyalty.Models.Dtos.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserController(
            UserManager<User> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CreateUserReponse()
                {
                    Message = "Invalid payload",
                    Success = false
                });
            }

            var existingUser = await _userManager.FindByNameAsync(req.Username);
            if (existingUser != null)
            {
                return BadRequest(new CreateUserReponse()
                {
                    Message = "Username already exists",
                    Success = false
                });
            }
            var newUser = _mapper.Map<User>(req);
            var isCreated = await _userManager.CreateAsync(newUser, req.Password);
            var isAddRole = await _userManager.AddToRolesAsync(newUser, req.RoleNames);
            if (isCreated.Succeeded && isAddRole.Succeeded)
            {
                return Ok(new CreateUserReponse()
                {
                    Message = "Create Success",
                    Success = true
                });
            }
            return BadRequest(new CreateUserReponse()
            {
                Message = "invalid password",
                Success = false
            });

        }

    }
}
