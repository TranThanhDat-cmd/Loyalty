using AutoMapper;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests;
using Loyalty.Models.Dtos.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
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
            var isAddRole = await _userManager.AddToRoleAsync(newUser, req.RoleName);
            if (isCreated.Succeeded)
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = _userManager.Users.ToList();
            var usersReponse = _mapper.Map<List<GetUserReponse>>(users);
            return Ok(usersReponse);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var usersReponse = _mapper.Map<GetUserReponse>(user);
            return Ok(usersReponse);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var isDeleted = await _userManager.DeleteAsync(user);
            if (isDeleted.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleUserRequest req)
        {
            var user = await _userManager.FindByIdAsync(req.UserId.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var isRemoved = await _userManager.RemoveFromRoleAsync(user, req.OldRoleName);
            var isUpdated = await _userManager.AddToRoleAsync(user, req.NewRoleName);

            if (isRemoved.Succeeded && isUpdated.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");

        }

    }
}
