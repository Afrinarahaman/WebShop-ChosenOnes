using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopProject.Api.Database;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.Helpers;
using WebshopProject.Api.Repository;
using Xunit;

namespace ProjectWebshopTests.Repositories
{
    public class OrderRepositoryTests
    {

        private readonly DbContextOptions<WebshopProjectContext> _options;
        private readonly WebshopProjectContext _context;
        private readonly OrderRepository _orderRepository;
        public OrderRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebshopProjectContext>()
                .UseInMemoryDatabase(databaseName: "WebshopProjectOrders")
                .Options;

            _context = new(_options);
            
        _orderRepository = new(_context);
        }
        [Fact]
        public async void SelectAllOrders_ShouldReturnListOfOrders_WhenOrderExists()
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
            _context.Order.Add(new()
            {

                Id = 1,
                OrderDate = DateTime.Now,
                CustomerId = 1,
            

            });
            _context.OrderDetail.Add(new()
            {
                ProductId= 1,
                ProductTitle= "Kids Microwave",
                ProductPrice= 199.99M,
                Quantity= 1

            });
            

            await _context.SaveChangesAsync();

            //Act
            var result = await _orderRepository.SelectAllOrders();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Order>>(result);
            Assert.Equal(1, result.Count);
            // Assert.Single(result);
        }
        [Fact]
        public async void SelectAllOrders_ShouldReturnEmptyListOfOrders_WhenNoOrderExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();




            //Act
            var result = await _orderRepository.SelectAllOrders();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Order>>(result);

            Assert.Empty(result);
        }

        [Fact]
        public async void SelectOrderById_ShouldReturnOrder_WhenOrderExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int orderId = 1;
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
            _context.Order.Add(new()
            {

                Id = 1,
                OrderDate = DateTime.Now,
                CustomerId = 1,


            });
            _context.OrderDetail.Add(new()
            {
                ProductId = 1,
                ProductTitle = "Kids Microwave",
                ProductPrice = 199.99M,
                Quantity = 1

            });


            await _context.SaveChangesAsync();

            //Act
            var result = await _orderRepository.SelectOrderById(orderId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(orderId, result.Id);
            // Assert.Empty(result);
        }

        [Fact]
        public async void SelectOrderById_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();




            //Act
            var result = await _orderRepository.SelectOrderById(1);

            //Assert


            Assert.Null(result);
        }

        [Fact]
        public async void SelectAllOrdersByCustomerId_ShouldReturnAllOrders_WhenThisCustomerHasOrdered()
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


            _context.Order.Add(new()
            {

                Id = 1,
                OrderDate = DateTime.Now,
                CustomerId = 1,


            });
            _context.OrderDetail.Add(new()
            {
                ProductId = 1,
                ProductTitle = "Kids Microwave",
                ProductPrice = 199.99M,
                Quantity = 1

            });


            await _context.SaveChangesAsync();

            //Act
            var result = await _orderRepository.SelectOrdersByCustomerId(customerId);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsType<List<Order>>(result);

            Assert.Equal(customerId, result[0].CustomerId);
            // Assert.Empty(result);
        }
        [Fact]
        public async void InsertNewOrder_ShouldAddnewIdToOrder_WhenSavingToDatabase()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;
            Order order = new()
            {

                Id = 1,
                OrderDate = DateTime.Now,
                CustomerId = 1,


            };
            OrderDetail orderDetail = new()
            {
                ProductId = 1,
                ProductTitle = "Kids Microwave",
                ProductPrice = 199.99M,
                Quantity = 1

            };



            await _context.SaveChangesAsync();

            //Act
            var result = await _orderRepository.InsertNewOrder(order);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(expectedNewId, result.Id);

        }

        [Fact]
        public async void InsertNewOrder_ShouldFailToAddNewOrder_WhenOrderIdAlreadyExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();



            Order order = new()
            {

                Id = 1,
                OrderDate = DateTime.Now,
                CustomerId = 1,


            };
            OrderDetail orderDetail = new()
            {
                ProductId = 1,
                ProductTitle = "Kids Microwave",
                ProductPrice = 199.99M,
                Quantity = 1

            };
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            //Act
            async Task action() => await _orderRepository.InsertNewOrder(order);


            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);

        }


    }
}


