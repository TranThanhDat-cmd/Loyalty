using AutoMapper;
using Loyalty.Core.IConffiguration;
using Loyalty.Data.Entities;
using Loyalty.Models.Dtos.Requests.Product;
using Loyalty.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public ProductController(IUnitOfWork unitOfWork,
            IMapper mapper,
            IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageService = storageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _unitOfWork.Products.GetAll();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _unitOfWork.Products.Get(id);
            return Ok(product);
        }
        //[HttpGet]

        //public IActionResult GetProductsByCategory(string categoryName)
        //{
        //    var products = _unitOfWork.Products.GetProductsByCategory(categoryName);
        //    return Ok(products);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest req)
        {

            var product = _mapper.Map<Product>(req);
            product.DateCreated = DateTime.UtcNow;

            product.ProductImages = new List<ProductImage>();
            foreach (var item in req.Images)
            {
                product.ProductImages.Add(new ProductImage()
                {
                    DateCreated = DateTime.UtcNow,
                    ImageName = await SaveFile(item),
                });
            }
            await _unitOfWork.Products.Add(product);
            await _unitOfWork.CompleteAsync();
            return StatusCode(StatusCodes.Status201Created, product);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateProductRequest req)
        {
            if (id != req.Id)
            {
                return BadRequest();
            }
            var product = await _unitOfWork.Products.Get(id);
            if (product == null) { return NotFound(); }
            _mapper.Map<UpdateProductRequest, Product>(req, product);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Products.Get(id);
            if (product == null) { return NotFound(); }
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.CompleteAsync();
            return Ok();

        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
