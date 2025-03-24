using api.Data;
using api.Extension;
using api.Interface;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [ApiController]
    [Route("Cart")]

    public class CartController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _user;
        private readonly ICart _cart;
        public CartController(ApplicationDBContext context,UserManager<User> user,ICart cart)
        {
            _context=context;
            _user=user;
            _cart=cart;
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetId([FromRoute] int id){
           var id11=await _cart.GetIdAsync(id);
           if(id11==null){
            return NotFound();
           }
            return Ok(id11) ;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(){       
                var GetEmail=User.GetEmail();
                var FindByEmail=await _user.FindByEmailAsync(GetEmail);
                var GetAll=await _context.Carts.Include(c=>c.cartitem).Where(c=>c.UserId==FindByEmail.Id).ToListAsync();
                return Ok(GetAll);


        }
       
       
        
      

      
        
    }
}