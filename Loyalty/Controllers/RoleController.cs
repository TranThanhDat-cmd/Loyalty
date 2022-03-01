using AutoMapper;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests.Role;
using Loyalty.Models.Dtos.Responses.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IMapper _mapper;

        private RoleManager<Role> _roleManager;

        public RoleController(IMapper mapper, RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = _roleManager.Roles.ToList();
            var roleReponse = _mapper.Map<List<RoleReponse>>(roles);

            return Ok(roleReponse);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRoleRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var role = _mapper.Map<Role>(req);
            role.Id = Guid.NewGuid();

            var isCreated = await _roleManager.CreateAsync(role);


            if (isCreated.Succeeded)
            {
                var roleReponse = _mapper.Map<RoleReponse>(role);
                return Ok(new AddRoleReponse
                {
                    Data = roleReponse,
                    Success = true
                });
            }
            return BadRequest("Something went wrong");

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            var isDeleted = await _roleManager.DeleteAsync(role);
            if (isDeleted.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            var roleReponse = _mapper.Map<RoleReponse>(role);
            return Ok(roleReponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var role = await _roleManager.FindByIdAsync(req.Id.ToString());
            _mapper.Map<UpdateRoleRequest, Role>(req, role);

            var isUpdated = await _roleManager.UpdateAsync(role);


            if (isUpdated.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Something went wrong");
        }
    }
}
