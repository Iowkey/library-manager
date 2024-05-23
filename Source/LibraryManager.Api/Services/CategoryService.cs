using AutoMapper;
using LibraryManager.Api.DTOs;
using LibraryManager.Data.Entities;
using LibraryManager.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddCategoryAsync(category);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateCategoryAsync(int categoryId, CategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return false;
            }
            
            _mapper.Map(categoryDto, category);
            await _categoryRepository.UpdateCategoryAsync(category);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return false;
            }

            await _categoryRepository.DeleteCategoryAsync(categoryId);
            return true;
        }
    }
}