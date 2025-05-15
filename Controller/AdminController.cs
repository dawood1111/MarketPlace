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

        [HttpPut("UpdateOrderStatus")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(int OrderId,[FromBody] UpdateStatus orderStatus){
            
         var FindId= await _context.Orders.FirstOrDefaultAsync(p=>p.Id==OrderId);

         if(FindId==null){

            return NotFound(new{message="Error"});
         }
         FindId.OrderStatus=orderStatus.orderStatus;
         await _context.SaveChangesAsync();

         return Ok(new{message="Updated Succefully"});

        }
        [HttpDelete("DeleteOrder")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteOrder(int OrderId){
            var FindOrders=await _context.Orders.FirstOrDefaultAsync(o=>o.Id==OrderId);
            if(FindOrders==null){
                return NotFound();
            }
            _context.Orders.Remove(FindOrders);
            await _context.SaveChangesAsync();
            return Ok(new{message="Deleted Succefully"});

        }
    [HttpGet("GetStatus")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> GetStatus(){
        var OrderStatus=await _context.Orders.ToListAsync();
        var StatusCount= OrderStatus.GroupBy(o=>o.OrderStatus).Select(g=>new{
            status=g.Key,
            count=g.Count()
        }).ToList();
        return Ok(StatusCount);
    }
       [HttpGet("UserPerMonth")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUserPerMonth(){
            var PerDayUser=await _context.Users.GroupBy(u=>u.CreatedAt.Day).Select(
                g=>new{
                    Day=g.Key,
                    count=g.Count()
                }
            ).OrderBy(g=>g.Day).ToListAsync();
            return Ok(PerDayUser);
            
        }


        [HttpPost("AddProduct2")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto2 productDto,String CategoryName){


              var FindCategoryName=await _context.Categories.FirstOrDefaultAsync(n=>n.Name==CategoryName);

               int CategoryId=FindCategoryName.Id;

             

              var ProductModel=productDto.ToProduct2(CategoryId);

              await _context.Products.AddAsync(ProductModel);
               await _context.SaveChangesAsync();

                  
           
            return CreatedAtAction(nameof(GetId) ,new{id=ProductModel.Id});

        }
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProduct(){
            var LastProductAdded=await _context.Products.ToListAsync();
            if(LastProductAdded==null){
                return NotFound("no product");
            }
            return Ok(LastProductAdded);
        }
        

        

    }
}