using NLayer.Core.DTOs;

namespace NLayer.API_MVC.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("products/GetProductWithCategory");
            return response.Data;
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/GetById/{id}");
            return response.Data;
        }
        public async Task<ProductDto> SaveAsync(ProductDto productDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Products/Save", productDto);

            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
            return responseBody.Data;
        }
        public async Task<bool> UpdateAsync(ProductViewDto productViewDto)
        {
            var response = await _httpClient.PutAsJsonAsync("products/update", productViewDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
