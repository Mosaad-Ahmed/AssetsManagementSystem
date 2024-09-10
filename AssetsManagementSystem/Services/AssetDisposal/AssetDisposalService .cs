namespace AssetsManagementSystem.Services.AssetDisposal
{
    public class AssetDisposalService : BaseClassForServices
    {
        public AssetDisposalService(IUnitOfWork unitOfWork,
                 Others.Interfaces.IAutoMapper.IMapper mapper,
                 IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }


        public async Task AddAssetDisposalRecordAsync(AddAssetDisposalRecordDTO disposalDto)
        {
            //  Verify that the Asset exists and is not already deleted
            var asset = await UnitOfWork.readRepository<Asset>()
                                         .GetAsync(a => a.Id == disposalDto.AssetId 
                                         && (a.IsDeleted==false|| a.IsDeleted==null)
                                         &&a.Status != AssetStatus.Retired.ToString());
            if (asset == null)
            {
                throw new KeyNotFoundException("Asset not found or has already been deleted.");
            }

            //   Add the disposal record
 

            var disposalRecord = Mapper.Map<AssetDisposalRecord, AddAssetDisposalRecordDTO> (disposalDto);

            disposalRecord.ApprovedById =Guid.Parse(UserId);

            disposalRecord.AddedOnDate = DateTime.Now;

            await UnitOfWork.writeRepository<AssetDisposalRecord>().AddAsync(disposalRecord);

            //   Update the Asset status to 'Retired' and mark it as deleted
            asset.Status = "Retired";
            asset.IsDeleted = true;
            asset.DeletedDate = DateTime.Now;
            await DeleteAssetSuppliers(asset.Id);
            await UnitOfWork.writeRepository<Asset>().UpdateAsync(asset.Id, asset);

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
                throw new Exception("An error occurred while saving the disposal record and updating the asset status.");
            }
        }

        public async Task<IList<GetAssetDisposalRecordDTO>> GetAllAssetDisposalRecordsAsync()
        {
            var disposalRecords = await UnitOfWork.readRepository<AssetDisposalRecord>()
                                                   .GetAllAsync(d => (d.IsDeleted == false || d.IsDeleted == null));

            var disposalRecordsDto = disposalRecords.Select(d => new GetAssetDisposalRecordDTO
            {
                Id = d.Id,
                AssetId = d.AssetId,
                AssetName = d.Asset.Name,
                 Reason = d.Reason,
                Method = d.Method,
                ApprovedById = d.ApprovedById,
                ApprovedByName = d.ApprovedBy.UserName,
                AddedOnDate = d.AddedOnDate,
                UpdatedDate = d.UpdatedDate
            }).ToList();

            return disposalRecordsDto;
        }

        public async Task<GetAssetDisposalRecordDTO> GetAssetDisposalRecordByIdAsync(int disposalRecordId)
        {
            var disposalRecord = await UnitOfWork.readRepository<AssetDisposalRecord>()
                                                  .GetAsync(d => d.Id == disposalRecordId && (d.IsDeleted == false || d.IsDeleted == null));
            if (disposalRecord == null)
            {
                throw new KeyNotFoundException("Disposal record not found or has been deleted.");
            }

            var disposalRecordDto = new GetAssetDisposalRecordDTO
            {
                Id = disposalRecord.Id,
                AssetId = disposalRecord.AssetId,
                AssetName = disposalRecord.Asset.Name,
                 Reason = disposalRecord.Reason,
                Method = disposalRecord.Method,
                ApprovedById = disposalRecord.ApprovedById,
                ApprovedByName = disposalRecord.ApprovedBy.UserName,
                AddedOnDate = disposalRecord.AddedOnDate,
                UpdatedDate = disposalRecord.UpdatedDate
            };

            return disposalRecordDto;
        }

        public async Task UpdateAssetDisposalRecordAsync(UpdateAssetDisposalRecordDTO disposalDto)
        {
            //  Verify that the disposal record exists
            var existingRecord = await UnitOfWork.readRepository<AssetDisposalRecord>()
                                                  .GetAsync(d => d.Id == disposalDto.Id 
                                                  && (d.IsDeleted==false|| d.IsDeleted == null)
                                                 );
            if (existingRecord == null)
            {
                throw new KeyNotFoundException("Disposal record not found or associated asset has been deleted.");
            }

            //  Update the disposal record
             existingRecord.Reason = disposalDto.Reason;
            existingRecord.Method = disposalDto.Method;
            existingRecord.ApprovedById = Guid.Parse(UserId);
            existingRecord.UpdatedDate = DateTime.Now;

            await UnitOfWork.writeRepository<AssetDisposalRecord>().UpdateAsync(existingRecord.Id, existingRecord);

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
                throw new Exception("An error occurred while updating the disposal record.");
            }
        }

        public async Task DeleteAssetDisposalRecordAsync(int disposalRecordId)
        {
            //  Verify that the disposal record exists
            var existingRecord = await UnitOfWork.readRepository<AssetDisposalRecord>()
                                                  .GetAsync(d => d.Id == disposalRecordId && 
                                                  (d.IsDeleted == false || d.IsDeleted == null),enableTracing:true);
            if (existingRecord == null)
            {
                throw new KeyNotFoundException("Disposal record not found or has already been deleted.");
            }

            //   Mark the disposal record as deleted  
            existingRecord.IsDeleted = true;
            existingRecord.DeletedDate = DateTime.Now;
            existingRecord.ApprovedById = Guid.Parse(UserId);
            await UnitOfWork.writeRepository<AssetDisposalRecord>().UpdateAsync(existingRecord.Id, existingRecord);

            //   Update the associated Asset to set it back to 'Active' and remove its deletion status
            var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.Id == existingRecord.AssetId);
            if (asset == null)
            {
                throw new Exception("Associated asset not found.");
            }

            asset.Status = "Active";
            asset.IsDeleted = false;
            asset.DeletedDate = null;
            asset.UpdatedDate = DateTime.Now;

            await UnitOfWork.writeRepository<Asset>().UpdateAsync(asset.Id, asset);

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
                throw new Exception("An error occurred while deleting the disposal record and updating the asset.");
            }
        }



        #region Delete existing asset-supplier relationships
        private async Task DeleteAssetSuppliers(int assetId)
        {
            var existingAssetSuppliers = await UnitOfWork.readRepository<AssetsSuppliers>()
                .GetAllAsync(As => As.AssetId == assetId);

            await UnitOfWork.writeRepository<AssetsSuppliers>().DeleteRangeAsync(existingAssetSuppliers);
            await UnitOfWork.SaveChangeAsync();
        }
        #endregion

    }
}
