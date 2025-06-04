using api.DTO;
using api.Model;

namespace api.Interface
{
    public interface IAdminController
    {
     public Task<Product> ProductUpdate(string productName , string newName,string newDescription , decimal? newPrice);
     public  Task<string> DeleteProductAsync(string Name);

     public Task<Product> AddProductAsync(ProductDto productDto,string categoryName);

     public Task<string> AddCategoryAsync(string CategoryName);
    }
}