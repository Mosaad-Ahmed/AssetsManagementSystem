using AssetsManagementSystem.DTOs.CategoryDTOs;
using AssetsManagementSystem.Services.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetsManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(CategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }


        #region AddCategory 
        [HttpPost("add")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequestDTO addCategoryRequest)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for AddCategory request");
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryService.AddCategoryAsync(addCategoryRequest);
                _logger.LogInformation($"Category '{addCategoryRequest.Name}' added successfully.");
                return Ok(new { Message = "Category added successfully" });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Attempt to add a duplicate category: {addCategoryRequest.Name}");
                return Conflict(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding category.");
                return StatusCode(500, new { Error = "An error occurred while adding the category", Details = ex.Message });
            }
        }
        #endregion

        #region GetCategoryById
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Manager,Auditor")]

        public async Task<IActionResult> GetCategoryById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid category ID in GetCategoryById request");
                return BadRequest(new { Error = "Invalid category ID" });
            }

            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                _logger.LogInformation($"Category with ID {id} retrieved successfully.");
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Category with ID {id} not found.");
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving category.");
                return StatusCode(500, new { Error = "An error occurred while retrieving the category", Details = ex.Message });
            }
        }
        #endregion

        #region GetAllCategories
        [HttpGet("all")]
        [Authorize(Roles = "Admin,Manager,Auditor")]

        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                _logger.LogInformation("All categories retrieved successfully.");
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all categories.");
                return StatusCode(500, new { Error = "An error occurred while retrieving the categories", Details = ex.Message });
            }
        }
        #endregion

        #region UpdateCategory
        [HttpPut("update/{id:int}")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequestDTO updateCategoryRequest)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdateCategory request");
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                _logger.LogWarning("Invalid category ID in UpdateCategory request");
                return BadRequest(new { Error = "Invalid category ID" });
            }

            try
            {
                await _categoryService.UpdateCategoryAsync(id, updateCategoryRequest);
                _logger.LogInformation($"Category with ID {id} updated successfully.");
                return Ok(new { Message = "Category updated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Attempt to update category with duplicate name: {updateCategoryRequest.Name}");
                return Conflict(new { Error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Category with ID {id} not found.");
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category.");
                return StatusCode(500, new { Error = "An error occurred while updating the category", Details = ex.Message });
            }
        }
        #endregion

        #region DeleteCategory

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid category ID in DeleteCategory request");
                return BadRequest(new { Error = "Invalid category ID" });
            }

            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                _logger.LogInformation($"Category with ID {id} deleted successfully.");
                return Ok(new { Message = "Category deleted successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Category with ID {id} not found.");
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting category.");
                return StatusCode(500, new { Error = "An error occurred while deleting the category", Details = ex.Message });
            }
        }
        #endregion
    }
}
