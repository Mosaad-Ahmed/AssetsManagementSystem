
using AssetsManagementSystem.DTOs.AssetTransferDTOs;
using AssetsManagementSystem.Services.AssetTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetsManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssetTransferController : ControllerBase
    {
        private readonly AssetTransferService _assetTransferService;
        private readonly ILogger<AssetTransferController> _logger;

        public AssetTransferController(AssetTransferService assetTransferService, ILogger<AssetTransferController> logger)
        {
            _assetTransferService = assetTransferService;
            _logger = logger;
        }


        #region Add Asset Transfer
        [HttpPost]
        public async Task<IActionResult> AddAssetTransfer([FromBody] AddAssetTransferRecordRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for AddAssetTransfer.");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTransferService.AddAssetTransferAsync(dto);
                _logger.LogInformation("Asset transfer added successfully.");
                return CreatedAtAction(nameof(GetAssetTransferById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding asset transfer.");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion

        #region Update Asset Transfer
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssetTransfer(int id, [FromBody] UpdateAssetTransferRecordRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdateAssetTransfer.");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTransferService.UpdateAssetTransferAsync(id, dto);
                _logger.LogInformation("Asset transfer updated successfully.");
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Asset transfer not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating asset transfer.");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion


        #region Approve Transfer
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveTransfer(int id)
        {
            try
            {
                await _assetTransferService.ApproveTransferAsync(id);
                _logger.LogInformation($"Asset transfer {id} approved successfully.");
                return Ok("Transfer approved successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Asset transfer {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while approving asset transfer {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion

        #region Reject Transfer
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectTransfer(int id, [FromBody] string rejectionReason)
        {
            try
            {
                await _assetTransferService.RejectTransferAsync(id, rejectionReason);
                _logger.LogInformation($"Asset transfer {id} rejected successfully.");
                return Ok("Transfer rejected successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Asset transfer {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while rejecting asset transfer {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion

        #region Delete Asset Transfer
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetTransfer(int id)
        {
            try
            {
                await _assetTransferService.DeleteAssetTransferAsync(id);
                _logger.LogInformation("Asset transfer deleted successfully.");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Asset transfer not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting asset transfer.");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion

        #region Get Asset Transfer by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetTransferById(int id)
        {
            try
            {
                var result = await _assetTransferService.GetAssetTransferByIdAsync(id);
                _logger.LogInformation("Asset transfer retrieved successfully.");
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Asset transfer not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving asset transfer.");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion

        #region Get All Asset Transfers
        [HttpGet]
        public async Task<IActionResult> GetAllAssetTransfers()
        {
            try
            {
                var result = await _assetTransferService.GetAllAssetTransfersAsync();
                _logger.LogInformation("Asset transfers retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving asset transfers.");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion

         

    }
}
