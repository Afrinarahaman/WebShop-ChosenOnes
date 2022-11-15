using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Repository;

namespace WebshopProject.Api.Services
{

    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse> GetProductById(int productId);
        Task<List<ProductResponse>> GetProductsByCategoryId(int categoryId);
        Task<ProductResponse> CreateProduct(ProductRequest newProduct);
        Task<ProductResponse> UpdateProduct(int ProductId, ProductRequest updateProduct);
        Task<ProductResponse> DeleteProduct(int productId);

    }
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;



        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository=categoryRepository;
        }
        public async Task<List<ProductResponse>> GetAllProducts()
        {
            List<Product> products = await _productRepository.SelectAllProducts();

            return products.Select(product => MapProductToProductResponse(product)).ToList();

        }
        public async Task<ProductResponse> GetProductById(int productId)
        {
            Product product = await _productRepository.SelectProductById(productId);

            if (product != null)
            {

                return MapProductToProductResponse(product);
            }
            return null;
        }

        public async Task<List<ProductResponse>> GetProductsByCategoryId(int categoryId)
        {
            List < Product >  products = await _productRepository.GetProductsByCategoryId(categoryId);


            return products.Select(product => MapProductToProductResponse(product)).ToList();
        }



        public async Task<ProductResponse> CreateProduct(ProductRequest newProduct)
        {
            Product product = MapProductRequestToProduct(newProduct);

            Product insertedProduct = await _productRepository.InsertNewProduct(product);

            if (insertedProduct != null)
            {
                insertedProduct.Category = await _categoryRepository.SelectCategoryById(insertedProduct.CategoryId);
                return MapProductToProductResponse(insertedProduct);
            }

            return null;
        }

        public async Task<ProductResponse> UpdateProduct(int productId, ProductRequest updateProduct)
        {
            Product product = MapProductRequestToProduct(updateProduct);

            Product updatedProduct = await _productRepository.UpdateExistingProduct(productId, product);

            if (updatedProduct != null)
            {
                updatedProduct.Category = await _categoryRepository.SelectCategoryById(updatedProduct.CategoryId);
                return MapProductToProductResponse(updatedProduct);
            }

            return null;
        }

        public async Task<ProductResponse> DeleteProduct(int productId)
        {
            Product product = await _productRepository.DeleteProductById(productId);

            if (product != null)
            {
                product.Category = await _categoryRepository.SelectCategoryById(product.CategoryId);
                return MapProductToProductResponse(product);
            }

            return null;
        }


        private ProductResponse MapProductToProductResponse(Product product)
        {

            return new ProductResponse
            {
                Id = product.Id,
                Title=product.Title,
                Price=product.Price,
                Description=product.Description,
                Image=product.Image,
                Stock=product.Stock,
                CategoryId=product.CategoryId,

                Category = new ProductCategoryResponse
                {
                    Id = product.Category.Id,
                    CategoryName = product.Category.CategoryName

                }
            };
        }

        private static Product MapProductRequestToProduct(ProductRequest productRequest)
        {
            return new Product()
            {
                
                Title = productRequest.Title,
                Price = productRequest.Price,
                Description = productRequest.Description,
                Image=productRequest.Image,
                Stock=productRequest.Stock,
                CategoryId=productRequest.CategoryId
            };
        }
    }


    }
