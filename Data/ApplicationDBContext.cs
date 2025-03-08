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