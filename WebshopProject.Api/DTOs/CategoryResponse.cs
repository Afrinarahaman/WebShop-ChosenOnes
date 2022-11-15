using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Api.DTOs
{
    public class CategoryResponse
    {
       
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public List<CategoryProductResponse> Products { get; set; } = new();
    }
    public class CategoryProductResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
    }
}
