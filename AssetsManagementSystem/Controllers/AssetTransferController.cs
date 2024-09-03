﻿
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

        #region Transfer Location to Location
        [HttpPost]
        public async Task<IActionResult> TransferLocationToLocation([FromBody] LocationToLocationTransferDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for TransferLocationToLocation.");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTransferService.TransferLocationToLocationAsync(dto);
                _logger.LogInformation("Asset transfer (location to location) added successfully.");
                return CreatedAtAction(nameof(GetAssetTransferById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while transferring asset (location to location).");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion

        #region Transfer User to User
        [HttpPost]
        public async Task<IActionResult> TransferUserToUser([FromBody] UserToUserTransferDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for TransferUserToUser.");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTransferService.TransferUserToUserAsync(dto);
                _logger.LogInformation("Asset transfer (user to user) added successfully.");
                return CreatedAtAction(nameof(GetAssetTransferById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while transferring asset (user to user).");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion

        #region Transfer User and Location
        [HttpPost]
        public async Task<IActionResult> TransferUserAndLocation([FromBody] UserAndLocationTransferDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for TransferUserAndLocation.");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTransferService.TransferUserAndLocationAsync(dto);
                _logger.LogInformation("Asset transfer (user and location) added successfully.");
                return CreatedAtAction(nameof(GetAssetTransferById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while transferring asset (user and location).");
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
            if (string.IsNullOrWhiteSpace(rejectionReason))
            {
                _logger.LogWarning("Rejection reason is required.");
                return BadRequest("Rejection reason is required.");
            }

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

        #region Update Location to Location Transfer
        [HttpPut("{id}/locationtolocation")]
        public async Task<IActionResult> UpdateLocationToLocationTransfer(int id, [FromBody] LocationToLocationTransferDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdateLocationToLocationTransfer.");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTransferService.UpdateLocationToLocationTransferAsync(id, dto);
                _logger.LogInformation("Asset transfer (location to location) updated successfully.");
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Transfer record not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating asset transfer (location to location).");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion

        #region Update User to User Transfer
        [HttpPut("{id}/usertouser")]
        public async Task<IActionResult> UpdateUserToUserTransfer(int id, [FromBody] UserToUserTransferDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdateUserToUserTransfer.");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTransferService.UpdateUserToUserTransferAsync(id, dto);
                _logger.LogInformation("Asset transfer (user to user) updated successfully.");
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Transfer record not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating asset transfer (user to user).");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion

        #region Update User and Location Transfer
        [HttpPut("{id}/userandlocation")]
        public async Task<IActionResult> UpdateUserAndLocationTransfer(int id, [FromBody] UserAndLocationTransferDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdateUserAndLocationTransfer.");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _assetTransferService.UpdateUserAndLocationTransferAsync(id, dto);
                _logger.LogInformation("Asset transfer (user and location) updated successfully.");
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Transfer record not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating asset transfer (user and location).");
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
                _logger.LogInformation($"Asset transfer {id} deleted successfully.");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Asset transfer {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting asset transfer {id}.");
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
                _logger.LogInformation($"Asset transfer {id} retrieved successfully.");
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Asset transfer {id} not found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving asset transfer {id}.");
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
                _logger.LogInformation("All asset transfers retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all asset transfers.");
                return StatusCode(500, "Internal server error.");
            }
        }
        #endregion
    }
}
