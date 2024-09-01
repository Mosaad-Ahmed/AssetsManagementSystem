
using AssetsManagementSystem.DTOs.AssetMaintenanceDTOs;

namespace AssetsManagementSystem.Services.AssetMaintenance
{
    public class AssetMaintenanceService : BaseClassForServices
    {
        public AssetMaintenanceService(IUnitOfWork unitOfWork, Others.Interfaces.IAutoMapper.IMapper mapper, IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public async Task AddMaintenanceRecordAsync(AddAssetMaintenanceRecordDTO maintenanceDto)
        {
            //    Verify that the Asset exists
            var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.Id == maintenanceDto.AssetId && (a.IsDeleted==false || a.IsDeleted == null));
            if (asset == null)
            {
                throw new KeyNotFoundException("Asset not found or has been deleted.");
            }

            //   Add the maintenance record
            var maintenanceRecord = new AssetMaintenanceRecords
            {
                AssetId = maintenanceDto.AssetId,
                Description = maintenanceDto.Description,
                AddedOnDate = DateTime.Now,
                PerformedById = Guid.Parse(UserId)
                
            };

            await UnitOfWork.writeRepository<AssetMaintenanceRecords>().AddAsync(maintenanceRecord);

            //   Update the Asset status to 'InRepair'
            asset.Status = "InRepair";
            asset.UpdatedDate = DateTime.Now;

            await UnitOfWork.writeRepository<Asset>().UpdateAsync(asset.Id, asset);

            //  Save changes in a transaction
            await UnitOfWork.BeginTransactionAsync();

            try
            {
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw new Exception("An error occurred while saving the maintenance record and updating the asset status.");
            }
        }


        public async Task<GetAssetMaintenanceRecordDTO> GetMaintenanceRecordByIdAsync(int maintenanceRecordId)
        {
            var maintenanceRecord = await UnitOfWork.readRepository<AssetMaintenanceRecords>()
                                                     .GetAsync(m => m.Id == maintenanceRecordId && (m.IsDeleted == false || m.IsDeleted == null) );

            if (maintenanceRecord == null)
            {
                throw new KeyNotFoundException("Maintenance record not found or has been deleted.");
            }

            var maintenanceDto = Mapper.Map<GetAssetMaintenanceRecordDTO, AssetMaintenanceRecords>(maintenanceRecord);
            return maintenanceDto;
        }

         
        public async Task<IList<GetAssetMaintenanceRecordDTO>> GetAllMaintenanceRecordsAsync()
        {
            var maintenanceRecords = await UnitOfWork.readRepository<AssetMaintenanceRecords>()
                                                      .GetAllAsync(m => (m.IsDeleted == false || m.IsDeleted == null) );

            var maintenanceDtos = Mapper.Map<GetAssetMaintenanceRecordDTO, AssetMaintenanceRecords>(maintenanceRecords);
            return maintenanceDtos;
        }




        public async Task UpdateMaintenanceRecordAsync(UpdateAssetMaintenanceRecordDTO maintenanceDto)
        {
            //   Verify that the maintenance record exists
            var existingRecord = await UnitOfWork.readRepository<AssetMaintenanceRecords>()
                                                  .GetAsync(m => m.Id == maintenanceDto.Id && (m.IsDeleted == false || m.IsDeleted == null));
            if (existingRecord == null)
            {
                throw new KeyNotFoundException("Maintenance record not found or has been deleted.");
            }

            //  Update the maintenance record
             existingRecord.Description = maintenanceDto.Description;
             existingRecord.UpdatedDate = DateTime.Now;
             existingRecord.PerformedById = Guid.Parse(UserId);

            await UnitOfWork.writeRepository<AssetMaintenanceRecords>().UpdateAsync(existingRecord.Id, existingRecord);

            //   Save changes in a transaction
            await UnitOfWork.BeginTransactionAsync();

            try
            {
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw new Exception("An error occurred while updating the maintenance record.");
            }
        }
         
        public async Task DeleteMaintenanceRecordAsync(int maintenanceRecordId)
        {
            //  Verify that the maintenance record exists
            var existingRecord = await UnitOfWork.readRepository<AssetMaintenanceRecords>()
                                                  .GetAsync(m => m.Id == maintenanceRecordId && (m.IsDeleted == false || m.IsDeleted == null));
            if (existingRecord == null)
            {
                throw new KeyNotFoundException("Maintenance record not found or has been deleted.");
            }

            //  Mark the record as deleted and set the deletion date
            existingRecord.IsDeleted = true;
            existingRecord.DeletedDate = DateTime.Now;
            existingRecord.PerformedById = Guid.Parse(UserId);

            await UnitOfWork.writeRepository<AssetMaintenanceRecords>().UpdateAsync(existingRecord.Id, existingRecord);

            //   Save changes in a transaction
            await UnitOfWork.BeginTransactionAsync();

            try
            {
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw new Exception("An error occurred while deleting the maintenance record.");
            }
        }
    }
}
