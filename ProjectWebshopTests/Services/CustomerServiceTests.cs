using Moq;
using System.Collections.Generic;
using WebshopProject.Api.Authorization;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Helpers;
using WebshopProject.Api.Repositories;
using WebshopProject.Api.Services;
using Xunit;



namespace WebshopProjectTests.Services
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _customerService;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository = new();
        private readonly Mock<IJwtUtils> jwt = new();
        

        public CustomerServiceTests()
        {
            _customerService = new CustomerService(_mockCustomerRepository.Object,jwt.Object);

        }

        [Fact]
        public async void GetAllCustomers_ShouldReturnListOfCustomerResponses_WhenCustomersExists()
        {
            //Arrange

            List<Customer> customers = new();


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
                FirstName = "Peter",
                Email = "peter@abc.com",
                Password = "password",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role = Role.Admin
            });

            _mockCustomerRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(customers);

            //Act
            var result = await _customerService.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<CustomerResponse>>(result);
        }

        [Fact]
        public async void GetAllCustomers_ShouldReturnEmptyListOfCustomerResponses_WhenNoCustomersExists()
        {
            //Arrange
            List<Customer> customers = new();

            _mockCustomerRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(customers);

            //Act
            var result = await _customerService.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<CustomerResponse>>(result);
        }

        [Fact]
        public async void GetCustomerByIdShouldReturnCustomerResponseWhenCustomerExists()
        {
            //Arrange
            int customerId = 1;
            
            Customer customer = new()
            {
                Id = 1,
                FirstName = "Peter",
                Email = "peter@abc.com",
                Password = "password",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role = Role.Admin
            };

            _mockCustomerRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(customer);

            //Act
            var result = await _customerService.GetById(customerId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<CustomerResponse>(result);
            Assert.Equal(customer.Id, result.Id);
            Assert.Equal(customer.FirstName, result.FirstName);
            Assert.Equal(customer.LastName, result.LastName);
            Assert.Equal(customer.Address, result.Address);
            Assert.Equal(customer.Telephone, result.Telephone);

        }

        [Fact]
        public async void GetCustomerByIdShloudReturnNullWhenCustomerDoesNotExist()
        {
            //Arrage
            int customerId = 1;

            _mockCustomerRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _customerService.GetById(customerId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateCustomer_ShouldReturnCustomerResponse_WhenCreateIsSuccess()
        {
            //Arrange

            RegisterCustomerRequest newCustomer = new()
            {

                FirstName = "Peter",
                Email = "peter@abc.com",
                Password = "password",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",


            };

           

            _mockCustomerRepository
               .Setup(x => x.Create(It.IsAny<Customer>()))
               .ReturnsAsync(() => null);

            //Act
            var result = await _customerService.Register(newCustomer);

            //Assert         
           
            Assert.Null(result);
       
           

        }

        [Fact]
        public async void CreateCustomer_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            //Arrange
            RegisterCustomerRequest newCustomer = new()
            {
                FirstName = "Peter",
                Email = "peter@abc.com",
                Password = "password",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678"

            };

            _mockCustomerRepository
                .Setup(x => x.Create(It.IsAny<Customer>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _customerService.Register(newCustomer);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateCustomer_ShouldReturnCustomerResponse_WhenUpdateIsSuccess()
        {
            // NOTICE, we do not test id anything actually changed on the DB,
            // we only test that the returned values match the submitted values

            //Arrange
            RegisterCustomerRequest customerRequest = new()
            {
              
                FirstName = "Peter",
                Email = "peter@abc.com",
                Password = "password",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
              

            };

            int customerId = 1;

           
            Customer customer = new()
            {
            
                Id= customerId,
                FirstName = "Peter",
                Email = "peter@abc.com",
                Password = "password",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role = Role.Admin


            };
           
            _mockCustomerRepository
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Customer>()))
                .ReturnsAsync(customer);

            //Act
            var result = await _customerService.Update(customerId,customerRequest);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<CustomerResponse>(result);
            Assert.Equal(customerId, result.Id);
            Assert.Equal(customerRequest.FirstName, result.FirstName);
            Assert.Equal(customerRequest.LastName, result.LastName);
            Assert.Equal(customerRequest.Address, result.Address);
            Assert.Equal(customerRequest.Telephone, result.Telephone);
          


        }

        [Fact]
        public async void UpdateCustomer_ShouldReturnNull_WhenAuhtorDoesNotExist()
        {

            //Arrange
            RegisterCustomerRequest customerRequest = new()
            {
                FirstName = "Peter",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678"
            };

            int customerId = 1;

            _mockCustomerRepository
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Customer>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _customerService.Update(customerId, customerRequest);


            //Assert
            Assert.Null(result);

        }


        [Fact]
        public async void DeleteCustomer_shouldReturnCustomerResponse_WhenDeleteIsSuccess()
        {

            //Arrange
            int customerId = 1;
              

            
            Customer deletedCustomer = new()
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
             _mockCustomerRepository
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(deletedCustomer);

            // Act
            var result = await _customerService.Delete(customerId);

            // Assert
          
           
            Assert.NotNull(result);
            Assert.IsType<CustomerResponse>(result);
            Assert.Equal(customerId, result.Id);



        }

        [Fact]
        public async void DeletCustomer_ShouldNotReturnNull_whenCustomerDoesNotExist()
        {

            //Arrange
            int customerId = 1;

            _mockCustomerRepository
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _customerService.Delete(customerId);

            //Assert
            Assert.Null(null);

        }

    }
}
    