using AssetsManagementSystem.DTOs.ReceiveMaintainedDeviceDTOs;
using AssetsManagementSystem.Services.Assets;
using AssetsManagementSystem.Services.ReceiveMaintainedAsset;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetsManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReceiveMaintainedAssetController : ControllerBase
    {
        private readonly ReceiveMaintainedAssetService receiveMaintainedAsset;
        private readonly ILogger<ReceiveMaintainedAsset> _logger;

        
        public ReceiveMaintainedAssetController(ReceiveMaintainedAssetService receiveMaintainedAsset, ILogger<ReceiveMaintainedAsset> logger)
        {
             this.receiveMaintainedAsset = receiveMaintainedAsset;
            _logger = logger;
        }
        #region Add ReceiveMaintainedAsset
        [HttpPost]
        public async Task<IActionResult> AddAsset([FromForm] AddReceiveMaintainedAssetRequest addReceiveMaintainedAsset)
        {
            try
            {
                _logger.LogInformation("Adding a new asset with SerialNumber: {SerialNumber}", addReceiveMaintainedAsset);

                 await receiveMaintainedAsset.AddReceiveMaintainedAsset(addReceiveMaintainedAsset);
                
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding theReceiveMaintainedAsset with Info : ", addReceiveMaintainedAsset);
                return BadRequest(new { error = ex.Message });
            }
        }
        #endregion


    }
}
