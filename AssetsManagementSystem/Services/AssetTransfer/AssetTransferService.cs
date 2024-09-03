using AssetsManagementSystem.DTOs.AssetTransferDTOs;

namespace AssetsManagementSystem.Services.AssetTransfer
{
    public class AssetTransferService : BaseClassForServices
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

        #region Location to Location Transfer
        public async Task<GetAssetTransferRecordResponseDTO> TransferLocationToLocationAsync(LocationToLocationTransferDTO dto)
        {
            ValidateLocationTransfer(dto.FromLocationId, dto.ToLocationId);

            var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.Id == dto.AssetId);
            if (asset == null) throw new KeyNotFoundException("Asset not found.");

            var transferRecord = new AssetTransferRecords
            {
                AssetId = dto.AssetId,
                FromLocationId = dto.FromLocationId,
                ToLocationId = dto.ToLocationId,
                FromUserId = asset.AssignedUserId,
                ToUserId = asset.AssignedUserId,
                TransferDate = dto.TransferDate,
                Status = TransferStatus.Approved
            };

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                await UnitOfWork.writeRepository<AssetTransferRecords>().AddAsync(transferRecord);
                asset.LocationId = dto.ToLocationId;
                await UnitOfWork.writeRepository<Asset>().UpdateAsync(asset.Id, asset);

                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();

                return Mapper.Map<GetAssetTransferRecordResponseDTO>(transferRecord);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        #endregion

        #region User to User Transfer
        public async Task<GetAssetTransferRecordResponseDTO> TransferUserToUserAsync(UserToUserTransferDTO dto)
        {
            ValidateUserTransfer(dto.FromUserId, dto.ToUserId);

            var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.Id == dto.AssetId);
            if (asset == null) throw new KeyNotFoundException("Asset not found.");

            var transferRecord = new AssetTransferRecords
            {
                AssetId = dto.AssetId,
                FromUserId = dto.FromUserId,
                ToUserId = dto.ToUserId,
                FromLocationId=asset.LocationId,
                ToLocationId = asset.LocationId,
                TransferDate = dto.TransferDate,
                Status = TransferStatus.Pending ,
                IsUserTransfer=true
                
            };

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                await UnitOfWork.writeRepository<AssetTransferRecords>().AddAsync(transferRecord);
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

        #region User and Location Transfer
        public async Task<GetAssetTransferRecordResponseDTO> TransferUserAndLocationAsync(UserAndLocationTransferDTO dto)
        {
            ValidateUserTransfer(dto.FromUserId, dto.ToUserId);
            ValidateLocationTransfer(dto.FromLocationId, dto.ToLocationId);

            var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.Id == dto.AssetId);
            if (asset == null) throw new KeyNotFoundException("Asset not found.");

            var transferRecord = new AssetTransferRecords
            {
                AssetId = dto.AssetId,
                FromUserId = dto.FromUserId,
                ToUserId = dto.ToUserId,
                FromLocationId = dto.FromLocationId,
                ToLocationId = dto.ToLocationId,
                TransferDate = dto.TransferDate,
                Status = TransferStatus.Pending // Requires approval from the new user
            };

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                await UnitOfWork.writeRepository<AssetTransferRecords>().AddAsync(transferRecord);
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();

                return Mapper.Map<GetAssetTransferRecordResponseDTO>(transferRecord);
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
            var transferRecord = await UnitOfWork.readRepository<AssetTransferRecords>().GetAsync(r => r.Id == transferId,enableTracing:false);
            if (transferRecord == null) throw new KeyNotFoundException("Transfer record not found.");
            if (transferRecord.Status != TransferStatus.Pending) throw new InvalidOperationException("Transfer is not in a pending state.");

