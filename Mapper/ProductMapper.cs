using api.DTO;
using api.Model;

namespace api.Mapper
{
    public static class ProductMapper
    {
        public static Product ToProduct(this ProductDto product){
            return new Product{
                Name=product.Name,
                Price=product.Price,
                Description=product.Description,
                Category_Id=product.Category_Id
            };
        }
        
    }
}