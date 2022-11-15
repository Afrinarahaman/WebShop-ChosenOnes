using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopProject.Api.Database;
using Xunit;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.Repositories;
using WebshopProject.Api.Helpers;
using Moq;
using WebshopProject.Api.Authorization;

namespace ProjectWebshopTests.Repositories
{
    public class CustomerRepositoryTests
    {
        private readonly DbContextOptions<WebshopProjectContext> _options;
        private readonly WebshopProjectContext _context;
        private readonly CustomerRepository _customerRepository;
        private readonly Mock<IJwtUtils> jwt = new();

       

        public CustomerRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebshopProjectContext>()
                .UseInMemoryDatabase(databaseName: "WebshopProjectCustomers")
                .Options;

            _context = new(_options);
            _customerRepository = new(_context);

        }

        [Fact]
        public async void SelectAllCustomers_ShouldReturnListOfCustomers_WhenCustomersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Customer.Add(new()
            {
                Id = 1,
                FirstName = "Peter",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role = Role.Admin
            });
         
            _context.Customer.Add(new()
           
            {
                Id = 2,
                FirstName = "Susana",
                LastName = "Andersan",
                Email = "susan@abc.com",
                Password = "password",
                Address = "House no:486 , 3400 Green street",
                Telephone = "12345678",
                Role = Role.Customer


            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _customerRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Customer>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllCustomers_ShouldReturnEmptyListOfCustomers_WhenNoCustomersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _customerRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Customer>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectCustomerById_ShouldReturnCustomer_WhenCustomerExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int customerId = 1;


            _context.Customer.Add(new()
            {
                Id = 1,
                FirstName = "Peter",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role = Role.Admin
            });
           

            await _context.SaveChangesAsync();

            //Act

            var result = await _customerRepository.GetById(customerId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public async void SelectCustomerById_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _customerRepository.GetById(1);

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public async void InsertNewCustomer_ShouldAddNewIdToCustomer_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

          
           Customer newCustomer = new()
           {
               Id =expectedNewId,
               FirstName = "Peter",
               Email = "peter@abc.com",
               Password = "password",
               LastName = "Aksten",
               Address = "House no:123 , 2700 Ghost street",
               Telephone = "12345678",
               Role = Role.Admin,
               
           };
        

            //Act

            var result = await _customerRepository.Create(newCustomer);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(expectedNewId, result.Id);
        }
       
        [Fact]     
        public async void InsertNewCustomer_ShouldFailToAddNewCustomer_WhenCustomerIdAlreadyExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

          // int customerId = 1;
             
            Customer customer = new()
            {
                Id = 1,
                Email = "peter@abc.com",
                FirstName = "Peter",             
                Password = "password",
                LastName = "Aksten",
                Address = "House no:123 , 2700 Ghost street",
                Telephone = "12345678",
                Role = Role.Admin
            };
                   
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            //Act
            Func <Task> action = async() => await _customerRepository.Create(customer);

            //Assert           
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);        
            Assert.Contains("id has already been added", ex.Message);
        }


        //[Fact]
        //public async void InsertNewCustomer_ShouldFailToAddNewCustomer_WhenCustomerEmailAlreadyExists()
        //{
        //    //Arrange
        //    await _context.Database.EnsureDeletedAsync();

        //    // int customerId = 1;

        //    Customer customer = new()
        //    {
        //        Id = 1,
        //        Email = "peter@abc.com",
        //        FirstName = "Peter",
        //        Password = "password",
        //        LastName = "Aksten",
        //        Address = "House no:123 , 2700 Ghost street",
        //        Telephone = "12345678",
        //        Role = Role.Admin
        //    };

        //    _context.Customer.Add(customer);

        //    await _context.SaveChangesAsync();

        //    //Act
        //    Func<Task> action = async () => await _customerRepository.Create(customer);

        //    //Assert

        //    var ex = await Assert.ThrowsAsync<Exception>(action);
        //    Assert.Contains("id has already been added", ex.Message);
        //}

        [Fact]
        public async void UpdateExistingCustomer_ShouldChangeValuesOnCustomer_WhenCustomerExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int customerId = 1;

            Customer newCustomer = new()
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

            _context.Customer.Add(newCustomer);
            await _context.SaveChangesAsync();

            Customer updateCustomer = new()
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


            //Act
            var result = await _customerRepository.Update(customerId, updateCustomer);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(customerId, result.Id);
            Assert.Equal(updateCustomer.FirstName, result.FirstName);
            Assert.Equal(updateCustomer.LastName, result.LastName);
            Assert.Equal(updateCustomer.Address, result.Address);
            Assert.Equal(updateCustomer.Telephone, result.Telephone);
        }

        [Fact]
        public async void UpdateExistingCustomer_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int customerId = 1;

            Customer updateCustomer = new()
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


            //Act
            var result = await _customerRepository.Update(customerId, updateCustomer);

            //Asert
            Assert.Null(result);

        }

        [Fact]
        public async void DeleteCustomerById_ShouldReturnDeletedCustomer_WhenCustomerIsDeleted()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int customerId = 1;

            Customer newCustomer = new()
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

            _context.Customer.Add(newCustomer);
            await _context.SaveChangesAsync();


            //Act
            var result = await _customerRepository.Delete(customerId);
            var customer = await _customerRepository.GetById(customerId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(customerId, result.Id);
            Assert.Null(customer);
        }

        [Fact]
        public async void DeleteCustomerById_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _customerRepository.Delete(1);


            //Assert
            Assert.Null(result);

        }
    }
}


