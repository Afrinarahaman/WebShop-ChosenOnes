using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System.Collections.Generic;
using WebshopProject.Api.Controllers;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Services;
using Xunit;
using WebshopProject.Api.Helpers;

namespace ProjectWebshopTests.Controllers
{
    public class CustomerControllerTests
    {

        private readonly CustomerController _customerController;
        private readonly Mock<ICustomerService> _mockCustomerService = new();
    
        public CustomerControllerTests()
        {
            _customerController = new(_mockCustomerService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenCustomerExists()
        {
            //Arrange
            List<CustomerResponse> customers = new();
            customers.Add(new()
            {

                Id = 1,
                FirstName = "Peter",
                Email = "peter@abc.com",
                Password = "password",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role = Role.Admin

            });

            customers.Add(new()
            {
                Id = 2,
                FirstName = "Susana",
                LastName = "Andersan",
                Email= "susana@abc.com",
                Password="password",
                Address = "House no:486 , 3400 Green street",
                Telephone = "12345678",
                Role = Role.Customer


            });

            _mockCustomerService
                .Setup(x => x.GetAll())
                .ReturnsAsync(customers);

            //Act
            var result = await _customerController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoCustomerExists()
        {
            //Arrange

            List<CustomerResponse> customers = new();

            _mockCustomerService
                .Setup(x => x.GetAll())
                .ReturnsAsync(customers);

            //Act
            var result = await _customerController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //Arrange                      
            _mockCustomerService
                .Setup(x => x.GetAll())
                .ReturnsAsync(() => null);

            //Act
            var result = await _customerController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange                      
            _mockCustomerService
                .Setup(x => x.GetAll())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _customerController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            int customerId = 1;
            CustomerResponse customer = new()
            {
                Id = customerId,              
                FirstName = "Peter",
                Email = "peter@abc.com",
                Password = "password",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role = Role.Admin

            };

            _mockCustomerService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(customer);

            //Act
            var result = await _customerController.GetById(customerId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenCustomerDoesNotExists()
        {
            //Arrange
            int customerId = 1;

            _mockCustomerService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _customerController.GetById(customerId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockCustomerService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //Act
            var result = await _customerController.GetById(1);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenCustomerIsSuccessfullyCreated()
        {
            //Arrange
            RegisterCustomerRequest newCustomer = new()
            {
                FirstName = "Peter",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",

            };

            int customerId = 1;

            CustomerResponse customerResponse = new()
            {
                Id = customerId,
                FirstName = "Peter",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678"

            };

            _mockCustomerService
                .Setup(x => x.Register(It.IsAny<RegisterCustomerRequest>()))
                .ReturnsAsync(customerResponse);

            //Act
            var result = await _customerController.Register(newCustomer);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            RegisterCustomerRequest newCustomer = new()
            {
                FirstName = "Peter",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678"
            };

            _mockCustomerService
                .Setup(x => x.Register(It.IsAny<RegisterCustomerRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //Act
            var result = await _customerController.Register(newCustomer);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenCustomerIsSuccessfullyUpdate()
        {
            //Arrange
            RegisterCustomerRequest updateCustomer = new()
            {
                FirstName = "Peter",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678"
            };

            int customerId = 1;

            CustomerResponse customerResponse = new()
            {
                Id = customerId,
                FirstName = "Peter",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role= Role.Admin

            };

            _mockCustomerService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<RegisterCustomerRequest>()))
                .ReturnsAsync(customerResponse);

            //Act
            var result = await _customerController.Update(customerId,updateCustomer);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            RegisterCustomerRequest updateCustomer = new RegisterCustomerRequest
            {
                FirstName = "Peter",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Email="peter@abc.com",
                Password="password"
                
            
            };

            int customerId = 1;


            _mockCustomerService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<RegisterCustomerRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));
            
            //Act
            var result = await _customerController.Update(customerId, updateCustomer);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenCustomerIsDeleted()
        {
            //Arrange
            int customerId = 1;

            CustomerResponse customerResponse = new()
            {
                Id = customerId,
                FirstName = "Peter",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role = Role.Admin

            };

            _mockCustomerService
               .Setup(x => x.Delete(It.IsAny<int>()))
               .ReturnsAsync(customerResponse);

            //Act
            var result = await _customerController.Delete(customerId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeleteCustomerWhichDoesNotExist()
        {
            //Arrange
            int customerId = 1;

            _mockCustomerService
                 .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() =>null);
           
           //Act
            var result = await _customerController.Delete(customerId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int customerId = 1;

            _mockCustomerService
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));         


            //Act
            var result = await _customerController.Delete(customerId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        /***/

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenLoginIsSuccessfullyCreated()
        {
            //Arrange
            LoginRequest newLogin = new()
            {
                Email = "peter@abc.com",
                Password = "password"
               
            };

            int customerId = 1;

            LoginResponse loginResponse = new()
            {
                Id = customerId,
                FirstName = "Peter",
                Email = "peter@abc.com",
                Password = "password",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role= Role.Admin,
                Token= ""
            };

            _mockCustomerService
                .Setup(x => x.Authenticate(It.IsAny<LoginRequest>()))
                .ReturnsAsync(loginResponse);

            //Act
            var result = await _customerController.Authenticate(newLogin);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenLoginExceptionIsRaised()
        {
            //Arrange
            LoginRequest newLogin = new()
            {
                Email = "peter@abc.com",
                Password = "password"

            };
            _mockCustomerService
                .Setup(x => x.Authenticate(It.IsAny<LoginRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //Act
            var result = await _customerController.Authenticate(newLogin);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

    }
}


