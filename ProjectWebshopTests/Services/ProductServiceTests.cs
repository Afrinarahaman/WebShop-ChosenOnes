using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Repository;
using WebshopProject.Api.Services;
using Xunit;

namespace ProjectWebshopTests.Services
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        
        private readonly Mock<IProductRepository> _mockProductRepository = new();
        private readonly Mock<ICategoryRepository> _mockCategoryRepository = new();

        public ProductServiceTests()
        {
            _productService = new ProductService(_mockProductRepository.Object, _mockCategoryRepository.Object);
        }

        [Fact]
        public async void GetAllProducts_ShouldReturnListOfProductResponses_WhenProductsExists()
        {
            // Arrange
            List<Product> Products = new();
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };

           
            Products.Add(new()
            {
                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1,
                Category=newCategory

            });

            Products.Add(new()
            {
                Id = 2,
                Title = "T-Shirt",
                Price = 199.99M,
                Description = "T-Shirt for boys",
                Image = "test.jpg",
                Stock = 10,
                CategoryId = 1,
                Category=newCategory
            });

            _mockProductRepository
                .Setup(x => x.SelectAllProducts())
                .ReturnsAsync(Products);

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<ProductResponse>>(result);
        }

        [Fact]
        public async void GetAllProducts_ShouldReturnEmptyListOfProductResponses_WhenNoProductsExists()
        {
            // Arrange
            List<Product> Products = new();

            _mockProductRepository
                .Setup(x => x.SelectAllProducts())
                .ReturnsAsync(Products);

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<ProductResponse>>(result);
        }

        [Fact]
        public async void GetProductById_ShouldReturnProductResponse_WhenProductExists()
        {
            // Arrange

            int productId = 1;
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };

            Product product = new()
            {
                Id = productId,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1,
                Category=newCategory

            };

            _mockProductRepository
                .Setup(x => x.SelectProductById(It.IsAny<int>()))
                .ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Title, result.Title);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Image, result.Image);
            Assert.Equal(product.Stock, result.Stock);
            Assert.Equal(product.CategoryId, result.CategoryId);

        }


        [Fact]
        public async void GetProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            _mockProductRepository
                .Setup(x => x.SelectProductById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productService.GetProductById(productId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateProduct_ShouldReturnProductResponse_WhenCreateIsSuccess()
        {
            // Arrange
            //ProductCategoryResponse newCategory = new()
            //{
            //    Id=1,
            //    CategoryName="Toy"
            //};
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };

            ProductRequest newProduct = new()
            {
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1


            };

            int productId = 1;

            Product createdProduct = new()
            {
                Id = productId,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1,
                Category=null
            };

            _mockProductRepository

                .Setup(x => x.InsertNewProduct(It.IsAny<Product>()))
                .ReturnsAsync(createdProduct);
            _mockCategoryRepository
                .Setup(x => x.SelectCategoryById(It.IsAny<int>()))
                .ReturnsAsync(newCategory);

            // Act
            var result = await _productService.CreateProduct(newProduct);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(newProduct.Title,result.Title);
            Assert.Equal(newProduct.Price,result.Price);    
            Assert.Equal(newProduct.Description,result.Description);
            Assert.Equal(newProduct.Image,result.Image);
            Assert.Equal(newProduct.Stock,result.Stock);    
            Assert.Equal(newProduct.CategoryId, result.CategoryId);


        }

        [Fact]
        public async void CreateProduct_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };
            ProductRequest newProduct = new()
            {
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1
            };

            _mockProductRepository
                .Setup(x => x.InsertNewProduct(It.IsAny<Product>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productService.CreateProduct(newProduct);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async void UpdateProduct_ShouldReturnProductResponse_WhenUpdateIsSuccess()
        {
            // NOTICE, we do not test if anything actually changed on the DB,
            // we only test that the returned values match the submitted values
            // Arrange
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };
            ProductRequest productRequest = new()
            {
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1
                

            };

            int productId = 1;

            Product product = new()
            {
                Id = productId,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1,
                Category =null
            };

            _mockProductRepository
                .Setup(x => x.UpdateExistingProduct(It.IsAny<int>(), It.IsAny<Product>()))
                .ReturnsAsync(product);
            _mockCategoryRepository
                .Setup(x => x.SelectCategoryById(It.IsAny<int>()))
                .ReturnsAsync(newCategory);

            // Act
            var result = await _productService.UpdateProduct(productId, productRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(productRequest.Title,result.Title);    
            Assert.Equal(productRequest.Description,result.Description);
            Assert.Equal(productRequest.Image,result.Image);
            Assert.Equal(productRequest.Stock,result.Stock);
            Assert.Equal(productRequest.CategoryId,result.CategoryId);


        }


        [Fact]
        public async void UpdateProduct_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            ProductRequest productRequest = new()
            {
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1
            };

            int productId = 1;

            _mockProductRepository
                .Setup(x => x.UpdateExistingProduct(It.IsAny<int>(), It.IsAny<Product>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productService.UpdateProduct(productId, productRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteProduct_ShouldReturnProductResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int productId = 1;
           
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };
            

            Product deletedProduct = new()
            {
                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1,
                Category =null
            };

            _mockProductRepository
                .Setup(x => x.DeleteProductById(It.IsAny<int>()))
                .ReturnsAsync(deletedProduct);
            _mockCategoryRepository
                .Setup(x => x.SelectCategoryById(It.IsAny<int>()))
                .ReturnsAsync(newCategory);
            // Act
            var result = await _productService.DeleteProduct(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            
        }

        [Fact]
        public async void DeleteProduct_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            _mockProductRepository
                .Setup(x => x.DeleteProductById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productService.DeleteProduct(productId);

            // Assert
            Assert.Null(result);
        }

    }
}
