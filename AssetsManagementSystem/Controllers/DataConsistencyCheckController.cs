using AssetsManagementSystem.DTOs.DataConsistencyCheckDTOs;
using AssetsManagementSystem.Services.Assets;
using AssetsManagementSystem.Services.DataConsistencyCheck;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetsManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataConsistencyCheckController : ControllerBase
    {
        private readonly ILogger<DataConsistencyCheckService> logger;
        private readonly DataConsistencyCheckService dataConsistencyCheckService;

        public DataConsistencyCheckController(ILogger<DataConsistencyCheckService> logger,
                                               DataConsistencyCheckService dataConsistencyCheckService)
        {
            this.logger = logger;
            this.dataConsistencyCheckService = dataConsistencyCheckService;
        }

        [HttpPost]
        [Authorize(Roles = "Auditor")]

        public async Task<IActionResult> AddDataConsistencyCheckRecord(AddDataConsistencyCheckRequestDTO addDataConsistencyCheckRequestDTO)
        {
            try
            {
                logger.LogInformation("Adding a new DataConsistencyCheckRecord with INFO :", addDataConsistencyCheckRequestDTO);

                await dataConsistencyCheckService.AddDataConsistencyCheckRecord(addDataConsistencyCheckRequestDTO);

                return Created();

            }
            catch (Exception ex)
            {
                logger.LogError(ex,"An error occurred while Adding a new DataConsistencyCheckRecord");
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}


 