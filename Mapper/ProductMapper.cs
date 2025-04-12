using api.DTO;
using api.Model;

namespace api.Mapper
{
    public static class ProductMapper
    {
        public static Product ToProduct(this ProductDto product,int CategoryId){
            return new Product{
                Name=product.Name,
                Price=product.Price,
                Description=product.Description,
                FileName=product.Image.FileName,
                Category_Id=CategoryId
            };
        }
        
    }
}