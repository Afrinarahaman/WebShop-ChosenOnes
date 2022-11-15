﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebshopProject.Api.Database;

namespace WebshopProject.Api.Migrations
{
    [DbContext(typeof(WebshopProjectContext))]
    partial class WebshopProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebshopProject.Api.Database.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Toy"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "T-Shirt"
                        });
                });

            modelBuilder.Entity("WebshopProject.Api.Database.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "House no:123 , 2700 Ghost street",
                            Email = "peter@abc.com",
                            FirstName = "Peter",
                            LastName = "Aksten",
                            Password = "password",
                            Role = 0,
                            Telephone = "1245678"
                        },
                        new
                        {
                            Id = 2,
                            Address = "House no:486 , 3400 Green street",
                            Email = "susana@abc.com",
                            FirstName = "Susana",
                            LastName = "Andersan",
                            Password = "password",
                            Role = 1,
                            Telephone = "12345678"
                        },
                        new
                        {
                            Id = 3,
                            Address = "House no:123 , 3400 Green street",
                            Email = "Sara@abc.com",
                            FirstName = "Sara",
                            LastName = "Khan",
                            Password = "password",
                            Role = 1,
                            Telephone = "1999999"
                        });
                });

            modelBuilder.Entity("WebshopProject.Api.Database.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("WebshopProject.Api.Database.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(6,2)");

                    b.Property<string>("ProductTitle")
                        .HasColumnType("nvarchar(32)");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("WebshopProject.Api.Database.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(32)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6,2)");

                    b.Property<short>("Stock")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Kids Toys",
                            Image = "microwave.jpg",
                            Price = 299.99m,
                            Stock = (short)10,
                            Title = " Kids Microwave"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "T-Shirt for boys",
                            Image = "BlueTShirt.jpg",
                            Price = 199.99m,
                            Stock = (short)10,
                            Title = "Blue T-Shirt"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Kids Toys",
                            Image = "motorcycle.jpg",
                            Price = 599.99m,
                            Stock = (short)10,
                            Title = " Kids Motorcycle"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Soft Baby Sofa for Babies",
                            Image = "BabySofa.jpg",
                            Price = 399.99m,
                            Stock = (short)10,
                            Title = " BabySofa"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            Description = "T-Shirt for kids",
                            Image = "RedT-Shirt.jpg",
                            Price = 199.99m,
                            Stock = (short)10,
                            Title = "Red T-Shirt"
                        });
                });

            modelBuilder.Entity("WebshopProject.Api.Database.Entities.Order", b =>
                {
                    b.HasOne("WebshopProject.Api.Database.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WebshopProject.Api.Database.Entities.OrderDetail", b =>
                {
                    b.HasOne("WebshopProject.Api.Database.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebshopProject.Api.Database.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebshopProject.Api.Database.Entities.Product", b =>
                {
                    b.HasOne("WebshopProject.Api.Database.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WebshopProject.Api.Database.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebshopProject.Api.Database.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}