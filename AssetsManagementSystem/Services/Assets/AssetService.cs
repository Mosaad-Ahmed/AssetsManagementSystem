﻿namespace AssetsManagementSystem.Services.Assets
{
    public class AssetService : BaseClassForServices
    {
        public AssetService(IUnitOfWork unitOfWork, Others.Interfaces.IAutoMapper.IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        #region Add Asset
        public async Task<GetAssetResponseDTO> AddAssetAsync(AddAssetRequestDTO addAssetDto)
        {
            var existingAsset = await UnitOfWork.readRepository<Asset>()
                .GetAsync(a => a.SerialNumber == addAssetDto.SerialNumber);

            if (existingAsset != null)
            {
                throw new InvalidOperationException("An asset with the same serial number already exists.");
            }

            // Validate foreign keys and category-subcategory relation
            await ValidateForeignKeysAndCategoryRelationAsync(addAssetDto);

            var asset = Mapper.Map<Asset, AddAssetRequestDTO>(addAssetDto);
            asset.AddedOnDate = DateTime.Now;

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                asset.ManufacturerId = 1;
                await UnitOfWork.writeRepository<Asset>().AddAsync(asset);
           
                await UnitOfWork.SaveChangeAsync();

                // Add related records in AssetsSuppliers table
                await AddOrUpdateAssetSuppliers(asset.Id, addAssetDto.SupplierIds);

                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }

            return await GetAssetByIdAsync(asset.Id);
        }
        #endregion

        #region Get Asset by ID
        public async Task<GetAssetResponseDTO> GetAssetByIdAsync(int id)
        {
            var asset = await UnitOfWork.readRepository<Asset>()
                .GetAsync(a => a.Id == id && 
                (a.IsDeleted == false || a.IsDeleted == null)&&
                a.Status!=AssetStatus.Retired.ToString());

            if (asset == null)
            {
                throw new KeyNotFoundException("Asset not found.");
            }

            var getAssetResponseDTO = new GetAssetResponseDTO
            {
                Id = asset.Id,
                Name = asset.Name,
                ModelNumber = asset.ModelNumber,
                SerialNumber = asset.SerialNumber,
                PurchaseDate = asset.PurchaseDate,
                PurchasePrice = asset.PurchasePrice,
                dicription=asset.dicription,
                WarrantyExpiryDate = asset.WarrantyExpiryDate,
                Status = asset.Status,
                LocationName = asset.Location.Name,
                AssignedUserName = string.Concat(asset.AssignedUser.FirstName, " ", asset.AssignedUser.LastName),
                CategoryName = asset.Category.Name,
                SubCategoryName = asset.AssetSubCategory.Name,
                SupplierNames = asset.AssetsSuppliers.Select(s => s.Supplier.Name).ToList(),
                AddedOnDate = asset.AddedOnDate,
                UpdatedDate = asset.UpdatedDate
            };

            return getAssetResponseDTO;

        }
        #endregion

        #region Get All Assets
        public async Task<IList<GetAssetResponseDTO>> GetAllAssetsAsync()
        {
            var assets = await UnitOfWork.readRepository<Asset>()
                .GetAllAsync(a => a.IsDeleted == false || a.IsDeleted == null);
             

            var getAssetResponseDTO = assets.Select(a=>new GetAssetResponseDTO() 
            {
                Id = a.Id,
                Name = a.Name,
                ModelNumber = a.ModelNumber,
                SerialNumber = a.SerialNumber,
                PurchaseDate = a.PurchaseDate,
                PurchasePrice = a.PurchasePrice,
                WarrantyExpiryDate = a.WarrantyExpiryDate,
                Status = a.Status,
                dicription = a.dicription,
                LocationName = a.Location.Name, 
                AssignedUserName =string.Concat(a.AssignedUser.FirstName," ",a.AssignedUser.LastName),
                CategoryName = a.Category?.Name, 
                SubCategoryName=a.AssetSubCategory?.Name,
                SupplierNames = a.AssetsSuppliers.Select(s => s.Supplier.Name).ToList(),  
                AddedOnDate = a.AddedOnDate,
                UpdatedDate = a.UpdatedDate
            }).ToList();

            return getAssetResponseDTO;
        }
        #endregion

        #region Update Asset
        public async Task<GetAssetResponseDTO> UpdateAssetAsync(int id, UpdateAssetRequestDTO updateAssetDto)
        {
            await UnitOfWork.BeginTransactionAsync();
            try
            {
                var existingAsset = await UnitOfWork.readRepository<Asset>()
                    .GetAsync(a => a.Id == id && 
                    (a.IsDeleted == false || a.IsDeleted == null)&&
                    a.Status!=AssetStatus.Retired.ToString());

                if (existingAsset == null)
                {
                    throw new KeyNotFoundException("Asset not found.");
                }

                #region Serial Number Not Updatable
                //var anotherAsset = await UnitOfWork.readRepository<Asset>()
                //    .GetAsync(a => a.SerialNumber == updateAssetDto.SerialNumber && a.Id != id);

                //if (anotherAsset != null)
                //{
                //    throw new InvalidOperationException("Another asset with the same serial number already exists.");
                //}
                #endregion

                // Validate foreign keys and category-subcategory relation
                await ValidateForeignKeysAndCategoryRelationAsync(updateAssetDto);

                #region Manually assign properties from the DTO to the existing asset
                existingAsset.Name = updateAssetDto.Name;
                existingAsset.ModelNumber = updateAssetDto.ModelNumber;
                existingAsset.SerialNumber = existingAsset.SerialNumber;
                existingAsset.PurchaseDate = updateAssetDto.PurchaseDate;
                existingAsset.PurchasePrice = updateAssetDto.PurchasePrice;
                existingAsset.WarrantyExpiryDate = updateAssetDto.WarrantyExpiryDate;
                existingAsset.Status = updateAssetDto.Status.ToString();
                existingAsset.LocationId = updateAssetDto.LocationId;
                existingAsset.AssignedUserId = updateAssetDto.AssignedUserId;
                existingAsset.CategoryId = updateAssetDto.CategoryId;
                existingAsset.SubCategoryId = updateAssetDto.SubCategoryId;
                #endregion


                existingAsset.UpdatedDate = DateTime.Now;

                await UnitOfWork.writeRepository<Asset>().UpdateAsync(id, existingAsset);
                await UnitOfWork.SaveChangeAsync();

                // Delete old supplier associations and add the new ones
                await DeleteAssetSuppliers(existingAsset.Id);
                await AddOrUpdateAssetSuppliers(existingAsset.Id, updateAssetDto.SupplierIds);

                await UnitOfWork.CommitTransactionAsync();

                return await GetAssetByIdAsync(existingAsset.Id);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        #endregion

        #region Delete Asset  
        public async Task DeleteAssetAsync(int id)
        {
            var existingAsset = await UnitOfWork.readRepository<Asset>()
                .GetAsync(a => a.Id == id && (a.IsDeleted == false || a.IsDeleted == null));

            if (existingAsset == null)
            {
                throw new KeyNotFoundException("Asset not found or already deleted.");
            }

            existingAsset.IsDeleted = true;
            existingAsset.DeletedDate = DateTime.Now;
            existingAsset.Status=AssetStatus.Retired.ToString();

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                await UnitOfWork.writeRepository<Asset>().UpdateAsync(id, existingAsset);
               await DeleteAssetSuppliers (existingAsset.Id);
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

         
        #region Add or update supplier associations
        private async Task AddOrUpdateAssetSuppliers(int assetId, ICollection<int> supplierIds)
        {
            foreach (var supplierId in supplierIds)
            {
                var assetSupplier = new AssetsSuppliers
                {
                    AssetId = assetId,
                    SupplierId = supplierId
                };

                await UnitOfWork.writeRepository<AssetsSuppliers>().AddAsync(assetSupplier);
            }

            await UnitOfWork.SaveChangeAsync();
        }
        #endregion

        #region Delete existing asset-supplier relationships
        private async Task DeleteAssetSuppliers(int assetId)
        {
            var existingAssetSuppliers = await UnitOfWork.readRepository<AssetsSuppliers>()
                .GetAllAsync(As => As.AssetId == assetId);

            await UnitOfWork.writeRepository<AssetsSuppliers>().DeleteRangeAsync(existingAssetSuppliers);
            await UnitOfWork.SaveChangeAsync();
        }
        #endregion

        #region Validate foreign keys and ensure that the SubCategory belongs to the correct MainCategory
        private async Task ValidateForeignKeysAndCategoryRelationAsync(IAssetDTO assetDto)
        {
            if (await UnitOfWork.readRepository<Location>().GetAsync(l => l.Id == assetDto.LocationId 
            && (l.IsDeleted == false || l.IsDeleted==null)) == null)
            {
                throw new KeyNotFoundException("Location not found.");
            }

            if (await UnitOfWork.readRepository<User>().GetAsync(u => u.Id == assetDto.AssignedUserId
            && (u.IsDeleted == false || u.IsDeleted == null)) == null)
            {
                throw new KeyNotFoundException("Assigned user not found.");
            }

            if (await UnitOfWork.readRepository<Category>().GetAsync(c => c.Id == assetDto.CategoryId
            && (c.IsDeleted == false || c.IsDeleted == null)) == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            var subCategory = await UnitOfWork.readRepository<Models.DbSets.SubCategory>().GetAsync(sc => sc.Id == assetDto.SubCategoryId
            && (sc.IsDeleted == false || sc.IsDeleted == null));
            if (subCategory == null)
            {
                throw new KeyNotFoundException("SubCategory not found.");
            }

            if (subCategory.MainCategoryId != assetDto.CategoryId)
            {
                throw new InvalidOperationException("SubCategory does not belong to the selected MainCategory.");
            }

            foreach (var supplierId in assetDto.SupplierIds)
            {
                if (await UnitOfWork.readRepository<Supplier>().GetAsync(s => s.Id == supplierId 
                && (s.IsDeleted == false || s.IsDeleted == null)) == null)
                {
                    throw new KeyNotFoundException($"Supplier with ID {supplierId} not found.");
                }
            }
        }
        #endregion
 

    }
}
