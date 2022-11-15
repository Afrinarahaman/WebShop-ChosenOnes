using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Api.Database;
using WebshopProject.Api.Database.Entities;

namespace WebshopProject.Api.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> SelectAllOrders();
        Task<Order> SelectOrderById(int orderId);
        Task<List<Order>> SelectOrdersByCustomerId(int customerId);
        //Task<List<Order>> SelectAllOrdersWithoutProducts();
        // Task<List<Order>> SelectCategoriesByProductId(int productId);
        Task<Order> InsertNewOrder(Order orderId);
        //Task<Order> UpdateExistingOrder(int orderId, Order order);
        //Task<Order> DeleteOrderById(int orderId);

    }


    public class OrderRepository : IOrderRepository
    {
        private readonly WebshopProjectContext  _context;

        public OrderRepository(WebshopProjectContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> SelectAllOrders()
        {
            try
            {
                return await _context.Order
                         .Include(o => o.Customer)
                         .Include(o => o.OrderDetails).ThenInclude(x=>x.Product)

                          .ToListAsync();
            }
            catch(Exception)
            {
                return null;
            }
        }
        public async Task<List<Order>> SelectOrdersByCustomerId(int customerId)
        {
            try
            {
                return await _context.Order
                    .Include(o=>o.Customer)
                         .Include(o => o.OrderDetails).ThenInclude(x=>x.Product).Where (c=> c.CustomerId== customerId)

                          .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Order> SelectOrderById(int orderId)
        {
            try
            {
                return await _context.Order
                    .Include(a => a.OrderDetails).ThenInclude(a => a.Product)
                    // .Include(a => a.OrderDetails).ThenInclude(a => a.Order)
                    .Include(c => c.Customer)
                    .FirstOrDefaultAsync(order => order.Id == orderId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Order> InsertNewOrder(Order order)
        {
            
                _context.Order.Add(order);
                await _context.SaveChangesAsync();
                return order;

           

        }
        public async Task<Order> DeleteOrderById(int orderId)
        {
            var deleteOrder = await _context.Set<Order>().FirstOrDefaultAsync(o => o.Id == orderId);
            try
            {
                if (deleteOrder != null)
                {
                    _context.Remove(deleteOrder);
                    await _context.SaveChangesAsync();

                }

                return deleteOrder;
            }
            catch(Exception)
            {
                return null;
            }
        }

       

        public async Task<Order> UpdateExistingOrder(int orderId, Order order)
        {

             
            try
            {
                //order.Customer = null;
                 _context.Update(order);


                await _context.SaveChangesAsync();

                return await _context.Order.FirstOrDefaultAsync(order => order.Id == orderId);
            }
            catch(Exception)
            {
                return null;
            }
           
        }
    }
}
