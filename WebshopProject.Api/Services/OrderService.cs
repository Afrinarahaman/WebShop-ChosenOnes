using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Repositories;
using WebshopProject.Api.Repository;

namespace WebshopProject.Api.Services
{

    public interface IOrderService
    {
        Task<List<OrderResponse>> GetAllOrders();

        Task<OrderResponse> GetOrderById(int orderId);
        Task<List<OrderResponse>> GetOrdersByCustomerId(int customerId);
        //Task<List<OrderResponse>> GetAllCategoriesWithoutProducts();
        Task<OrderResponse> CreateOrder(OrderRequest newOrder);
        //Task<OrderResponse> UpdateOrder(int orderId, OrderRequest updateOrder);
        //Task<OrderResponse> DeleteOrder(int orderId);
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }


        public async Task<List<OrderResponse>> GetAllOrders()
        {
            List<Order> orders = await _orderRepository.SelectAllOrders();

            if (orders != null)
            {
                return orders.Select(order => MapOrderToOrderResponse(order)).ToList();
            }

            return null;
        }



        public async Task<OrderResponse> GetOrderById(int orderId)
        {
            Order order = await _orderRepository.SelectOrderById(orderId);

            if (order != null)
            {

                return MapOrderToOrderResponse(order);
            }
            return null;
        }
        public async Task<OrderResponse> CreateOrder(OrderRequest newOrder)
        {
            Order order = MapOrderRequestToOrder(newOrder);
            Order insertOrder = await _orderRepository.InsertNewOrder(order);


            if (insertOrder != null)
            {

                Customer customer = await _customerRepository.GetById(newOrder.CustomerId);
                insertOrder.Customer = customer;
                return MapOrderToOrderResponse(insertOrder);

            }
            return null;
        }

        private Order MapOrderRequestToOrder(OrderRequest newOrder)
        {
            return new Order()
            {
                OrderDate = DateTime.Now,
                //OrderDate = newOrder.OrderDate,

                CustomerId = newOrder.CustomerId,
                OrderDetails = newOrder.OrderDetails.Select(x => new OrderDetail
                {
                    ProductId = x.ProductId,
                    ProductTitle = x.ProductTitle,
                    ProductPrice = x.ProductPrice,
                    Quantity = x.Quantity

                }).ToList()

            };
        }

        private OrderResponse MapOrderToOrderResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                Customer = new CustomerResponse()
                {
                    Id = order.Customer.Id,
                    Email = order.Customer.Email,
                    FirstName = order.Customer.FirstName,
                    LastName = order.Customer.LastName,
                    Address = order.Customer.Address,
                    Telephone = order.Customer.Telephone,
               },
                OrderDetails = order.OrderDetails.Select(order => new OrderDetailResponse
                {
                    Id = order.Id,
                    ProductId = order.ProductId,
                    ProductTitle = order.ProductTitle,
                    ProductPrice = order.ProductPrice,
                    Quantity = order.Quantity

                }).ToList()


            };


        }

        public async Task<List<OrderResponse>> GetOrdersByCustomerId(int customerId)
        {
            List<Order> orders = await _orderRepository.SelectOrdersByCustomerId(customerId);

            if (orders != null)
            {
                List<OrderResponse> responses = orders.Select(x => MapOrderToOrderResponse(x)).ToList();
                return responses;
            }
            return null;
        }
    }
}
