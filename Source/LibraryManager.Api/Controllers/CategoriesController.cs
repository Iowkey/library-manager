using LibraryManager.Api.DTOs;
using LibraryManager.Api.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace LibraryManager.Api.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateCategory(CategoryDto categoryDto)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
            return Created($"categories/{createdCategory.CategoryId}", createdCategory);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.CategoryId) return BadRequest();
            var result = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            if (!result)
            {
                return NotFound();
            }

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result) return NotFound();
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}