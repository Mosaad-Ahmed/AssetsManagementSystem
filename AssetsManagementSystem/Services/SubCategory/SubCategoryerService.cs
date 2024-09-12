
using AssetsManagementSystem.DTOs.CategoryDTOs;
using AssetsManagementSystem.DTOs.SubCategoryDTOs;
using AssetsManagementSystem.Models.DbSets;

namespace AssetsManagementSystem.Services.SubCategory
{
    public class SubCategoryerService : BaseClassForServices
    {
        public SubCategoryerService(IUnitOfWork unitOfWork, Others.Interfaces.IAutoMapper.IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public async Task<GetSubCategoryRequestDTO> AddSubCategory(AddSubCategoryRequestDTO addSubCategory)
        {
             var existingSubCategory = await UnitOfWork.readRepository<Models.DbSets.SubCategory>()
                .GetAsync(sc => sc.Name.ToLower() == addSubCategory.Name.ToLower());

            if (existingSubCategory != null)
            {
                throw new InvalidOperationException("SubCategory with the same name already exists.");
            }

           var category= await UnitOfWork.readRepository<Category>().GetAsync(predicate:c=> c.Id == addSubCategory.MainCategoryId &&
                                                                                            c.IsDeleted == true);

            if (category is not null)
                throw new InvalidOperationException("Main Category Has deleted..use other main category");


            var subCategory = Mapper.Map<Models.DbSets.SubCategory, AddSubCategoryRequestDTO>(addSubCategory);
            subCategory.AddedOnDate=DateTime.Now;
            await UnitOfWork.writeRepository<Models.DbSets.SubCategory>().AddAsync(subCategory);
            await UnitOfWork.SaveChangeAsync();

             return await GetSubCategoryByIdAsync(subCategory.Id);
        }

         public async Task<GetSubCategoryRequestDTO> UpdateSubCategoryAsync(int id, UpdateSubCategoryRequestDTO dto)
        {
            
                var existingSubCategory = await UnitOfWork.readRepository<Models.DbSets.SubCategory>()
                .GetAsync(sc => sc.Id == id && (sc.IsDeleted == false || sc.IsDeleted == null));

                if (existingSubCategory == null)
                {
                    throw new KeyNotFoundException("SubCategory not found.");
                }

                var anotherSubCategory = await UnitOfWork.readRepository<Models.DbSets.SubCategory>()
                   .GetAsync(sc => sc.Name.ToLower() == dto.Name.ToLower() && sc.Id != id);

                if (anotherSubCategory != null)
                {
                    throw new InvalidOperationException("Another SubCategory with the same name already exists.");
                }

                existingSubCategory.Name = dto.Name;
                existingSubCategory.MainCategoryId = dto.MainCategoryId;
                existingSubCategory.UpdatedDate = DateTime.Now;
                existingSubCategory.AddedOnDate = existingSubCategory.AddedOnDate;


                await UnitOfWork.writeRepository<Models.DbSets.SubCategory>().UpdateAsync(id, existingSubCategory);
                await UnitOfWork.SaveChangeAsync();
             
                 return Mapper.Map<GetSubCategoryRequestDTO, Models.DbSets.SubCategory>(existingSubCategory);
             
            
        }

        public async Task  DeleteSubCategory(int subCategoryId)
        {
             var existingSubCategory = await UnitOfWork.readRepository<Models.DbSets.SubCategory>()
                .GetAsync(sc => sc.Id == subCategoryId && (sc.IsDeleted==false||sc.IsDeleted==null));

            if (existingSubCategory == null)
            {
                throw new KeyNotFoundException("SubCategory not found or already deleted.");
            }
            var asset = await UnitOfWork.readRepository<Asset>()
                             .GetAsync(A => A.SubCategoryId == subCategoryId && (A.IsDeleted == false || A.IsDeleted == null));

            if (asset is not null)
            {
                throw new InvalidOperationException("There are Assets dependent on this SubCategory,Please Go and delete it first");
            }

            existingSubCategory.IsDeleted = true;
            existingSubCategory.DeletedDate = DateTime.Now;

             await UnitOfWork.writeRepository < Models.DbSets.SubCategory > ()
                .UpdateAsync(existingSubCategory.Id, existingSubCategory);

             await UnitOfWork.SaveChangeAsync();
        }

        public async Task<GetSubCategoryRequestDTO> GetSubCategoryByIdAsync(int id)
        {
            var subCategory = await UnitOfWork.readRepository <Models.DbSets.SubCategory > ()
                .GetAsync(sc => sc.Id == id && (sc.IsDeleted == false || sc.IsDeleted == null));

            if (subCategory == null)
            {
                throw new KeyNotFoundException("SubCategory not found.");
            }
            var getSubCategoryRequestDTO = Mapper.Map<GetSubCategoryRequestDTO, Models.DbSets.SubCategory>(subCategory);

            getSubCategoryRequestDTO.MainCategoryName = subCategory.MainAssetCategory.Name;

            return getSubCategoryRequestDTO;
        }

        public async Task<IList<GetSubCategoryRequestDTO>> GetAllSubCategoriesAsync()
        {
            var subCategories = await UnitOfWork.readRepository<Models.DbSets.SubCategory>()
                .GetAllAsync(predicate:sc =>(sc.IsDeleted == false || sc.IsDeleted == null));

            var getSubCategoryRequestDTO = Mapper.Map<GetSubCategoryRequestDTO, Models.DbSets.SubCategory>(subCategories);

             
            foreach (var item in getSubCategoryRequestDTO)
            {
                item.MainCategoryName =   subCategories.Where(sc => item.MainCategoryId == sc.MainAssetCategory.Id)
                                            .Select(sc => sc.MainAssetCategory.Name).FirstOrDefault();
 
            }
             
            return getSubCategoryRequestDTO;
        }

        public async Task<IList<GetSubCategoryRequestDTO>> GetAllByPaginationSubCategoriesAsync(int currentPage = 1, int pageSize = 10)
        {
            var subCategories = await UnitOfWork.readRepository<Models.DbSets.SubCategory>()
                .GetAllByPagningAsync(predicate: sc => (sc.IsDeleted == false || sc.IsDeleted == null), pageSize: pageSize, currentPage: currentPage);

            var getSubCategoryRequestDTO = Mapper.Map<GetSubCategoryRequestDTO, Models.DbSets.SubCategory>(subCategories);


            foreach (var item in getSubCategoryRequestDTO)
            {
                item.MainCategoryName = subCategories.Where(sc => item.MainCategoryId == sc.MainAssetCategory.Id)
                                            .Select(sc => sc.MainAssetCategory.Name).FirstOrDefault();

            }

            return getSubCategoryRequestDTO;
        }




    }
}
