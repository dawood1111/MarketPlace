using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [ApiController]
    [Route("Api/Order")]
    public class OrderController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public OrderController(ApplicationDBContext context)
        {
            _context=context;
        }
        [HttpPut("UpdateOrderStatus")]
        public async Task<IActionResult> Update(int OrderId,String orderStatus){
            
         var FindId= await _context.Orders.FirstOrDefaultAsync(p=>p.Id==OrderId);

         if(FindId==null){

            return NotFound(new{message="Error"});
         }
         FindId.OrderStatus=orderStatus;

         return Ok(new{message="Updated Succefully"});

        }

    
    }
}