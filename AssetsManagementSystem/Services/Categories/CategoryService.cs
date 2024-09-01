﻿using AssetsManagementSystem.DTOs.CategoryDTOs;
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

             await UnitOfWork.writeRepository<Category>().AddAsync(category);

             await UnitOfWork.SaveChangeAsync();
        }
        #endregion

        #region Retrieve a category by ID
        public async Task<GetCategoryRequestDTO> GetCategoryByIdAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("Invalid category ID.");
            }

            var category = await UnitOfWork.readRepository<Category>().GetAsync(c => c.Id == categoryId);

            var getCategoryRequestDTO = Mapper.Map<GetCategoryRequestDTO,Category>(category);


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
            var categories= await UnitOfWork.readRepository<Category>().GetAllAsync();

            var getCategoryRequestDTOs = Mapper.Map<GetCategoryRequestDTO, Category>(categories);

            return getCategoryRequestDTOs;

        }
        #endregion

        #region Update a category
        public async Task UpdateCategoryAsync(int categoryId, UpdateCategoryRequestDTO updateCategoryRequest)
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

            var category = await UnitOfWork.readRepository<Category>().GetAsync(c => c.Id == categoryId);

            
             
            category.IsDeleted = true;
            category.DeletedDate = DateTime.Now;
            //category.AddedOnDate = category.AddedOnDate;
            //category.UpdatedDate = category.AddedOnDate;

             await UnitOfWork.writeRepository<Category>().UpdateAsync(categoryId,category);
             await UnitOfWork.SaveChangeAsync();
        }
        #endregion
    }
}
