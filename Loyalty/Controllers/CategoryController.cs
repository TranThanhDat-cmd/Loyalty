using Loyalty.Data;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private MyDbContext _context;

        public CategoryController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listCategory = _context.Categories;
            return Ok(listCategory);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create(AddCategoryRequest req)
        {
            try
            {
                var category = new Category()
                {

                    Name = req.Name,
                    IsShowOnHome = false,
                    Status = req.Status,

                };
                _context.Add(category);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCategoryRequest req)
        {

            var dbCategory = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            dbCategory.Name = req.Name;
            dbCategory.IsShowOnHome = false;
            dbCategory.Status = req.Status;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var dbCategory = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(dbCategory);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status202Accepted);
        }
    }

}

