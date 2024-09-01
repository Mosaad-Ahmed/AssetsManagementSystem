using AssetsManagementSystem.DTOs.AssetTransferDTOs;

namespace AssetsManagementSystem.Services.AssetTransfer
{
    public class AssetTransferService:BaseClassForServices
    {
        private readonly UserManager<User> _userManager;

        public AssetTransferService(IUnitOfWork unitOfWork,
                                    Others.Interfaces.IAutoMapper.IMapper mapper,
                                    IHttpContextAccessor httpContextAccessor,
                                    UserManager<User> userManager)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            _userManager = userManager;
        }


        #region Add Asset Transfer Record (General)
        public async Task<GetAssetTransferRecordResponseDTO> AddAssetTransferAsync(AddAssetTransferRecordRequestDTO dto)
        {
            // Validate the transfer to ensure no duplicate user or location transfer
            ValidateTransfer(dto);

            // Retrieve the asset to be transferred
            var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.Id == dto.AssetId);
            if (asset == null) throw new KeyNotFoundException("Asset not found.");

            // Map DTO to Entity
            var transferRecord = Mapper.Map<AssetTransferRecords>(dto);
            transferRecord.IsUserTransfer = dto.FromUserId != dto.ToUserId;
            transferRecord.Status = TransferStatus.Pending;

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                // Save the transfer record
                await UnitOfWork.writeRepository<AssetTransferRecords>().AddAsync(transferRecord);
                await UnitOfWork.SaveChangeAsync();

                // Determine which action to take based on the transfer type
                if (dto.FromUserId != dto.ToUserId && dto.FromLocationId != dto.ToLocationId)
                {
                    // Both user and location are changing
                    await HandleUserAndLocationTransferAsync(dto, transferRecord, asset);
                }
                else if (dto.FromUserId != dto.ToUserId)
                {
                    // Only user is changing
                    await HandleUserTransferAsync(dto, transferRecord);
                }
                else if (dto.FromLocationId != dto.ToLocationId)
                {
                    // Only location is changing
                    await HandleLocationTransferAsync(asset, transferRecord);
                }

                await UnitOfWork.CommitTransactionAsync();
                return Mapper.Map<GetAssetTransferRecordResponseDTO,AssetTransferRecords>(transferRecord);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        #endregion


        #region Approve Transfer
        public async Task ApproveTransferAsync(int transferId)
        {
            var transferRecord = await UnitOfWork.readRepository<AssetTransferRecords>().GetAsync(r => r.Id == transferId);
            if (transferRecord == null) throw new KeyNotFoundException("Transfer record not found.");
            if (transferRecord.Status != TransferStatus.Pending) throw new InvalidOperationException("Transfer is not in a pending state.");

            // Update the status to Approved
            transferRecord.Status = TransferStatus.Approved;
            transferRecord.ApprovalDate = DateTime.UtcNow;

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                // Update the asset details based on the transfer type
                var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.Id == transferRecord.AssetId);
                if (asset == null) throw new KeyNotFoundException("Asset not found.");

                if (transferRecord.IsUserTransfer)
                {
                    asset.AssignedUserId = transferRecord.ToUserId;
                }
                asset.LocationId = transferRecord.ToLocationId;

                await UnitOfWork.writeRepository<Asset>().UpdateAsync(asset.Id, asset);
                await UnitOfWork.writeRepository<AssetTransferRecords>().UpdateAsync(transferRecord.Id, transferRecord);
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        #endregion


