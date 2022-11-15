using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopProject.Api.Controllers;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Services;
using Xunit;

namespace ProjectWebshopTests.Controllers
{
    public class OrderControllerTests
    {
        private readonly OrderController _orderController;
        private readonly Mock<IOrderService> _mockOrderService = new();

        public OrderControllerTests()
        {
            _orderController = new(_mockOrderService.Object);
        }

        //[Fact]
        //public async void GetAll_ShouldReturnStatusCode200_WhenOrdersExist()
        //{
        //    //Arrange

        //    OrderDetail orderDetail = new()
        //    {
        //        ProductId = 1,
        //        ProductTitle = "Kids Microwave",
        //        ProductPrice = 199.99M,
        //        Quantity = 1

        //    };
        //    List<OrderResponse> orders = new();
           
        //    orders.Add(new()
        //    {
        //        Id = 1,
        //        OrderDate = DateTime.Now,
        //        CustomerId = 1,
        //        OrderDetails=orderDetail


        //    });
          
        //});

        //    _mockOrderService.Setup(x => x.GetAllOrders()).ReturnsAsync(orders);



        //    //Act
        //    var result = await _orderController.GetAll();

        //    //Assert
        //    var statusCodeResult = (IStatusCodeActionResult)result;
        //    Assert.Equal(200, statusCodeResult.StatusCode);

        //}
    }
}
