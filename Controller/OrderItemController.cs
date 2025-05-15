using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using api.Data;
using api.Extension;
using api.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [ApiController]
    [Route("OrderItem")]
    public class OrderItemController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _user;
        public OrderItemController(ApplicationDBContext context ,UserManager<User> user)
        {
            _context=context;
            _user=user;
        }
        [HttpGet("GetId")]
        public async Task<IActionResult> GetId([FromRoute] int id){
            return Ok(await _context.OrderItem.FirstOrDefaultAsync(f=>f.Id==id));
        }
        [HttpPost("CreateOrderItem")]
        public async Task<IActionResult> Create(){

            var GetEmail=User.GetEmail();

            var FindEmail=await _user.FindByEmailAsync(GetEmail);
           /*
            var Cart=await _context.Carts.Include(ci=>ci.cartitem).Where(u=>u.UserId==FindEmail.Id).ToListAsync();
            */
            var userCart=await _context.Carts.FirstOrDefaultAsync(c=>c.UserId==FindEmail.Id);
            if (userCart == null)
             {
             return BadRequest(new { message = "Cart not found for this user." });
               }

            var CartItem=await _context.CartItems.Where(ci=>ci.CartId==userCart.Id).ToListAsync();

            if (CartItem == null || !CartItem.Any())
                  {
               return BadRequest(new { message = "No items in the cart." });
                    }
            
            var FindshippingAddress=await _context.shippingAddresses.FirstOrDefaultAsync(sa=>sa.UserId==FindEmail.Id);
            if (FindshippingAddress == null)

              return BadRequest(new { message = "Shipping address not found" });

           
            var FindOrder=await _context.Orders.FirstOrDefaultAsync(u=>u.UserId==FindEmail.Id);

            if(FindOrder==null){

            var order=new Order{

             UserId=FindEmail.Id,
             OrderDate=DateTime.Now,
             ShippingAddressId=FindshippingAddress.Id,
             OrderStatus="Pending",
             TotalPrice=0
       
              
            };

            await _context.Orders.AddAsync(order);
             await _context.SaveChangesAsync();

            FindOrder=order;
            
            }
        
           
         

            foreach(var cartItems in CartItem){
                var orderItem=new OrderItems{
            
                OrderId=FindOrder.Id,
                ProductName=cartItems.ProductName,
                Quantity=cartItems.Quantity,
                Price=cartItems.Price,
                TotalPrice=cartItems.Price*cartItems.Quantity
                

                };

                FindOrder.TotalPrice+=orderItem.TotalPrice;
              

                await _context.OrderItem.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
            return Ok(new{message="Created Succefully"});
             
            
                

            

            


           
        }
    }
    }
        
    
