using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithDtoController : CustomBaseController
    {
        private readonly IProductServiceWithDto _productServiceWithDto;
        public ProductWithDtoController(IProductServiceWithDto productServiceWithDto)
        {
            _productServiceWithDto = productServiceWithDto;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreatActionResult(await _productServiceWithDto.GetProductsWitCategory());
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreatActionResult(await _productServiceWithDto.GetAllAsync());
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreatActionResult(await _productServiceWithDto.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto productDto)
        {
            return CreatActionResult(await _productServiceWithDto.AddAsync(productDto));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            return CreatActionResult(await _productServiceWithDto.UpdateAsync(productDto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreatActionResult(await _productServiceWithDto.RemoveAsync(id));
        }
        [HttpPost("SaveAll")]
        public async Task<IActionResult> Save(List<ProductDto> productsDtos)
        {
            return CreatActionResult(await _productServiceWithDto.AddRangeAsync(productsDtos));
        }
        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> RemoveAll(List<int> ids)
        {
            return CreatActionResult(await _productServiceWithDto.RemoveRangeAsync(ids));
        }
        [HttpDelete("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreatActionResult(await _productServiceWithDto.AnyAsync(x => x.Id == id));
        }
    }
}