        #region Reject Transfer
        public async Task RejectTransferAsync(int transferId, string rejectionReason)
        {
            var transferRecord = await UnitOfWork.readRepository<AssetTransferRecords>().GetAsync(r => r.Id == transferId);
            if (transferRecord == null) throw new KeyNotFoundException("Transfer record not found.");
            if (transferRecord.Status != TransferStatus.Pending) throw new InvalidOperationException("Transfer is not in a pending state.");

            // Update the status to Rejected and save the rejection reason
            transferRecord.Status = TransferStatus.Rejected;
            transferRecord.RejectionReason = rejectionReason;
            transferRecord.ApprovalDate = null;  

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                await UnitOfWork.writeRepository<AssetTransferRecords>().UpdateAsync(transferRecord.Id, transferRecord);
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        #endregion



        #region Update Asset Transfer Record
        public async Task<GetAssetTransferRecordResponseDTO> UpdateAssetTransferAsync(int id, UpdateAssetTransferRecordRequestDTO dto)
        {
            var transferRecord = await UnitOfWork.readRepository<AssetTransferRecords>().GetAsync(r => r.Id == id);
            if (transferRecord == null) throw new KeyNotFoundException("Transfer record not found.");

            // Validate the transfer to ensure no duplicate user or location transfer
            ValidateTransfer(dto);

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                // Update transfer record fields
                UpdateTransferRecordFields(transferRecord, dto);

                // Determine which action to take based on the transfer type
                if (dto.FromUserId != dto.ToUserId && dto.FromLocationId != dto.ToLocationId)
                {
                    // Both user and location are changing
                    await HandleUserAndLocationTransferAsync(dto, transferRecord, transferRecord.Asset);
                }
                else if (dto.FromUserId != dto.ToUserId)
                {
                    // Only user is changing
                    await HandleUserTransferAsync(dto, transferRecord);
                }
                else if (dto.FromLocationId != dto.ToLocationId)
                {
                    // Only location is changing
                    await HandleLocationTransferAsync(transferRecord.Asset, transferRecord);
                }

                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();

                return Mapper.Map<GetAssetTransferRecordResponseDTO,AssetTransferRecords>(transferRecord);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        #endregion


        #region Delete Asset Transfer Record
        public async Task DeleteAssetTransferAsync(int id)
        {
            var transferRecord = await UnitOfWork.readRepository<AssetTransferRecords>().GetAsync(r => r.Id == id);
            if (transferRecord == null) throw new KeyNotFoundException("Transfer record not found.");

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                await UnitOfWork.writeRepository<AssetTransferRecords>().DeleteAsync(transferRecord);
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        #endregion


        #region Get Asset Transfer Record by ID
        public async Task<GetAssetTransferRecordResponseDTO> GetAssetTransferByIdAsync(int id)
        {
            var transferRecord = await UnitOfWork.readRepository<AssetTransferRecords>().GetAsync(tr => tr.Id == id);
            if (transferRecord == null) throw new KeyNotFoundException("Transfer record not found.");

            return Mapper.Map<GetAssetTransferRecordResponseDTO,AssetTransferRecords>(transferRecord);
        }
        #endregion


        #region Get All Asset Transfer Records
        public async Task<IList<GetAssetTransferRecordResponseDTO>> GetAllAssetTransfersAsync()
        {
            var transferRecords = await UnitOfWork.readRepository<AssetTransferRecords>().GetAllAsync();
            return Mapper.Map<GetAssetTransferRecordResponseDTO,AssetTransferRecords>(transferRecords);
        }
        #endregion

        #region Private Methods

        private void ValidateTransfer(AddAssetTransferRecordRequestDTO dto)
        {
            if (dto.FromUserId == dto.ToUserId)
                throw new InvalidOperationException("Cannot transfer asset to the same user.");
            if (dto.FromLocationId == dto.ToLocationId)
                throw new InvalidOperationException("Cannot transfer asset to the same location.");
        }

        private void UpdateTransferRecordFields(AssetTransferRecords transferRecord, UpdateAssetTransferRecordRequestDTO dto)
        {
            transferRecord.FromUserId = dto.FromUserId;
            transferRecord.ToUserId = dto.ToUserId;
            transferRecord.FromLocationId = dto.FromLocationId;
            transferRecord.ToLocationId = dto.ToLocationId;
            transferRecord.TransferDate = dto.TransferDate;
            transferRecord.Status = dto.Status;
            transferRecord.RejectionReason = dto.RejectionReason;
        }

        private async Task HandleUserTransferAsync(AddAssetTransferRecordRequestDTO dto, AssetTransferRecords transferRecord)
        {
            var toUser = await _userManager.FindByIdAsync(dto.ToUserId.ToString());
            if (toUser == null || toUser.UserStatus != UserStatus.Active.ToString())
            {
                throw new InvalidOperationException("Cannot transfer asset to the selected user.");
            }

            // Await user's approval
            transferRecord.Status = TransferStatus.Pending;
        }

        private async Task HandleLocationTransferAsync(Asset asset, AssetTransferRecords transferRecord)
        {
            transferRecord.Status = TransferStatus.Approved;
            await UpdateAssetLocationAsync(asset.Id, transferRecord.ToLocationId);
        }

        private async Task HandleUserAndLocationTransferAsync(AddAssetTransferRecordRequestDTO dto, AssetTransferRecords transferRecord, Asset asset)
        {
            // Handle user transfer first (await approval)
            await HandleUserTransferAsync(dto, transferRecord);

            // Once approved, handle location transfer
            if (transferRecord.Status == TransferStatus.Approved)
            {
                await HandleLocationTransferAsync(asset, transferRecord);
            }
        }

        private async Task UpdateAssetLocationAsync(int assetId, int newLocationId)
        {
            var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.Id == assetId);
            if (asset == null) throw new KeyNotFoundException("Asset not found.");

            asset.LocationId = newLocationId;
            await UnitOfWork.writeRepository<Asset>().UpdateAsync(asset.Id, asset);
        }

        #endregion



    }
}
