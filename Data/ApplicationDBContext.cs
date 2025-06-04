using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext:IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Cart> Carts{get;set;}
        public DbSet<Product> Products{get;set;}
        public DbSet<CartItem> CartItems{get;set;}
        public DbSet<Category> Categories{get;set;}
          public DbSet<OrderItems> OrderItem{get;set;}
        public DbSet<Order> Orders{get;set;}
        public DbSet<ShippingAddress> shippingAddresses{get;set;}







        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            
           builder.Entity<CartItem>().
           HasOne(c=>c.Carts).
           WithMany(ci=>ci.cartitem).
           HasForeignKey(c=>c.CartId);

             builder.Entity<Product>().
           HasOne(c=>c.Category1).
           WithMany(p=>p.product).
           HasForeignKey(c=>c.Category_Id);

            builder.Entity<Cart>().
           HasOne(a=>a.user).
           WithOne(c=>c.Carts).
           HasForeignKey<Cart>(a=>a.UserId);

          builder.Entity<Order>().
           HasOne(s=>s.shippingAddress).
           WithOne(o=>o.order).
           HasForeignKey<Order>(s=>s.ShippingAddressId);

          builder.Entity<OrderItems>().
           HasOne(o=>o.order).
           WithMany(oi=>oi.orderItems).
           HasForeignKey(o=>o.OrderId);

             builder.Entity<Order>().
           HasOne(u=>u.user).
           WithMany(o=>o.Orders).
           HasForeignKey(u=>u.UserId);

            builder.Entity<CartItem>().
           HasOne(u=>u.user).
           WithMany(ci=>ci.cartItems).
           HasForeignKey(u=>u.UserId);

         builder.Entity<ShippingAddress>().
           HasOne(u=>u.user).
           WithMany(sa=>sa.shippingAddresses).
           HasForeignKey(u=>u.UserId);

            builder.Entity<CartItem>().
           HasOne(u=>u.product).
           WithMany(sa=>sa.cartItem).
           HasForeignKey(u=>u.ProductId);


  




           
            List<IdentityRole> role =new List<IdentityRole>{
                 new IdentityRole{
                    Name="User",
                    NormalizedName="USER"
                 },
                 new IdentityRole{
                  Name="Admin",
                  NormalizedName="ADMIN"
                 }
            }; 
            builder.Entity<IdentityRole>().HasData(role);
        }


    }
}