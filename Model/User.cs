using Microsoft.AspNetCore.Identity;

namespace api.Model
{
    public class User:IdentityUser
    {
      public Cart Carts { get; set; }
        
    }
}