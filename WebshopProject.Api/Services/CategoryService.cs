﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Repository;

namespace WebshopProject.Api.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategories();

        Task<CategoryResponse> GetCategoryById(int categoryId);
        Task<List<CategoryResponse>> GetAllCategoriesWithoutProducts();
        Task<CategoryResponse> CreateCategory(CategoryRequest newCategory);
        Task<CategoryResponse> UpdateCategory(int categoryId, CategoryRequest updateCategory);
        Task<CategoryResponse> DeleteCategory(int categoryId);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            List<Category> categories = await _categoryRepository.SelectAllCategories();

            if (categories != null)
            {
                return categories.Select(category => MapCategoryToCategoryResponse(category)).ToList();
            }

            return null;
        }
        public async Task<CategoryResponse> GetCategoryById(int categoryId)
        {
            Category category = await _categoryRepository.SelectCategoryById(categoryId);

            if (category != null)
            {

                return MapCategoryToCategoryResponse(category);
            }
            return null;
        }
        public async Task<List<CategoryResponse>> GetAllCategoriesWithoutProducts()
        {
            List<Category> categories = await _categoryRepository.SelectAllCategoriesWithoutProducts( );

            if (categories != null)
            {
                return categories.Select(category => MapCategoryToCategoryResponse(category)).ToList();
            }
            return null;
        }

        public async Task<CategoryResponse> CreateCategory(CategoryRequest newCategory)
        {
            Category category = MapCategoryRequestToCategory(newCategory);

            Category insertedCategory = await _categoryRepository.InsertNewCategory(category);

            if (insertedCategory != null)
            {
                return MapCategoryToCategoryResponse(insertedCategory);

            }
            return null;
        }

        public async Task<CategoryResponse> UpdateCategory(int categoryId, CategoryRequest updateCategory)
        {
            Category category = MapCategoryRequestToCategory(updateCategory);

            Category updatedCategory = await _categoryRepository.UpdateExistingCategory(categoryId, category);

            if (updatedCategory != null)
            {
                return MapCategoryToCategoryResponse(updatedCategory);
            }
            return null;
        }

        public async Task<CategoryResponse> DeleteCategory(int categoryId)
        {
            Category deletedCategory = await _categoryRepository.DeleteCategoryById(categoryId);

            if (deletedCategory != null)
            {
                return MapCategoryToCategoryResponse(deletedCategory);
            }
            return null;
        }
        public static Category MapCategoryRequestToCategory(CategoryRequest category)
        {
            return new Category()
            {
                CategoryName = category.CategoryName,
                
            };
        }
        private CategoryResponse MapCategoryToCategoryResponse(Category categories)
        {
            return new CategoryResponse
            {
                Id = categories.Id,
                CategoryName = categories.CategoryName,
                Products = categories.Products.Select(category => new CategoryProductResponse
                {
                    Id = category.Id,
                    Title = category.Title,
                    Price = category.Price,
                    Description = category.Description,
                    Image = category.Image,
                    Stock = category.Stock
                   
                }).ToList()
            };

        }
  
    }
}
