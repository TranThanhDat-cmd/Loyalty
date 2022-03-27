using Loyalty.Core.IConffiguration;
using Loyalty.Data;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests.Category;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var category = await _unitOfWork.Categories.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryRequest req)
        {
            try
            {
                var category = new Category()
                {

                    Name = req.Name,
                    IsShowOnHome = false,
                    Status = req.Status,

                };
                await _unitOfWork.Categories.Add(category);
                await _unitOfWork.CompleteAsync();
                return StatusCode(StatusCodes.Status201Created, category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryRequest req)
        {

            var dbCategory = await _unitOfWork.Categories.Get(req.Id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            dbCategory.Name = req.Name;
            dbCategory.IsShowOnHome = false;
            dbCategory.Status = req.Status;

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var dbCategory = await _unitOfWork.Categories.Get(id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            _unitOfWork.Categories.Delete(dbCategory);
            await _unitOfWork.CompleteAsync();
            return StatusCode(StatusCodes.Status202Accepted);
        }
    }

}

