using AssetsManagementSystem.DTOs.DocumentDTOs;
using AssetsManagementSystem.Services.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetsManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(DocumentService documentService, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }

        #region Add Document
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> AddDocument([FromForm] AddDocumentRequestDTO addDocumentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for AddDocument.");
                    return BadRequest(ModelState);
                }

                var document = await _documentService.AddDocumentAsync(addDocumentDto);
                _logger.LogInformation($"Document {document.Id} added successfully.");

                return CreatedAtAction(nameof(GetDocumentById), new { id = document.Id }, document);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding document.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }
        #endregion

        #region Update Document
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> UpdateDocument(int id, [FromForm] UpdateDocumentRequestDTO updateDocumentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for UpdateDocument.");
                    return BadRequest(ModelState);
                }

                var updatedDocument = await _documentService.UpdateDocumentAsync(id, updateDocumentDto);
                _logger.LogInformation($"Document {id} updated successfully.");

                return Ok(updatedDocument);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Document {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating document.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }
        #endregion

        #region Get Document by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager,Auditor")]

        public async Task<IActionResult> GetDocumentById(int id)
        {
            try
            {
                var document = await _documentService.GetDocumentByIdAsync(id);
                _logger.LogInformation($"Document {id} retrieved successfully.");
                return Ok(document);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Document {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving document.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }
        #endregion

        #region Delete Document
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                await _documentService.RemoveDocumentAsync(id);
                _logger.LogInformation($"Document {id} deleted successfully.");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Document {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting document.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }
        #endregion
    }
}
