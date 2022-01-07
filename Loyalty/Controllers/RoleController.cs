using AutoMapper;
using Loyalty.Core.IRepositories;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests;
using Loyalty.Models.Dtos.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IMapper _mapper;
        private IRoleRepository _repository;

        public RoleController(IMapper mapper, IRoleRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _repository.GetAll();
            var roleReponse = _mapper.Map<List<GetRoleReponse>>(roles);

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
            var isAdded = await _repository.Add(role);
            if (isAdded)
            {
                var roleReponse = _mapper.Map<GetRoleReponse>(role);
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
            var isDeleted = await _repository.Detele(id);
            if (isDeleted)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var role = await _repository.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var role = _mapper.Map<Role>(req);
            var isUpdated = await _repository.Update(role);
            if (isUpdated)
            {
                return NoContent();
            }
            return BadRequest("Something went wrong");
        }
    }
}
