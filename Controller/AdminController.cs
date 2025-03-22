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

        public AdminController(IAdmin UserAdmin,ApplicationDBContext context)
        {
            _UserAdmin=UserAdmin;
            _context=context;
            
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
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto,String CategoryName){
            var FindCategoryName=await _context.Categories.FirstOrDefaultAsync(n=>n.Name==CategoryName);
            int CategoryId=FindCategoryName.Id;
            var ProductModel=productDto.ToProduct(CategoryId);
            await _context.Products.AddAsync(ProductModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetId),new{id=ProductModel.Id},ProductModel);

        }

        [HttpPut("UpdateProduct")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult>Update(String Name,[FromBody] ProductDto productDto,String CategoryName){
            var FindProductName= await _context.Products.FirstOrDefaultAsync(p=>p.Name==Name);
            var FindCategoryName=await _context.Categories.FirstOrDefaultAsync(n=>n.Name==CategoryName);
            FindProductName.Name=productDto.Name;
            FindProductName.Price=productDto.Price;  
            FindProductName.Category_Id=FindCategoryName.Id;
            FindProductName.Description=productDto.Description;
            await _context.SaveChangesAsync();
            return Ok(FindProductName);

        }
        [HttpDelete("ProductDelete")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(String Name){
          var FindProductName= await _context.Products.FirstOrDefaultAsync(p=>p.Name==Name);
          _context.Products.Remove(FindProductName);
          await _context.SaveChangesAsync();
          return Ok(new{Message="Deleted Successfully"});


        }

          [HttpGet("GetAllOrders")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllOrders(){
            return Ok(await _context.Orders.Include(o=>o.orderItems).Include(s=>s.shippingAddress).ToListAsync());
        }
          [HttpPost("AddCategory")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCategory(String CategoryName){

            var Category=new Category{
              Name=CategoryName
            };

            await _context.Categories.AddAsync(Category);
            await _context.SaveChangesAsync();
            return Ok(new{Message="Created Successfully"});

        }

    }
}