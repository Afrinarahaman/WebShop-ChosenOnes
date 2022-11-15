using Moq;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Helpers;
using WebshopProject.Api.Repositories;
using WebshopProject.Api.Repository;
using WebshopProject.Api.Services;
using Xunit;

namespace ProjectWebshopTests.Services
{
    public class OrderServiceTests
    {

        private readonly OrderService _orderService;

        private readonly Mock<IOrderRepository> _mockOrderRepository = new();
        private readonly Mock<ICustomerRepository> _mockCustomerRepository = new();

        public OrderServiceTests()
        {
            _orderService = new OrderService(_mockOrderRepository.Object, _mockCustomerRepository.Object);
        }
        [Fact]
        public async void GetAllOrders_ShouldReturnListOfOrderResponses_WhenOrdersExists()
        {
            // Arrange
            List<Order> Orders = new();


      
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };



            List<OrderDetail> OrderDetails = new();
            OrderDetails.Add(new()
            {
                ProductId = 1,
                ProductTitle = "Kids Microwave",
                ProductPrice = 199.99M,
                Quantity = 1

            });
            Orders.Add(new()
            {

                Id = 1,
                OrderDate = DateTime.Now,
                CustomerId = 1,
                Customer = new Customer()
                {
                    Id = 1,
                    Email = "afrina@abc.com",
                    Password = "",
                    FirstName = "Afrina",
                    LastName = "Rahaman",
                    Address = "Husum",
                    Telephone = "12345",
                    Role = Role.Customer
                },
                OrderDetails = OrderDetails


            }); 





            _mockOrderRepository
                .Setup(x => x.SelectAllOrders())
                .ReturnsAsync(Orders);

            // Act
            var result = await _orderService.GetAllOrders();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<List<OrderResponse>>(result);
        }

        [Fact]
        public async void GetAllOrders_ShouldReturnEmptyListOfOrderResponses_WhenNoOrdersExists()
        {
            // Arrange
            List<Order> Orders = new();

            _mockOrderRepository
                .Setup(x => x.SelectAllOrders())
                .ReturnsAsync(Orders);

            // Act
            var result = await _orderService.GetAllOrders();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<OrderResponse>>(result);
        }
        [Fact]
        public async void GetOrderById_ShouldReturnOrderResponse_WhenOrderExists()
        {
            // Arrange

            int orderId = 1;

            List<OrderDetail> OrderDetails = new();
            OrderDetails.Add(new()
            {
                ProductId = 1,
                ProductTitle = "Kids Microwave",
                ProductPrice = 199.99M,
                Quantity = 1

            });
            Order order = new()
            {
                Id = orderId,
                //OrderDate = DateTime.Now,
                CustomerId = 1,
                Customer = new()
                {
                    Id = 1,
                    Email = "afrina@abc.com",
                    Password = "",
                    FirstName = "Afrina",
                    LastName = "Rahaman",
                    Address = "Husum",
                    Telephone = "12345",
                    Role = Role.Customer



                },
                OrderDetails = OrderDetails




            };

            _mockOrderRepository
                .Setup(x => x.SelectOrderById(It.IsAny<int>()))
                .ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderById(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderResponse>(result);
            Assert.Equal(order.Id, result.Id);
            //Assert.Equal( result.OrderDate);
            Assert.Equal(order.CustomerId, result.CustomerId);
            //Assert.Equal(order.Customer, result.Customer);
            Assert.Equal(1, result.OrderDetails.Count);


        }

        [Fact]
        public async void GetOrderById_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            // Arrange
            int orderId = 1;

            _mockOrderRepository
                .Setup(x => x.SelectOrderById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _orderService.GetOrderById(orderId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateOrder_ShouldReturnOrderResponse_WhenCreateIsSuccess()
        {
            // Arrange
            List<OrderDetailRequest> newOrderDetailsRequest = new();
            newOrderDetailsRequest.Add(new()
            {
                ProductId = 1,
                ProductTitle = "Kids Microwave",
                ProductPrice = 199.99M,
                Quantity = 1

            });
            OrderRequest newOrderRequest = new()
            {
                CustomerId = 1,
                OrderDetails = newOrderDetailsRequest
            };

            List<OrderDetail> newOrderDetails = new();
            newOrderDetails.Add(new()
            {
                ProductId = 1,
                ProductTitle = "Kids Microwave",
                ProductPrice = 199.99M,
                Quantity = 1

            });

            Customer customer = new Customer()
            {
                Id = 1,
                Email = "",
                Password = "",
                FirstName = "",
                LastName = "",
                Address = "",
                Telephone = "",
                Role = Role.Customer

            };
            int orderId = 1;
            Order newOrder = new()
            {
                Id = 1,
                OrderDate = DateTime.Now,
                CustomerId = 1,
                OrderDetails = newOrderDetails


            };


            _mockCustomerRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(customer);
            _mockOrderRepository
                .Setup(x => x.InsertNewOrder(It.IsAny<Order>()))
                .ReturnsAsync(newOrder);

            // Act
            var result = await _orderService.CreateOrder(newOrderRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderResponse>(result);
            Assert.Equal(orderId, result.Id);
            Assert.Equal(newOrder.CustomerId, result.CustomerId);
            Assert.Equal(1, result.OrderDetails.Count);
           


        }

        [Fact]
        public async void CreateOrder_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            List<OrderDetailRequest> newOrderDetailsRequest = new();
            newOrderDetailsRequest.Add(new()
            {
                ProductId = 1,
                ProductTitle = "Kids Microwave",
                ProductPrice = 199.99M,
                Quantity = 1

            });
            OrderRequest newOrderRequest = new()
            {


                OrderDate = DateTime.Now,
                CustomerId = 1,
                OrderDetails = newOrderDetailsRequest



            };
            Customer customer = new Customer()
            {
                Id = 1,
                Email = "",
                Password = "",
                FirstName = "",
                LastName = "",
                Address = "",
                Telephone = "",
                Role = Role.Customer

            };

           

            _mockCustomerRepository
               .Setup(x => x.GetById(It.IsAny<int>()))
               .ReturnsAsync(() => null);
            _mockOrderRepository
                .Setup(x => x.InsertNewOrder(It.IsAny<Order>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _orderService.CreateOrder(newOrderRequest);

            // Assert
            Assert.Null(result);
        }

    }
}
