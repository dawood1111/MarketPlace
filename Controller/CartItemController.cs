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
         public async Task<IActionResult> Create([FromBody] CartItemDto cartItemDto,String ProductName){
             var user=User.GetEmail();
          var FindEmail=  await _user.FindByEmailAsync(user);
          if(FindEmail==null){ return NotFound("email not found");} 

            var FindProductName=await _context.Products.FirstOrDefaultAsync(p=>p.Name==ProductName);
            if(FindProductName==null)return NotFound("product not found");
          var cart = await _context.Carts
        .FirstOrDefaultAsync(c => c.UserId == FindEmail.Id);
              decimal price=FindProductName.Price;
              var ProductId=FindProductName.Id;
              
              

            var CartItemModel=cartItemDto.ToCartItem(price,ProductId);
             CartItemModel.CartId=cart.Id;
        
            await _cartItem.CareteCartItem(CartItemModel);
             

              return CreatedAtAction(nameof(GetId),new{id=CartItemModel.CartId,CartItemModel});
        }
       
       
        
    }
}