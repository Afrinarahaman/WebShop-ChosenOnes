using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebshopProject.Api.Database.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName="nvarchar(20)")]
        public string CategoryName { get; set; }
       

        public List<Product> Products { get; set; } = new();
    }
}
