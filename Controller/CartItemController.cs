using api.Data;
using api.DTO;
using api.Extension;
using api.Interface;
using api.Mapper;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [ApiController]
    [Route("CartItem")]
    public class CartItemController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _user;
        private readonly ICartItem _cartItem;

        public CartItemController(ApplicationDBContext context,UserManager<User>user,ICartItem cartItem)
        {
            _context=context;
            _user=user;
            _cartItem=cartItem;
        }
       
        [HttpGet("Id")]
        public async Task<IActionResult> GetId([FromRoute] int id){
            return Ok(await _cartItem.GetIdAsync(id)) ;
        }


         [HttpPost("AddCartItem")]
         public async Task<IActionResult> Create([FromBody] List<CartItemDto> cartItemDto){
           var user=User.GetEmail();

          var FindEmail=  await _user.FindByEmailAsync(user);

          if(FindEmail==null){ return NotFound("email not found");} 



                     var cart = await _context.Carts
            .FirstOrDefaultAsync(c => c.UserId == FindEmail.Id); 
        
              foreach(var item in  cartItemDto){
                var product=await _context.Products.FirstOrDefaultAsync(p=>p.Name==item.ProductName);
                var cartItem =new CartItem{

                    ProductName=product.Name,
                    Quantity=item.Quantity,
                    Price=product.Price??0,
                    ProductId=product.Id,
                    CartId=cart.Id,
                    UserId=FindEmail.Id

                    

                };
               await _context.CartItems.AddAsync(cartItem);
              }
              await _context.SaveChangesAsync();
              
            
             

                   return Ok(new { message = "Items added to cart successfully" }); 
        }
       
       
        
    }
}