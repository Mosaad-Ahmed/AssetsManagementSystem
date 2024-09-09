using AssetsManagementSystem.DTOs.SubCategoryDTOs;
using AssetsManagementSystem.Services.SubCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetsManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly SubCategoryerService _subCategoryService;
        private readonly ILogger<SubCategoryController> _logger;

        public SubCategoryController(SubCategoryerService subCategoryService, ILogger<SubCategoryController> logger)
        {
            _subCategoryService = subCategoryService;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> AddSubCategory([FromBody] AddSubCategoryRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _subCategoryService.AddSubCategory(dto);
                _logger.LogInformation("SubCategory added successfully with ID {SubCategoryId}.", result.Id);
                return CreatedAtAction(nameof(GetSubCategoryById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Attempt to add a subcategory with a duplicate name.");
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a subcategory.");
                return StatusCode(500, "Internal server error.");
            }
        }

         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            try
            {
                var result = await _subCategoryService.GetSubCategoryByIdAsync(id);
                _logger.LogInformation("SubCategory with ID {SubCategoryId} retrieved successfully.", id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "SubCategory with ID {SubCategoryId} not found.", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving subcategory with ID {SubCategoryId}.", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubCategories()
        {
            try
            {
                var result = await _subCategoryService.GetAllSubCategoriesAsync();
                _logger.LogInformation("All subcategories retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all subcategories.");
                return StatusCode(500, "Internal server error.");
            }
        }

         

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, [FromBody] UpdateSubCategoryRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _subCategoryService.UpdateSubCategoryAsync(id, dto);
                _logger.LogInformation("SubCategory with ID {SubCategoryId} updated successfully.", id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "SubCategory with ID {SubCategoryId} not found for update.", id);
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Attempt to update a subcategory to a duplicate name.");
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating subcategory with ID {SubCategoryId}.", id);
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            try
            {
                await _subCategoryService.DeleteSubCategory(id);
                _logger.LogInformation("SubCategory with ID {SubCategoryId} deleted successfully.", id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "SubCategory with ID {SubCategoryId} not found for deletion.", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting subcategory with ID {SubCategoryId}.", id);
                return StatusCode(500, "Internal server error.");
            }
        }

         


    }
}
