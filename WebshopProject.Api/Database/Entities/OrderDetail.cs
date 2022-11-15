using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Api.Database.Entities
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

      
        public int ProductId { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string ProductTitle { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal ProductPrice { get; set; }
               

        [Column(TypeName = "smallint")]
        public int Quantity { get; set; }

        //[Column(TypeName = "Date")]  //this is a columnAttribute from System.CoponentModel.DataAnnotations (defined in enityframework.dll)
        //public DateTime OrderDate { get; set; }

               
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
