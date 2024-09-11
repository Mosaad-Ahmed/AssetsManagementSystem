
using AssetsManagementSystem.DTOs.AssetMaintenanceDTOs;

namespace AssetsManagementSystem.Services.AssetMaintenance
{
    public class AssetMaintenanceService : BaseClassForServices
    {
        private readonly UserManager<User> userManager;

        public AssetMaintenanceService(IUnitOfWork unitOfWork,
            Others.Interfaces.IAutoMapper.IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager) 
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            this.userManager = userManager;
        }

        public async Task AddMaintenanceRecordAsync(AddAssetMaintenanceRecordDTO maintenanceDto)
        {
            //    Verify that the Asset exists
            var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.SerialNumber == maintenanceDto.AssetSerialNumber && (a.IsDeleted==false || a.IsDeleted == null));
            if (asset == null)
            {
                throw new KeyNotFoundException("Asset not found or has been deleted.");
            }

            var supplier = await UnitOfWork.readRepository<Supplier>().GetAsync(a => a.Id == maintenanceDto.TOWhomOfSupplierId && (a.IsDeleted == false || a.IsDeleted == null));
            if (supplier == null)
            {
                throw new KeyNotFoundException("supplier not found or has been deleted.");
            }

            var user = await userManager.FindByIdAsync(maintenanceDto.TOWhomOfUserId.ToString());
            if (user.UserStatus != UserStatus.Active.ToString())
            {
                throw new KeyNotFoundException("User not found or has been deleted.");
            }



            //   Add the maintenance record
            var maintenanceRecord = new AssetMaintenanceRecords
            {
                AssetId = asset.Id,
                Description = maintenanceDto.Description,
                AddedOnDate = DateTime.Now,
                PerformedById = Guid.Parse(UserId),
                TOWhomOfSupplierId=maintenanceDto.TOWhomOfSupplierId,
                TOWhomOfUserId=maintenanceDto.TOWhomOfUserId,
                
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
          
            maintenanceDto.AssetName = maintenanceRecord.Asset.Name;

            var supplier = await UnitOfWork.readRepository<Supplier>().GetAsync(x => x.Id == maintenanceRecord.TOWhomOfSupplierId);
            maintenanceDto.TOWhomOfSupplierName = supplier.CompanyName;
              
            var user = await userManager.FindByIdAsync(maintenanceRecord.TOWhomOfUserId.ToString());
            maintenanceDto.TOWhomOfUserName = string.Concat(user.FirstName," ",user.LastName);

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
            existingRecord.TOWhomOfUserId = maintenanceDto.TOWhomOfUserId;
            existingRecord.TOWhomOfSupplierId=maintenanceDto.TOWhomOfSupplierId;
        

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

        #region
        //public async Task DeleteMaintenanceRecordAsync(int maintenanceRecordId)
        //{
        //    //  Verify that the maintenance record exists
        //    var existingRecord = await UnitOfWork.readRepository<AssetMaintenanceRecords>()
        //                                          .GetAsync(m => m.Id == maintenanceRecordId 
        //                                          && (m.IsDeleted == false || m.IsDeleted == null));
        //    if (existingRecord == null)
        //    {
        //        throw new KeyNotFoundException("Maintenance record not found or has been deleted.");
        //    }

        //    //  Mark the record as deleted and set the deletion date
        //    existingRecord.IsDeleted = true;
        //    existingRecord.DeletedDate = DateTime.Now;
        //    existingRecord.PerformedById = Guid.Parse(UserId);

        //    await UnitOfWork.writeRepository<AssetMaintenanceRecords>().UpdateAsync(existingRecord.Id, existingRecord);

        //   var asset =  await UnitOfWork.readRepository<Asset>()
        //        .GetAsync(predicate: a => a.Id == existingRecord.AssetId && (a.IsDeleted == false || a.IsDeleted == null));

        //    asset.Status = AssetStatus.Active.ToString();
        //    asset.UpdatedDate = DateTime.Now;

        //    await UnitOfWork.writeRepository<Asset>().UpdateAsync(asset.Id, asset);

        //    //   Save changes in a transaction
        //    await UnitOfWork.BeginTransactionAsync();

        //    try
        //    {
        //        await UnitOfWork.SaveChangeAsync();
        //        await UnitOfWork.CommitTransactionAsync();
        //    }
        //    catch
        //    {
        //        await UnitOfWork.RollbackTransactionAsync();
        //        throw new Exception("An error occurred while deleting the maintenance record.");
        //    }
        //}
        #endregion
    }
}
