using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService,
                                  IMapper mapper,
                                  ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var products = (await _productService.GetProductsWithCategory()).Data;
            return View(products);
        }
        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductViewDto productViewDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(_mapper.Map<Product>(productViewDto));
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            var categories = await _categoryService.GetAllAsync();

            var categoryiesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryiesDto, "Id", "Name", product.CategoryId);

            return View(_mapper.Map<ProductDto>(product));
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductViewDto productViewDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(_mapper.Map<Product>(productViewDto));
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetAllAsync();

            var categoryiesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryiesDto, "Id", "Name", productViewDto.CategoryId);

            return View(productViewDto);
        }
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            await _productService.RemoveAsync(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
