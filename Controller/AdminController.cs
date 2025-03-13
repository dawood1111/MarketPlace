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


        [HttpPost("CreateProduct")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto){
            var ProductModel=productDto.ToProduct();
            await _context.Products.AddAsync(ProductModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetId),new{id=ProductModel.Id},ProductModel);

        }

        [HttpPut("UpdateProduct")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult>Update(String Name,[FromBody] ProductDto productDto){
            var FindProductName= await _context.Products.FirstOrDefaultAsync(p=>p.Name==Name);
            FindProductName.Name=productDto.Name;
            FindProductName.Price=productDto.Price;  
            FindProductName.Category_Id=productDto.Category_Id;
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

    }
}