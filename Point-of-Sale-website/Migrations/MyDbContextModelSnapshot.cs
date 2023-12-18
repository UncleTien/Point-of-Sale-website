﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Point_of_Sale_website.Models;

#nullable disable

namespace Point_of_Sale_website.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Point_of_Sale_website.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RequestApproved")
                        .HasColumnType("bit");

                    b.Property<string>("RequestedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Công nghệ thông tin",
                            RequestApproved = true,
                            RequestedBy = "StoreOwner",
                            RequestedDate = new DateTime(2023, 12, 10, 0, 22, 48, 770, DateTimeKind.Local).AddTicks(9049)
                        },
                        new
                        {
                            Id = 2,
                            Name = "Tâm lý xã hội",
                            RequestApproved = true,
                            RequestedBy = "StoreOwner",
                            RequestedDate = new DateTime(2023, 12, 10, 0, 22, 48, 770, DateTimeKind.Local).AddTicks(9062)
                        },
                        new
                        {
                            Id = 3,
                            Name = "kiến thức gia đình",
                            RequestApproved = true,
                            RequestedBy = "StoreOwner",
                            RequestedDate = new DateTime(2023, 12, 10, 0, 22, 48, 770, DateTimeKind.Local).AddTicks(9064)
                        });
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Trần Tuấn",
                            CategoryId = 1,
                            Description = "Mô tả cho cuốn sách Book 01",
                            Price = 100000m,
                            Title = "Book 01"
                        });
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Salespeople"
                        },
                        new
                        {
                            Id = 3,
                            Name = "HQAdministrator"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Customers"
                        });
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            FullName = "Admin",
                            Gender = "Nam",
                            IsLocked = false,
                            Password = "P@ssw0rd",
                            Phone = "0123456789",
                            Username = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "storeowner@gmail.com",
                            FullName = "Store Owner",
                            Gender = "Nam",
                            IsLocked = false,
                            Password = "P@ssw0rd",
                            Phone = "9876543210",
                            Username = "StoreOwner"
                        });
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 3
                        });
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.Order", b =>
                {
                    b.HasOne("Point_of_Sale_website.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.OrderDetail", b =>
                {
                    b.HasOne("Point_of_Sale_website.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Point_of_Sale_website.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.Product", b =>
                {
                    b.HasOne("Point_of_Sale_website.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.Role", b =>
                {
                    b.HasOne("Point_of_Sale_website.Models.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.UserRole", b =>
                {
                    b.HasOne("Point_of_Sale_website.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Point_of_Sale_website.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Point_of_Sale_website.Models.User", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
