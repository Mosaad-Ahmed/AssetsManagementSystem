using AssetsManagementSystem.DTOs.AssetDTOs;
using AssetsManagementSystem.Services.AssetDisposal;
using AssetsManagementSystem.Services.AssetMaintenance;
using AssetsManagementSystem.Services.Assets;
using AssetsManagementSystem.Services.AssetTransfer;
using AssetsManagementSystem.Services.Categories;
using AssetsManagementSystem.Services.DataConsistencyCheck;
using AssetsManagementSystem.Services.Document;
using AssetsManagementSystem.Services.Locations;
using AssetsManagementSystem.Services.ReceiveMaintainedAsset;
using AssetsManagementSystem.Services.SubCategory;
using AssetsManagementSystem.Services.Suppliers;

namespace AssetsManagementSystem.Services
{
    public static class Registeration
    {
        public static void AddFolderOfServices(this IServiceCollection services)
        {
             services.AddScoped<AuthRules>();

             services.AddScoped<Accounting>();

             services.AddScoped<LocationService>();

            services.AddScoped<CategoryService>();

            services.AddScoped<SupplierService>();

            services.AddScoped<AssetService>();

            services.AddScoped<AssetMaintenanceService>();

            services.AddScoped<AssetDisposalService>();

            services.AddScoped<SubCategoryerService>();

            services.AddScoped<AssetTransferService>();

            services.AddScoped<DocumentService>();

            services.AddScoped<ReceiveMaintainedAssetService>();

            services.AddScoped<DataConsistencyCheckService>();
        }
    }
}
