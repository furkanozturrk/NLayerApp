using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.API_MVC.Services;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.API_MVC.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(CategoryApiService categoryApiService, ProductApiService productApiService)
        {
            _categoryApiService = categoryApiService;
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _productApiService.GetProductsWithCategoryAsync());
        }

        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();



            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {

                await _productApiService.SaveAsync(productDto);


                return RedirectToAction(nameof(Index));
            }

            var categoriesDto = await _categoryApiService.GetAllAsync();



            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }


        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);


            var categoriesDto = await _categoryApiService.GetAllAsync();



            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);

            return View(product);

        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductViewDto productViewDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productViewDto);

                return RedirectToAction(nameof(Index));
            }

            var categoriesDto = await _categoryApiService.GetAllAsync();



            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productViewDto.CategoryId);

            return View(productViewDto);

        }
        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}