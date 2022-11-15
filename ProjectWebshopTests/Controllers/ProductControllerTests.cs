using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using WebshopProject.Api.Controllers;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Services;
using Xunit;

namespace ProjectWebshopTests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductService> _mockProductService = new();

        public ProductControllerTests()
        {
            _productController = new(_mockProductService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenProductsExist()
        {
            //Arrange

            List<ProductResponse> products = new();
            products.Add(new()
            {
                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1


            });
            products.Add(new()
            {
                Id = 2,
                Title = "T-Shirt",
                Price = 199.99M,
                Description = "T-Shirt for boys",
                Image = "test.jpg",
                Stock = 10,
                CategoryId = 2



            });

            _mockProductService.Setup(x => x.GetAllProducts()).ReturnsAsync(products);



            //Act
            var result = await _productController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoProductsExists()
        {
            //Arrange
            List<ProductResponse> products = new();


            _mockProductService
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(products);



            //Act
            var result = await _productController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //Arrange



            _mockProductService
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(() => null);



            //Act
            var result = await _productController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange



            _mockProductService
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _productController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            int productId = 1;

            ProductResponse product = new()

            {
                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1

            };


            _mockProductService
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(product);



            //Act
            var result = await _productController.GetById(productId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenProductDoesNotExist()
        {
            //Arrange
            int productId = 1;



            _mockProductService
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(() => null);



            //Act
            var result = await _productController.GetById(productId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockProductService
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));



            //Act
            var result = await _productController.GetById(1);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenProductIsSuccessfullyCreated()
        {
            //Arrange


            ProductRequest newProduct = new()

            {

               
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1


            };

            //int productId = 1;

            ProductResponse productResponse = new()
            {
                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1


            };
            _mockProductService
                .Setup(x => x.CreateProduct(It.IsAny<ProductRequest>()))
                .ReturnsAsync(productResponse);



            //Act
            var result = await _productController.Create(newProduct);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        
        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange


            ProductRequest newProduct = new()

            {

                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1

            };


            _mockProductService
                .Setup(x => x.CreateProduct(It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _productController.Create(newProduct);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenProductIsSuccessfullyUpdated()
        {
            //Arrange


            ProductRequest updateProduct = new()

            {

                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1

            };

            int productId = 1;

            ProductResponse productResponse = new()
            {

                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1


            };
            _mockProductService
                .Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(productResponse);



            //Act
            var result = await _productController.Update(productId, updateProduct);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenTryingToUpdateProductWhichDoesNotExist()
        {
            //Arrange


            ProductRequest updateProduct = new()

            {


                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1

            };

            

            _mockProductService
                .Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => null);



            //Act
            int productId = 1;

            var result = await _productController.Update(productId, updateProduct);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange


            ProductRequest updateProduct = new()

            {

                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1

            };

            int productId = 1;

            _mockProductService
                .Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _productController.Update(productId, updateProduct);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenProductIsDeleted()
        {
            //Arrange
            int productId = 1;

            ProductResponse productResponse = new()
            {
                Id = 1,
                Title = "ToyBus",
                Price = 299.99M,
                Description = "Kids Toys",
                Image = "test2.jpg",
                Stock = 10,
                CategoryId = 1

            };
            _mockProductService
                .Setup(x => x.DeleteProduct(It.IsAny<int>()))
                .ReturnsAsync(productResponse);



            //Act
            var result = await _productController.Delete(productId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeleteProductWhichDoesNotExist()
        {
            //Arrange

            int productId = 1;

            _mockProductService
                .Setup(x => x.DeleteProduct(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _productController.Delete(productId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange


            int productId = 1;

            _mockProductService
                .Setup(x => x.DeleteProduct(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _productController.Delete(productId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }



    }
}
