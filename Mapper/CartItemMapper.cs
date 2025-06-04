using api.Controller;
using api.DTO;
using api.Model;

namespace api.Mapper
{
    public static class Mapper
    {
        public static CartItem ToCartItem(this CartItemDto cartItem,decimal? ProductPrice,int ProductId,String productName,String userId){
            return new CartItem{
                
                Quantity=cartItem.Quantity,
                Price=ProductPrice??0m,
                ProductId=ProductId,
                ProductName=productName,
                UserId=userId
               
              
            

            };


        }
        
    }
}