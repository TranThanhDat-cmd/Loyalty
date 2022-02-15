using AutoMapper;
using Loyalty.Data;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests.User;
using Loyalty.Models.Dtos.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;


        public UserController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;

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

            var isRoleExist = await _roleManager.RoleExistsAsync(req.RoleName);
            if (isRoleExist)
            {
                var newUser = _mapper.Map<User>(req);
                var isCreated = await _userManager.CreateAsync(newUser, req.Password);
                var isAddRole = await _userManager.AddToRoleAsync(newUser, req.RoleName);
                if (isCreated.Succeeded && isAddRole.Succeeded)
                {
                    return Ok(new CreateUserReponse()
                    {
                        Message = "Create Success",
                        Success = true
                    });
                }
            }



            return BadRequest("Role Name Invalid");

        }


        [HttpPost]
        [Route("AddRoleUser")]
        public async Task<IActionResult> AddRoleUser(AddRoleUserRequest req)
        {
            if (ModelState.IsValid)
            {
                var isRoleExist = await _roleManager.RoleExistsAsync(req.RoleName);
                var user = await _userManager.FindByIdAsync(req.UserId.ToString());
                if (isRoleExist && user != null)
                {
                    var isAddRole = await _userManager.AddToRoleAsync(user, req.RoleName);
                    if (isAddRole.Succeeded)
                    {
                        return Ok();
                    }
                    return BadRequest("Something went wrong");
                }

                return NotFound("Role or user is not exist");


            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = _userManager.Users.ToList();
            var usersReponse = _mapper.Map<List<UserReponse>>(users);
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
            var usersReponse = _mapper.Map<UserReponse>(user);
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
