using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Point_of_Sale_website.Models;
public class MyDbContext : DbContext
{

    public MyDbContext(DbContextOptions<MyDbContext> options)
             : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "Admin",
                Password = "P@ssw0rd",
                FullName = "Admin",
                Email = "admin@gmail.com",
                Phone = "0123456789",
                Gender = "Nam",
                IsLocked = false,
            },
            new User
            {
                Id = 2,
                Username = "StoreOwner",
                Password = "P@ssw0rd",
                FullName = "Store Owner",
                Email = "storeowner@gmail.com",
                Phone = "9876543210",
                Gender = "Nam",
                IsLocked = false,
            });

        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Name = "Admin"
            },
            new Role
            {
                Id = 2,
                Name = "Salespeople"
            },
            new Role
            {
                Id = 3,
                Name = "HQAdministrator"
            },
            new Role
            {
                Id = 4,
                Name = "Customers"
            }
            );

        modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = 1,
                RoleId = 1
            },
            new UserRole
            {
                UserId = 2,
                RoleId = 3
            });

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Book 01",
                Author = "Trần Tuấn",
                Description = "Mô tả cho cuốn sách Book 01",
                Price = 100000,
                CategoryId = 1,
            });

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Công nghệ thông tin",
                RequestedBy = "StoreOwner",
                RequestedDate = DateTime.Now,
                RequestApproved = true
            },
             new Category
             {
                 Id = 2,
                 Name = "Tâm lý xã hội",
                 RequestedBy = "StoreOwner",
                 RequestedDate = DateTime.Now,
                 RequestApproved = true
             },
             new Category
             {
                 Id = 3,
                 Name = "kiến thức gia đình",
                 RequestedBy = "StoreOwner",
                 RequestedDate = DateTime.Now,
                 RequestApproved = true
             }
            );

        base.OnModelCreating(modelBuilder);
    }
}