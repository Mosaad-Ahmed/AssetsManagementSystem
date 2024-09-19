using AssetsManagementSystem.DTOs.CategoryDTOs;
using AssetsManagementSystem.Models.DbSets;

namespace AssetsManagementSystem.Services.Categories
{
    public class CategoryService:BaseClassForServices
    {
        public CategoryService(IUnitOfWork unitOfWork,
            Others.Interfaces.IAutoMapper.IMapper mapper,
            IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        #region Adding a new category
        public async Task AddCategoryAsync(AddCategoryRequestDTO addCategoryRequest)
        {
            if (addCategoryRequest == null)
            {
                throw new ArgumentNullException(nameof(addCategoryRequest), "Category details cannot be null.");
            }

             var existingCategory = await UnitOfWork.readRepository<Category>()
                                        .GetAsync(c => c.Name == addCategoryRequest.Name);

            if (existingCategory != null)
            {
                throw new InvalidOperationException("A category with the same name already exists.");
            }

              var category = Mapper.Map<Category,AddCategoryRequestDTO>(addCategoryRequest);
           
              category.AddedOnDate=DateTime.Now;

            var categoryforId = addCategoryRequest.ParentCategoryId == 0 ? null :
                await UnitOfWork.readRepository<Category>().GetAsync(c => c.SerialCode == (addCategoryRequest.ParentCategoryId).ToString());

             category.ParentCategoryId=categoryforId?.Id;


             await UnitOfWork.writeRepository<Category>().AddAsync(category);

             await UnitOfWork.SaveChangeAsync();
        }
        #endregion

        #region GetMainCategory

        public async Task<IList<GetCategoryRequestDTO>> GetMainCategory() 
        {
            var categories=await UnitOfWork.readRepository<Category>().GetAllAsync(predicate:c=>(c.ParentCategoryId==null||c.ParentCategoryId==0));

            var getCategoriesRequestDTOs = Mapper.Map<GetCategoryRequestDTO, Category>(categories);

            return getCategoriesRequestDTOs;
            
        }

        #endregion



        #region GetSubCategory
        public async Task<IList<GetCategoryRequestDTO>> GetSubCategory(int parentId)
        {
            var categories = await UnitOfWork.readRepository<Category>().GetAllAsync(predicate: c => (c.IsDeleted == null || c.IsDeleted == false)
                                                                            && c.ParentCategoryId==parentId);

            var getCategoriesRequestDTOs = Mapper.Map<GetCategoryRequestDTO, Category>(categories);
           

            return getCategoriesRequestDTOs;

        }

        #endregion



        #region Retrieve a category by ID
        public async Task<GetCategoryRequestDTO> GetCategoryByIdAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("Invalid category ID.");
            }

            var category = await UnitOfWork.readRepository<Category>()
                .GetAsync(c => c.Id == categoryId && (c.IsDeleted == false || c.IsDeleted == null));

            var getCategoryRequestDTO = Mapper.Map<GetCategoryRequestDTO,Category>(category);
            getCategoryRequestDTO.ParentCategoryName = category.ParentCategory.Name;

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            return getCategoryRequestDTO;
        }
        #endregion

        #region Retrieve all categories
        public async Task<IEnumerable<GetCategoryRequestDTO>> GetAllCategoriesAsync()
        {
            var categories= await UnitOfWork.readRepository<Category>()
                .GetAllAsync(predicate: c=> (c.IsDeleted == false || c.IsDeleted == null));

            var getCategoryRequestDTOs = categories.Select
                (
                c=>new GetCategoryRequestDTO()
                {
                    Id= c.Id,   
                    Name= c.Name,
                    SerialCode= c.SerialCode,
                    ParentCategoryId= c.ParentCategoryId==null?null:c.ParentCategoryId,
                    ParentCategoryName=c.ParentCategoryId==null?null:c.ParentCategory.Name,
                    AddedOnDate=c.AddedOnDate,
                    UpdatedDate=c.UpdatedDate
                }
                );
            return getCategoryRequestDTOs;
            
        }
        #endregion

        #region Retrieve all categories
        public async Task<IEnumerable<GetCategoryRequestDTO>> GetAllByPaginationCategoriesAsync(int currentPage = 1, int pageSize = 10)
        {
            var categories = await UnitOfWork.readRepository<Category>()
                .GetAllByPagningAsync(predicate: c => (c.IsDeleted == false || c.IsDeleted == null), pageSize: pageSize, currentPage: currentPage);

            var getCategoryRequestDTOs = Mapper.Map<GetCategoryRequestDTO, Category>(categories);

            return getCategoryRequestDTOs;

        }
        #endregion 

        #region Update a category
        public async Task UpdateCategoryAsync(int categoryId, UpdateCategoryRequestDTO
            updateCategoryRequest)
        {
            if (updateCategoryRequest == null)
            {
                throw new ArgumentNullException(nameof(updateCategoryRequest), "Category details cannot be null.");
            }

            if (categoryId <= 0)
            {
                throw new ArgumentException("Invalid category ID.");
            }

            var category = await UnitOfWork.readRepository<Category>().GetAsync(c => c.Id == categoryId 
                                                                        && (c.IsDeleted == null || c.IsDeleted ==false )
                                                                        );

 
             var existingCategory = await UnitOfWork.readRepository<Category>()
                                        .GetAsync(c => c.Name == updateCategoryRequest.Name && c.Id != categoryId);

            if (existingCategory != null)
            {
                throw new InvalidOperationException("Another category with the same name already exists.");
            }

             category.Name = updateCategoryRequest.Name;
             category.Description = updateCategoryRequest.Description;
             category.UpdatedDate = DateTime.Now;
             category.AddedOnDate = category.AddedOnDate;

             await UnitOfWork.writeRepository<Category>().UpdateAsync(category.Id, category);

             await UnitOfWork.SaveChangeAsync();
        }
        #endregion

        #region Delete a category
        public async Task DeleteCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("Invalid category ID.");
            }

            var category = await UnitOfWork.readRepository<Category>()
                                .GetAsync(c => c.Id == categoryId && (c.IsDeleted == false || c.IsDeleted == null) );



            var asset = await UnitOfWork.readRepository<Asset>()
                               .GetAsync(A => A.CategoryId == categoryId && (A.IsDeleted == false || A.IsDeleted == null));

            if (asset is not null)
            {
                throw new InvalidOperationException("There are Assets dependent on this Category,Please Go and delete it first");
            }
             
            category.IsDeleted = true;
            category.DeletedDate = DateTime.Now;
       

             await UnitOfWork.writeRepository<Category>().UpdateAsync(categoryId,category);
             await UnitOfWork.SaveChangeAsync();
        }
        #endregion
    }
}

