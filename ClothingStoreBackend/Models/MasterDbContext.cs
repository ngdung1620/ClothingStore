using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreBackend.Models
{
    public class MasterDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<ApplicationRole> AspNetRoles { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GroupCategory> GroupCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Size> Sizes { get; set; }
        
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductCart> ProductCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        
        


        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductSize>().HasKey(ps => new { ps.ProductId, ps.SizeId });
            modelBuilder.Entity<ProductSize>()
                .HasOne<Product>(ps => ps.Product)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(ps => ps.ProductId);
            modelBuilder.Entity<ProductSize>()
                .HasOne<Size>(ps => ps.Size)
                .WithMany(s => s.ProductSizes)
                .HasForeignKey(ps => ps.SizeId);
            
            modelBuilder.Entity<ProductCart>()
                .HasOne<Product>(pc => pc.Product)
                .WithMany(p => p.ProductCarts)
                .HasForeignKey(pc => pc.ProductId);
            modelBuilder.Entity<ProductCart>()
                .HasOne<Cart>(pc => pc.Cart)
                .WithMany(c => c.ProductCarts)
                .HasForeignKey(pc => pc.CartId);
            
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<Cart>(b => b.UserId);
        }
    }
}