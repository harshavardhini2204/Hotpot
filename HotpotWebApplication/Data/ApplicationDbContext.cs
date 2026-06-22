using HotpotWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace HotpotWebApplication.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment>Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasOne(e => e.Restaurant).WithOne(e => e.User).HasForeignKey<Restaurant>(r=>r.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasOne(e=>e.Cart).WithOne(c=>c.User).HasForeignKey<Cart>(e=>e.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany(e => e.Orders).WithOne(e => e.User).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Restaurant>().HasMany(e => e.Orders).WithOne(e => e.Restaurant).HasForeignKey(e => e.RestaurantId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Restaurant>().HasMany(e => e.MenuItems).WithOne(e => e.Restaurant).HasForeignKey(e => e.RestaurantId);
            modelBuilder.Entity<Category>().HasMany(e => e.MenuItems).WithOne(e => e.Category).HasForeignKey(e => e.CategoryId);
            modelBuilder.Entity<Cart>().HasMany(e => e.CartItems).WithOne(e => e.Cart).HasForeignKey(e => e.CartId);
            modelBuilder.Entity<MenuItem>().HasMany(e => e.CartItems).WithOne(e => e.MenuItem).HasForeignKey(e => e.MenuItemId);
            modelBuilder.Entity<MenuItem>().HasMany(e => e.CartItems).WithOne(e => e.MenuItem).HasForeignKey(e => e.MenuItemId);
            modelBuilder.Entity<Order>().HasMany(e => e.OrderItems).WithOne(e => e.Order).HasForeignKey(e => e.OrderId);
            modelBuilder.Entity<Order>().HasMany(e => e.Payments).WithOne(e => e.Order).HasForeignKey(e => e.OrderId);
            modelBuilder.Entity<MenuItem>().Property(e => e.Price).HasPrecision(18, 2);
            modelBuilder.Entity<MenuItem>().Property(e => e.DiscountPrice).HasPrecision(18, 2);
            modelBuilder.Entity<MenuItem>().Property(e => e.Carbs).HasPrecision(18, 2);
            modelBuilder.Entity<MenuItem>().Property(e => e.Protein).HasPrecision(18, 2);
            modelBuilder.Entity<MenuItem>().Property(e => e.Fat).HasPrecision(18, 2);
            modelBuilder.Entity<CartItem>().Property(e => e.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(e => e.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().Property(e => e.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().Property(e => e.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Payment>().Property(e => e.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(o => o.OrderStatus).HasConversion<string>();
            modelBuilder.Entity<Payment>().Property(p => p.PaymentStatus).HasConversion<string>();



        }

    }
}
