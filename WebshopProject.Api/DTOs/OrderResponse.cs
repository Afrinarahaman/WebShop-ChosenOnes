using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Api.Database.Entities;

namespace WebshopProject.Api.DTOs
{
    public class OrderResponse
    {
        public int Id { get; set; }

       
        public DateTime OrderDate { get; set; }

       
        public int CustomerId { get; set; }

       
        public CustomerResponse Customer { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; } = new();

    }
   
}
