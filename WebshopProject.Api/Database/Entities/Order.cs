using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Api.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Date")]  //this is a columnAttribute from System.CoponentModel.DataAnnotations (defined in enityframework.dll)
        public DateTime OrderDate { get; set; }
         
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new();
    }
}
