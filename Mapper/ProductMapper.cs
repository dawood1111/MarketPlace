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
                FileName= product.Image.FileName,
                Category_Id=CategoryId
            };
        }

         public static Product ToProduct2(this ProductDto2 product,int CategoryId){
            return new Product{
                Name=product.Name,
                Price=product.Price,
                Category_Id=CategoryId
            };
        }
        
    }
}