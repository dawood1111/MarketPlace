using api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [ApiController]
    [Route("DashBoard")]
    public class DashBoradController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public DashBoradController(ApplicationDBContext context)
        {
            _context=context;
            
        }
        [HttpGet("TotalRevenue")]
        [Authorize(Roles ="Admin")]
            public async Task<IActionResult> GetTotalRevenue(){
            var TotalRevenue=await _context.Orders.SumAsync(o=>o.TotalPrice);
          
            return Ok(new{TotalRevenue});
        }
          
           [HttpGet("TotalBuyers")]
        [Authorize(Roles ="Admin")]
            public async Task<IActionResult> GetTotalBuyers(){
        
            var TotalBuyers=await _context.Users.CountAsync(u=>_context.Orders.Any(o=>o.UserId==u.Id));
            return Ok(new{TotalBuyers});
        }
        
           [HttpGet("TotalOrders")]
        [Authorize(Roles ="Admin")]
            public async Task<IActionResult> GetTotalOrders(){
            var TotalOrders=await _context.Orders.CountAsync();
          
            return Ok(new{TotalOrders});
        }
        
    }
}