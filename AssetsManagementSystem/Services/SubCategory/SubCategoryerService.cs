
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

              var subCategory = Mapper.Map<Models.DbSets.SubCategory, AddSubCategoryRequestDTO>(addSubCategory);
            subCategory.AddedOnDate=DateTime.Now;
            await UnitOfWork.writeRepository<Models.DbSets.SubCategory>().AddAsync(subCategory);
            await UnitOfWork.SaveChangeAsync();

             return Mapper.Map<GetSubCategoryRequestDTO, Models.DbSets.SubCategory>(subCategory);
        }

         public async Task<GetSubCategoryRequestDTO> UpdateSubCategoryAsync(int id, UpdateSubCategoryRequestDTO dto)
        {
            
                var existingSubCategory = await UnitOfWork.readRepository<Models.DbSets.SubCategory>()
                .GetAsync(sc => sc.Id == id);

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

             existingSubCategory.IsDeleted = true;
            existingSubCategory.DeletedDate = DateTime.UtcNow;

             await UnitOfWork.writeRepository < Models.DbSets.SubCategory > ()
                .UpdateAsync(existingSubCategory.Id, existingSubCategory);

             await UnitOfWork.SaveChangeAsync();
        }

        public async Task<GetSubCategoryRequestDTO> GetSubCategoryByIdAsync(int id)
        {
            var subCategory = await UnitOfWork.readRepository <Models.DbSets.SubCategory > ().GetAsync(sc => sc.Id == id);

            if (subCategory == null)
            {
                throw new KeyNotFoundException("SubCategory not found.");
            }

             return Mapper.Map<GetSubCategoryRequestDTO,Models.DbSets.SubCategory>(subCategory);
        }

        public async Task<IList<GetSubCategoryRequestDTO>> GetAllSubCategoriesAsync()
        {
            var subCategories = await UnitOfWork.readRepository<Models.DbSets.SubCategory>().GetAllAsync();

             return Mapper.Map<GetSubCategoryRequestDTO, Models.DbSets.SubCategory>(subCategories);
        }






    }
}
