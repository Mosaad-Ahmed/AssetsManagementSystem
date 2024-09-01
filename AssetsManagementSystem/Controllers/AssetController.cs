namespace AssetsManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly AssetService _assetService;
        private readonly ILogger<AssetController> _logger;

        public AssetController(AssetService assetService, ILogger<AssetController> logger)
        {
            _assetService = assetService;
            _logger = logger;
        }

        #region Add Asset
        [HttpPost]
        public async Task<IActionResult> AddAsset([FromForm] AddAssetRequestDTO addAssetDto)
        {
            try
            {
                _logger.LogInformation("Adding a new asset with SerialNumber: {SerialNumber}", addAssetDto.SerialNumber);
                var result = await _assetService.AddAssetAsync(addAssetDto);
                return CreatedAtAction(nameof(GetAssetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the asset with SerialNumber: {SerialNumber}", addAssetDto.SerialNumber);
                return BadRequest(new { error = ex.Message });
            }
        }
        #endregion

        #region Get Asset by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching asset with ID: {AssetId}", id);
                var result = await _assetService.GetAssetByIdAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Asset with ID: {AssetId} not found", id);
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the asset with ID: {AssetId}", id);
                return BadRequest(new { error = ex.Message });
            }
        }
        #endregion

        #region Get All Assets
        [HttpGet]
        public async Task<IActionResult> GetAllAssets()
        {
            try
            {
                _logger.LogInformation("Fetching all assets.");
                var result = await _assetService.GetAllAssetsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all assets.");
                return BadRequest(new { error = ex.Message });
            }
        }
        #endregion

        #region Update Asset
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsset(int id, [FromForm] UpdateAssetRequestDTO updateAssetDto)
        {
            try
            {
                _logger.LogInformation("Updating asset with ID: {AssetId}", id);
                var result = await _assetService.UpdateAssetAsync(id, updateAssetDto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Asset with ID: {AssetId} not found", id);
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while updating asset with ID: {AssetId}", id);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the asset with ID: {AssetId}", id);
                return BadRequest(new { error = ex.Message });
            }
        }
        #endregion

        #region Delete Asset
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            try
            {
                _logger.LogInformation("Deleting asset with ID: {AssetId}", id);
                await _assetService.DeleteAssetAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Asset with ID: {AssetId} not found or already deleted", id);
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the asset with ID: {AssetId}", id);
                return BadRequest(new { error = ex.Message });
            }
        }
        #endregion




    }
}
