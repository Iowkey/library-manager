using LibraryManager.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Api.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int categoryId);
        Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryModel);
        Task<bool> UpdateCategoryAsync(int categoryId, CategoryDto categoryModel);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
