using Microsoft.AspNetCore.Identity;

namespace api.Model
{
    public class User:IdentityUser
    {
      public String Role { get; set; }
      public Cart Carts { get; set; }
      public List<Order> Orders { get; set; }=new List<Order>();
      public List<CartItem> cartItems { get; set; }=new List<CartItem>();
      public List<ShippingAddress> shippingAddresses { get; set; }=new List<ShippingAddress>();

      

    }
}