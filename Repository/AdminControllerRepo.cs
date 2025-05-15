using api.Data;
using api.DTO;
using api.Interface;
using api.Mapper;
using api.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Repository
{
    public class AdminControllerRepo:IAdminController
    {
        private readonly ApplicationDBContext _context;
          public AdminControllerRepo(ApplicationDBContext context)
        {
            _context=context;
            
        }

        public async Task<string> AddCategoryAsync(string CategoryName)
        {

              var Category=new Category{
              Name=CategoryName
            };

            await _context.Categories.AddAsync(Category);
            await _context.SaveChangesAsync();

                   return "Successed";
        }


        public async Task<Product> AddProductAsync([FromForm]ProductDto productDto, string categoryName)
        {
       
          var FindCategoryName=await _context.Categories.FirstOrDefaultAsync(n=>n.Name==categoryName);

               int CategoryId=FindCategoryName.Id;

               string fileName=Path.GetFileName(productDto.Image.FileName);
               
               string FolderPath=@"C:\Users\user\Desktop\image";
               
               string path=Path.Combine(FolderPath,fileName);

               using(Stream stream=new FileStream(path,FileMode.Create) ){

                await productDto.Image.CopyToAsync(stream);

               }

               var ProductModel=productDto.ToProduct(CategoryId);

              await _context.Products.AddAsync(ProductModel);
               await _context.SaveChangesAsync();


                  return ProductModel;

       

             

         }

   

        public async Task<string> DeleteProductAsync(string Name)
        {
             var FindProductName= await _context.Products.FirstOrDefaultAsync(p=>p.Name==Name);
          _context.Products.Remove(FindProductName);
          await _context.SaveChangesAsync();
          return "Deleted Successfully";
        }



        public async Task<Product> ProductUpdate(string productName, string newName, string newDescription, decimal? newPrice)
        {
          var FindNameAsync=await _context.Products.FirstOrDefaultAsync(p=>p.Name==productName);
             if(!string.IsNullOrEmpty(newName)){
               FindNameAsync.Name=newName;
          }
           
           if(newPrice.HasValue){
               FindNameAsync.Price=newPrice;
          }

            await _context.SaveChangesAsync();

           return FindNameAsync ;
        }

        
   
    }
}