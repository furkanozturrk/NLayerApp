using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var categories = await _categoryService.GetAllAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            return CreatActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto));

        }
        [HttpGet]
        public async Task<IActionResult> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            return CreatActionResult(await _categoryService.GetSingleCategoryByIdWithProductsAsync(categoryId));
        }
    }
}
