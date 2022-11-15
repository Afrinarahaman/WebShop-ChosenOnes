using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Api.Database;
using WebshopProject.Api.Database.Entities;

namespace WebshopProject.Api.Repository
{

    public interface IOrderDetailRepository
    {
        
        Task<OrderDetail> SelectOrderDetailById(int orderdetailId);
        Task<List<OrderDetail>> SelectAllOrderDetails();
       // Task<List<Order>> SelectAllOrderDetailsById(int orderId);
        // Task<List<Order>> SelectCategoriesByProductId(int productId);
        //Task<OrderDetail> InsertNewOrderDetil(OrderDetail orderdetailId);
        //Task<Order> UpdateExistingOrder(int orderId, Order order);
        //Task<Order> DeleteOrderById(int orderId);

    }
    public class OrderDetailRepository: IOrderDetailRepository
    {
        private WebshopProjectContext _context;

        public OrderDetailRepository(WebshopProjectContext context)
        {
            _context = context;
        }
        //public async Task<OrderDetail> InsertNewOrderDetil(OrderDetail orderDetail)
        //{
        //    try
        //    {
        //        _context.OrderDetail.Add(orderDetail);
        //        await _context.SaveChangesAsync();
        //        return orderDetail;

        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        public async Task<List<OrderDetail>> SelectAllOrderDetails()
        {
            try
            {
                return await _context.OrderDetail
                         .Include(o => o.Order)

                          .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

       

        public async Task<OrderDetail> SelectOrderDetailById(int orderdetailId)
        {
            try
            {
                return await _context.OrderDetail
                    .Include(a => a.Order)
                   
                    .Include(p => p.Product)
                    .FirstOrDefaultAsync(orderdetail => orderdetail.Id == orderdetailId);
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
