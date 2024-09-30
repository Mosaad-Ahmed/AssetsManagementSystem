﻿using AssetsManagementSystem.DTOs.CategoryDTOs;
using AssetsManagementSystem.Services.Categories;
using ClosedXML.Excel;
using System.Data;

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
        [Authorize(Roles = "User,Manager,Admin")]
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
        //[Authorize(Roles = "Admin,Manager,Auditor")]

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

        #region File Excel
        [HttpGet("ExportExcelOfAsset")]
        public async Task<IActionResult> ExportExcelOfAsset() 
        {

            using (XLWorkbook xLWorkbook=new XLWorkbook())
            {

                xLWorkbook.AddWorksheet(GetDataOfAssets().Result,$"SheetOfAsset");
                using (MemoryStream ms=new MemoryStream())
                {
                    xLWorkbook.SaveAs(ms);
                    return File(ms.ToArray(),
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                $"Sheet Of Asset for Date {DateOnly.FromDateTime(DateTime.Now)}");

                }

            }
             

         }


        [NonAction]
        private async Task<DataTable> GetDataOfAssets() 
        {

            var data =await _assetService.GetAllAssetsAsync();

            DataTable dt = new DataTable();
             
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("ModelNumber", typeof(string));
            dt.Columns.Add("SerialNumber", typeof(string));
            dt.Columns.Add("PurchaseDate", typeof(DateOnly));
            dt.Columns.Add("PurchasePrice", typeof(decimal));
            dt.Columns.Add("WarrantyExpiryDate", typeof(DateOnly));
            dt.Columns.Add("DepreciationDate", typeof(DateOnly));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("LocationName", typeof(string));
            dt.Columns.Add("AssignedUserName", typeof(string));
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Columns.Add("ManfactureName", typeof(string));
            dt.Columns.Add("AddedOnDate", typeof(DateOnly));
            dt.Columns.Add("UpdatedDate", typeof(DateOnly));
            

            if (data.Count>0)
            {
                foreach (var d in data)
                {
                    dt.Rows.Add(d.Id, d.Name, d.ModelNumber,
                                    d.SerialNumber, d.PurchaseDate, d.PurchasePrice, d.WarrantyExpiryDate,
                                    d.DepreciationDate, d.Status, d.dicription, d.LocationName, d.AssignedUserName,
                                    d.ManfactureName, DateOnly.FromDateTime(d.AddedOnDate),
                                        DateOnly.FromDateTime(d.UpdatedDate ?? new DateTime())
                                         
                               );
                }
         
 
            }
           
            return dt;

        }

        #endregion


        [HttpPost("ReadCategoriesFromExcel")]
        public async Task<IActionResult> ReadAssetsFromExcel(IFormFile file)
        {
            var targetColor = XLColor.FromArgb(146, 212, 193);

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1); // تجاوز العنوان (الصف الأول)

                    foreach (var row in rows)
                    {
                        bool isCustomColor = row.CellsUsed().All(cell => cell.Style.Fill.BackgroundColor == targetColor);

                        if (isCustomColor)
                        {
                            // إيجاد آخر خلية غير فارغة في الصف
                            int lastNonEmptyCellIndex = 0;
                            for (int i = row.LastCellUsed().Address.ColumnNumber; i >= 1; i--)
                            {
                                if (!row.Cell(i).IsEmpty())
                                {
                                    lastNonEmptyCellIndex = i;
                                    break;
                                }
                            }

                            // استخراج SerialNumber (الخلية الأخيرة غير الفارغة)
                            string serialNumber = lastNonEmptyCellIndex >= 1 ? row.Cell(lastNonEmptyCellIndex).GetString() : null;

                            // استخراج CategoryId (الخلية التي قبل الأخيرة)
                            string categoryId = lastNonEmptyCellIndex >= 2 ? row.Cell(lastNonEmptyCellIndex - 1).GetString() : null;

                            // هنا يمكن استخدام المتغيرات حسب الحاجة
                            // على سبيل المثال، يمكن طباعة أو استخدام القيم في منطق معين:
                            //Console.WriteLine($"Serial Number: {serialNumber}, Category ID: {categoryId}");

                            // إذا كنت تريد إضافة البيانات إلى قاعدة البيانات أو استخدام الخدمة
                            await _assetService.AddAssetAsync(new AddAssetRequestDTO()
                            {
                                Name = row.Cell(1).GetString(),
                                SerialNumber = serialNumber,
                                CategoryId = int.Parse(categoryId),
                                dicription = row.Cell(1).GetString(),
                                ModelNumber = serialNumber,
                                Status = AssetStatus.Active,
                                LocationId = 1,
                                ManufacturerId = 1,
                                SupplierIds = new List<int>() { 1 },
                                AssignedUserId = Guid.Parse("bdabcf06-a956-4ef7-8045-3214e68b9b4c"),
                                PurchasePrice = 0,
                                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                                WarrantyExpiryDate = DateOnly.FromDateTime(DateTime.Now).AddDays(1),
                                DepreciationDate = DateOnly.FromDateTime(DateTime.Now)


                            });
                        }
                    }
                }
            }

            return Ok(new { Message = "تم استخراج SerialNumber و CategoryId بنجاح" });
        }

    }
}

 