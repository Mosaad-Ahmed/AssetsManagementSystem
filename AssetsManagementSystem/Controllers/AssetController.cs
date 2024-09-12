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
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> AddAsset([FromForm] AddAssetRequestDTO addAssetDto)
        {
            try
            {
                _logger.LogInformation("Adding a new asset with SerialNumber: {SerialNumber}", addAssetDto.SerialNumber);
                var result = await _assetService.AddAssetAsync(addAssetDto);
                return CreatedAtAction(nameof(GetAssetById), new {  result.SerialNumber }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the asset with SerialNumber: {SerialNumber}", addAssetDto.SerialNumber);
                return BadRequest(new { error = ex.Message });
            }
        }
        #endregion

        #region Get Asset by ID
        [HttpGet("{serialNumber}")]
        [Authorize(Roles = "Admin,Manager,Auditor")]

        public async Task<IActionResult> GetAssetById(string serialNumber)
        {
            try
            {
                _logger.LogInformation("Fetching asset with serialNumber: {AssetSerialNumber}", serialNumber);
                var result = await _assetService.GetAssetByIdAsync(serialNumber);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Asset with ID: {AssetId} not found", serialNumber);
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the asset with serialNumber: {AssetSerialNumber}", serialNumber);
                return BadRequest(new { error = ex.Message });
            }
        }
        #endregion

        #region Get Asset by Current User
        [HttpGet()]
        [Authorize(Roles = "User,Manager")]
        public async Task<IActionResult> GetAssetForCurrentUser()
        {
            try
            {
                 var result = await _assetService.GetAssetsForCurrentUser();
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                 return BadRequest(new { error = ex.Message });
            }
        }
        #endregion

        #region Get All Assets
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Auditor")]

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

        #region Get All Assets ByPagination
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Auditor")]

        public async Task<IActionResult> GetAssetsByPagination(int currentPage , int pageSize )
        {
            try
            {
                _logger.LogInformation("Fetching all assets.");
                var result = await _assetService.GetAllByPaginationAssetsAsync(currentPage,pageSize);
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
        [HttpPut()]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> UpdateAsset(string serialNumber, [FromForm] UpdateAssetRequestDTO updateAssetDto)
        {
            try
            {
                _logger.LogInformation("Updating asset with ID: {AssetId}", serialNumber);
                var result = await _assetService.UpdateAssetAsync(serialNumber, updateAssetDto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Asset with ID: {AssetId} not found", serialNumber);
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while updating asset with ID: {AssetId}", serialNumber);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the asset with ID: {AssetId}", serialNumber);
                return BadRequest(new { error = ex.Message });
            }
        }
        #endregion

        #region Delete Asset
        [HttpDelete("{serialNumber}")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> DeleteAsset(string serialNumber)
        {
            try
            {
                _logger.LogInformation("Deleting asset with serialNumber: {AssetSerialNumber}", serialNumber);
                await _assetService.DeleteAssetAsync(serialNumber);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Asset with serialNumber: {AssetSerialNumber}\", serialNumber not found or already deleted", serialNumber);
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the asset with ID: {AssetId}", serialNumber);
                return BadRequest(new { error = ex.Message });
            }
        }
        #endregion




    }
}
