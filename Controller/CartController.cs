using api.Data;
using api.Extension;
using api.Interface;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<IActionResult> GetId([FromRoute] int id){
           var id11=await _cart.GetIdAsync(id);
           if(id11==null){
            return NotFound();
           }
            return Ok(id11) ;
        }
       
       
        
      

      
        
    }
}