using Microsoft.EntityFrameworkCore;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.Helpers;

namespace WebshopProject.Api.Database
{
    public class WebshopProjectContext : DbContext
    {
        public WebshopProjectContext() { }
        public WebshopProjectContext(DbContextOptions<WebshopProjectContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
     
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>().HasIndex(c => c.Email).IsUnique();

            modelBuilder.Entity<Customer>().HasData(
             new()
             {
                 Id = 1,
                 Email = "peter@abc.com",
                 Password = "password",
                 FirstName = "Peter",
                 LastName = "Aksten",
                 Address = "House no:123 , 2700 Ghost street",
                 Telephone = "1245678",
                 Role = Role.Admin
             },

             new()
             {
                 Id = 2,
                 Email = "susana@abc.com",
                 Password = "password",
                 FirstName = "Susana",
                 LastName = "Andersan",
                 Address = "House no:486 , 3400 Green street",
                 Telephone = "12345678",
                 Role = Role.Customer
             },
               new()
               {
                   Id = 3,
                   Email = "Sara@abc.com",
                   Password = "password",
                   FirstName = "Sara",
                   LastName = "Khan",
                   Address = "House no:123 , 3400 Green street",
                   Telephone = "1999999",
                   Role = Role.Customer
               }
             );

            modelBuilder.Entity<Category>().HasData(
               new()
               {
                   Id = 1,
                   CategoryName = "Toy"


               },
               new()
               {
                   Id = 2,
                   CategoryName = "T-Shirt"
               }
               );
            modelBuilder.Entity<Product>().HasData(
                new()
                {
                    Id = 1,
                    Title = " Kids Microwave",
                    Price = 299.99M,
                    Description = "Kids Toys",
                    Image = "microwave.jpg",
                    Stock = 10,
                    CategoryId = 1

                },

                new()
                {
                    Id = 2,
                    Title = "Blue T-Shirt",
                    Price = 199.99M,
                    Description = "T-Shirt for boys",
                    Image = "BlueTShirt.jpg",
                    Stock = 10,
                    CategoryId = 2

                },

                new()
                {
                    Id = 3,
                    Title = " Kids Motorcycle",
                    Price = 599.99M,
                    Description = "Kids Toys",
                    Image = "motorcycle.jpg",
                    Stock = 10,
                    CategoryId = 1

                },
                new()
                {
                      Id = 4,
                      Title = " BabySofa",
                      Price = 399.99M,
                      Description = "Soft Baby Sofa for Babies",
                      Image = "BabySofa.jpg",
                      Stock = 10,
                      CategoryId = 1

                },
                new()
                {
                    Id = 5,
                    Title = "Red T-Shirt",
                    Price = 199.99M,
                    Description = "T-Shirt for kids",
                    Image = "RedT-Shirt.jpg",
                    Stock = 10,
                    CategoryId = 2

                }
              );

           

            
            


        }

       
    }

}
