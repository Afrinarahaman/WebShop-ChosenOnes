using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopProject.Api.Database;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.Repository;
using Xunit;

namespace ProjectWebshopTests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly DbContextOptions<WebshopProjectContext> _options;
        private readonly WebshopProjectContext _context;
        private readonly ProductRepository _productRepository;
        public ProductRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebshopProjectContext>()
                .UseInMemoryDatabase(databaseName: "WebshopProjectProducts")
                .Options;

            _context = new(_options);
            _productRepository = new(_context);
        }
        [Fact]
        public async void SelectAllProducts_ShouldReturnListOfProducts_WhenProductExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            _context.Category.Add(new()
            {
                Id = 1,
                CategoryName = "Toy"

            });

            _context.Product.Add(new()
            {

                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1


            });
            _context.Product.Add(new()
            {

                Id = 2,
                Title = "T-Shirt",
                Price = 199.99M,
                Description = "T-Shirt for boys",
                Image = "test.jpg",
                Stock = 10,
                CategoryId = 1


            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _productRepository.SelectAllProducts();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Equal(2, result.Count);
            // Assert.Empty(result);
        }
        [Fact]
        public async void SelectAllProducts_ShouldReturnEmptyListOfProducts_WhenNoProductExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();




            //Act
            var result = await _productRepository.SelectAllProducts();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);

            Assert.Empty(result);
        }
        [Fact]
        public async void SelectProductById_ShouldReturnProduct_WhenProductExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            _context.Category.Add(new()
            {
                Id = 1,
                CategoryName = "Toy"

            });


            _context.Product.Add(new()
            {

                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1
                


            });


            await _context.SaveChangesAsync();

            //Act
            var result = await _productRepository.SelectProductById(productId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
            // Assert.Empty(result);
        }
        [Fact]
        public async void SelectProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();




            //Act
            var result = await _productRepository.SelectProductById(1);

            //Assert


            Assert.Null(result);
        }
        [Fact]
        public async void InsertNewProduct_ShouldAddnewIdToProduct_WhenSavingToDatabase()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Product product = new()
            {
                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                CategoryId = 1,
                Stock = 10



            };


            await _context.SaveChangesAsync();

            //Act
            var result = await _productRepository.InsertNewProduct(product);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(expectedNewId, result.Id);

        }

        [Fact]
        public async void InsertNewProduct_ShouldFailToAddNewProduct_WhenProductIdAlreadyExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();



            Product product = new()
            {
                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                CategoryId = 1,
                Stock = 10
            };

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            //Act
            async Task action() => await _productRepository.InsertNewProduct(product);


            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);

        }
        [Fact]
        public async void UpdateExistingProduct_ShouldChangeValuesOnProduct_WhenProductExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            Product newProduct = new()
            {
                Id = productId,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                CategoryId = 1,
                Stock = 10


            };

            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();

            Product updateProduct = new()
            {
                Id = productId,
                Title = "updated ToyBus",
                Price = 299.99M,
                Description = "updated Kids Toys",
                Image = "updatetest2.jpg",
                CategoryId = 1,
                Stock = 10

            };



            //Act
            var result = await _productRepository.UpdateExistingProduct(productId, updateProduct);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(updateProduct.Title, result.Title);
            Assert.Equal(updateProduct.Price, result.Price);
            Assert.Equal(updateProduct.Description, result.Description);
            Assert.Equal(updateProduct.Image, result.Image);
            Assert.Equal(updateProduct.CategoryId, result.CategoryId);
            Assert.Equal(updateProduct.Stock, result.Stock);


        }

        [Fact]
        public async void UpdateExistingProduct_ShouldReturnNull_WhenProductDoesNotExist()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;


            Product updateProduct = new()
            {
                Id = productId,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                CategoryId = 1,
                Stock = 10

            };



            //Act
            var result = await _productRepository.UpdateExistingProduct(productId, updateProduct);

            //Assert
            Assert.Null(result);

        }
        [Fact]
        public async void DeleteProductById_ShouldReturnDeleteProduct_WhenProductIsDeleted()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            Product newProduct = new()
            {
                Id = productId,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                CategoryId = 1,
                Stock = 10

            };

            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();




            //Act
            var result = await _productRepository.DeleteProductById(productId);
            var product = await _productRepository.SelectProductById(productId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
            Assert.Null(product);
        }
        [Fact]
        public async void DeleteProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();





            //Act
            var result = await _productRepository.DeleteProductById(1);


            //Assert

            Assert.Null(result);
        }

    }
}
