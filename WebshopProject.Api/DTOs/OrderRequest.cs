using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Api.DTOs
{
    public class OrderRequest
    {

        public DateTime OrderDate { get; set; }

        [Required]

        public int CustomerId { get; set; }

        [Required]
        public List<OrderDetailRequest> OrderDetails { get; set; }
}
}
