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
        [HttpGet("GetAll")]
       
        [HttpGet("Id")]
        public async Task<IActionResult> GetId([FromRoute] int id){
            return Ok(await _cartItem.GetIdAsync(id)) ;
        }
         [HttpPost("AddCartItem")]
         public async Task<IActionResult> Create([FromBody] CartItemDto cartItemDto,int ProductId){
             var user=User.GetEmail();
          var FindEmail=  await _user.FindByEmailAsync(user);
          if(FindEmail==null){ return NotFound("email not found");} 

            var FindProductId=await _context.Products.FirstOrDefaultAsync(p=>p.Id==ProductId);
            if(FindProductId==null)return NotFound("product not found");
          var cart = await _context.Carts
        .FirstOrDefaultAsync(c => c.UserId == FindEmail.Id);

            var CartItemModel=cartItemDto.ToCartItem(ProductId);
             CartItemModel.CartId=cart.Id;
        
            await _cartItem.CareteCartItem(CartItemModel);
             

              return CreatedAtAction(nameof(GetId),new{id=CartItemModel.CartId,CartItemModel});
        }
       
       
        
    }
}