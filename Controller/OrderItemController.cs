using System.Security.Authentication;
using api.Data;
using api.Extension;
using api.Model;
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
            var Cart=await _context.Carts.Include(ci=>ci.cartitem).Where(u=>u.UserId==FindEmail.Id).ToListAsync();
            
               var FindshippingAddress=await _context.shippingAddresses.FirstOrDefaultAsync(sa=>sa.UserId==FindEmail.Id);

           
            var FindOrder=await _context.Orders.FirstOrDefaultAsync(u=>u.UserId==FindEmail.Id);
            if(FindOrder==null){

            var order=new Order{
             UserId=FindEmail.Id,
              OrderDate=DateTime.Now,
              shippingAddress=FindshippingAddress,
              OrderStatus="Pending"
              
            };
            await _context.Orders.AddAsync(order);
             await _context.SaveChangesAsync();
            }
            
           
         
         decimal TotalPrice=0;
            foreach(var CartItem in Cart.SelectMany(c=>c.cartitem)){
                var orderItem=new OrderItems{
            
                OrderId=FindOrder.Id,
                ProductName=CartItem.ProductName,
               
                Quantity=CartItem.Quantity,
                Price=CartItem.Price
               
                };
              
                TotalPrice+=CartItem.Price*CartItem.Quantity;
                

                await _context.OrderItem.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
             
            

             FindOrder.TotalPrice=TotalPrice;
            

            return Ok(new{message="created succesfully"});




        }

        
    }
}