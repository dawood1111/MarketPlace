using api.Data;
using api.DTO;
using api.Interface;
using api.Mapper;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    
    [ApiController]
    [Route("UserAdmin")]
    public class AdminController:ControllerBase
    {
        private readonly IAdmin _UserAdmin;
        private readonly ApplicationDBContext _context;
        private readonly IAdminController _IAdminRepo;

        public AdminController(IAdmin UserAdmin,ApplicationDBContext context,IAdminController IAdminRepo)
        {
            _UserAdmin=UserAdmin;
            _context=context;
            _IAdminRepo=IAdminRepo;
            
        }

        [HttpPost("CreateAdmin")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> CreateAdmin(){

            await _UserAdmin.CreateAdmin();

            return Ok("Creation Completed");

        }



        [HttpGet("Id")]
        [Authorize(Roles ="Admin")]
    
        public async Task<IActionResult> GetId([FromRoute] int id){
            return Ok(await _context.Products.FindAsync(id));
        }




        [HttpPost("AddProduct")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromForm] ProductDto productDto,String CategoryName){
            var IAdd=await _IAdminRepo.AddProductAsync(productDto,CategoryName);
           
            return CreatedAtAction(nameof(GetId) ,new{id=IAdd.Id},IAdd);

        }




        [HttpPut("UpdateProduct")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult>Update(String Name,String NewName,decimal? NewPrice,String  NewDescription){

            var IUpdate=await _IAdminRepo.ProductUpdate(Name,NewName,NewDescription,NewPrice);

            return Ok(IUpdate);

        }




        [HttpDelete("ProductDelete")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(String Name){
       
          var IDelete= await _IAdminRepo.DeleteProductAsync(Name);

           return Ok(IDelete);

        }





          [HttpGet("GetAllOrders")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllOrders(){
            return Ok(await _context.Orders.Include(o=>o.orderItems).Include(s=>s.shippingAddress).ToListAsync());
        }





          [HttpPost("AddCategory")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCategory(String CategoryName){

           await _IAdminRepo.AddCategoryAsync(CategoryName);
    
            return Ok(new{Message="Created Successfully"});

        }

        [HttpGet("GetOrderItems")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetOrderItems(){
            return Ok(await _context.OrderItem.ToListAsync());
        }

    }
}