            transferRecord.Status = TransferStatus.Approved;
            transferRecord.ApprovalDate = DateTime.UtcNow;

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.Id == transferRecord.AssetId,enableTracing:false);
                if (asset == null) throw new KeyNotFoundException("Asset not found.");

                if (transferRecord.IsUserTransfer)
                {
                    asset.AssignedUserId = transferRecord.ToUserId;
                }
                asset.LocationId = transferRecord.ToLocationId;

                await UnitOfWork.writeRepository<AssetTransferRecords>().UpdateAsync(transferRecord.Id, transferRecord);

                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.writeRepository<Asset>().UpdateAsync(asset.Id, asset);
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

        #region Get Asset Transfer Record by ID
        public async Task<GetAssetTransferRecordResponseDTO> GetAssetTransferByIdAsync(int id)
        {
            var transferRecord = await UnitOfWork.readRepository<AssetTransferRecords>().GetAsync(tr => tr.Id == id);
            if (transferRecord == null) throw new KeyNotFoundException("Transfer record not found.");

            return Mapper.Map<GetAssetTransferRecordResponseDTO, AssetTransferRecords>(transferRecord);
        }
        #endregion

        #region Get All Asset Transfers
        public async Task<IList<GetAssetTransferRecordResponseDTO>> GetAllAssetTransfersAsync()
        {
            var transferRecords = await UnitOfWork.readRepository<AssetTransferRecords>().GetAllAsync();
            return Mapper.Map< GetAssetTransferRecordResponseDTO,AssetTransferRecords>(transferRecords);
        }
        #endregion

        #region Update Location to Location Transfer
        public async Task<GetAssetTransferRecordResponseDTO> UpdateLocationToLocationTransferAsync(int id, LocationToLocationTransferDTO dto)
        {
            var transferRecord = await UnitOfWork.readRepository<AssetTransferRecords>().GetAsync(r => r.Id == id);
            if (transferRecord == null) throw new KeyNotFoundException("Transfer record not found.");

            ValidateLocationTransfer(dto.FromLocationId, dto.ToLocationId);

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                transferRecord.FromLocationId = dto.FromLocationId;
                transferRecord.ToLocationId = dto.ToLocationId;
                transferRecord.TransferDate = dto.TransferDate;
                transferRecord.Status = TransferStatus.Approved; // Directly approve location transfers

                var asset = await UnitOfWork.readRepository<Asset>().GetAsync(a => a.Id == dto.AssetId);
                asset.LocationId = dto.ToLocationId;

                await UnitOfWork.writeRepository<Asset>().UpdateAsync(asset.Id, asset);
                await UnitOfWork.writeRepository<AssetTransferRecords>().UpdateAsync(transferRecord.Id, transferRecord);
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();

                return Mapper.Map<GetAssetTransferRecordResponseDTO>(transferRecord);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        #endregion

        #region Update User to User Transfer
        public async Task<GetAssetTransferRecordResponseDTO> UpdateUserToUserTransferAsync(int id, UserToUserTransferDTO dto)
        {
            var transferRecord = await UnitOfWork.readRepository<AssetTransferRecords>().GetAsync(r => r.Id == id);
            if (transferRecord == null) throw new KeyNotFoundException("Transfer record not found.");

            ValidateUserTransfer(dto.FromUserId, dto.ToUserId);

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                transferRecord.FromUserId = dto.FromUserId;
                transferRecord.ToUserId = dto.ToUserId;
                transferRecord.TransferDate = dto.TransferDate;
                transferRecord.Status = TransferStatus.Pending; // Pending approval

                await UnitOfWork.writeRepository<AssetTransferRecords>().UpdateAsync(transferRecord.Id, transferRecord);
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();

                return Mapper.Map<GetAssetTransferRecordResponseDTO>(transferRecord);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        #endregion

        #region Update User and Location Transfer
        public async Task<GetAssetTransferRecordResponseDTO> UpdateUserAndLocationTransferAsync(int id, UserAndLocationTransferDTO dto)
        {
            var transferRecord = await UnitOfWork.readRepository<AssetTransferRecords>().GetAsync(r => r.Id == id);
            if (transferRecord == null) throw new KeyNotFoundException("Transfer record not found.");

            ValidateUserTransfer(dto.FromUserId, dto.ToUserId);
            ValidateLocationTransfer(dto.FromLocationId, dto.ToLocationId);

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                transferRecord.FromUserId = dto.FromUserId;
                transferRecord.ToUserId = dto.ToUserId;
                transferRecord.FromLocationId = dto.FromLocationId;
                transferRecord.ToLocationId = dto.ToLocationId;
                transferRecord.TransferDate = dto.TransferDate;
                transferRecord.Status = TransferStatus.Pending; // Pending approval

                await UnitOfWork.writeRepository<AssetTransferRecords>().UpdateAsync(transferRecord.Id, transferRecord);
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();

                return Mapper.Map<GetAssetTransferRecordResponseDTO>(transferRecord);
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

        #region Private Methods

        private void ValidateLocationTransfer(int fromLocationId, int toLocationId)
        {
            if (fromLocationId == toLocationId)
                throw new InvalidOperationException("Cannot transfer asset to the same location.");
        }

        private void ValidateUserTransfer(Guid fromUserId, Guid toUserId)
        {
            if (fromUserId == toUserId)
                throw new InvalidOperationException("Cannot transfer asset to the same user.");
        }

        #endregion
    }
}
