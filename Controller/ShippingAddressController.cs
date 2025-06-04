using api.Data;
using api.DTO;
using api.Extension;
using api.Mapper;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    public class ShippingAddressController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _user;
        public ShippingAddressController(ApplicationDBContext context,UserManager<User>user)
        {
            _context=context;
            _user=user;
        }
         [HttpGet("GetId")]
        public async Task<IActionResult> GetId([FromRoute] int id){
            return Ok(await _context.shippingAddresses.FirstOrDefaultAsync(f=>f.Id==id));
        }
        [HttpPost("AddShoppingAddress")]
        public async Task<IActionResult> Create([FromBody]ShippingAddressDto shippingAddressDto){
           var GetEmail=User.GetEmail();
           var FindEmail=await _user.FindByEmailAsync(GetEmail);
          var ShippingAddressModel= shippingAddressDto.ToShippingAddress();
           ShippingAddressModel.UserId=FindEmail.Id;
          await _context.shippingAddresses.AddAsync(ShippingAddressModel);
          await _context.SaveChangesAsync();
     
          return CreatedAtAction(nameof(GetId),new{id=ShippingAddressModel.Id});
        }
        
    }
}