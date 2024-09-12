
namespace AssetsManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssetDisposalController : ControllerBase
    {
        private readonly AssetDisposalService _assetDisposalService;

        public AssetDisposalController(AssetDisposalService assetDisposalService)
        {
            _assetDisposalService = assetDisposalService;
        }

        //[Authorize(Roles = "Manager, Admin")]
        [HttpPost("AddDisposalRecord")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> AddAssetDisposalRecord([FromBody] AddAssetDisposalRecordDTO disposalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _assetDisposalService.AddAssetDisposalRecordAsync(disposalDto);
                return Ok(new { message = "Disposal record added successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }


        [HttpGet("GetDisposalRecord/{disposalRecordId}")]
        [Authorize(Roles = "Admin,Manager,Auditor")]

        public async Task<IActionResult> GetAssetDisposalRecordById(int disposalRecordId)
        {
            try
            {
                var disposalRecord = await _assetDisposalService.GetAssetDisposalRecordByIdAsync(disposalRecordId);
                return Ok(disposalRecord);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }



        [HttpGet("GetAllDisposalRecords")]
        [Authorize(Roles = "Admin,Manager,Auditor")]

        public async Task<IActionResult> GetAllAssetDisposalRecords()
        {
            try
            {
                var disposalRecords = await _assetDisposalService.GetAllAssetDisposalRecordsAsync();

                if (disposalRecords == null || disposalRecords.Count == 0)
                {
                    return NotFound(new { message = "No disposal records found." });
                }

                return Ok(disposalRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("GetByPaginationDisposalRecords")]
        [Authorize(Roles = "Admin,Manager,Auditor")]

        public async Task<IActionResult> GetByPaginationDisposalRecords(int currentPage, int pageSize)
        {
            try
            {
                var disposalRecords = await _assetDisposalService.GetAllByPaginationAssetDisposalRecordsAsync(currentPage, pageSize);

                if (disposalRecords == null || disposalRecords.Count == 0)
                {
                    return NotFound(new { message = "No disposal records found." });
                }

                return Ok(disposalRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }


        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("UpdateDisposalRecord")]
        public async Task<IActionResult> UpdateAssetDisposalRecord([FromForm] UpdateAssetDisposalRecordDTO disposalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _assetDisposalService.UpdateAssetDisposalRecordAsync(disposalDto);
                return Ok(new { message = "Disposal record updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }


         [HttpDelete("DeleteDisposalRecord/{disposalRecordId}")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> DeleteAssetDisposalRecord(int disposalRecordId)
        {
            try
            {
                await _assetDisposalService.DeleteAssetDisposalRecordAsync(disposalRecordId);
                return Ok(new { message = "Disposal record deleted and asset status updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }
         
    }
}

