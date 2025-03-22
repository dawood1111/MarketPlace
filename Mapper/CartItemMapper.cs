using api.Controller;
using api.DTO;
using api.Model;

namespace api.Mapper
{
    public static class Mapper
    {
        public static CartItem ToCartItem(this CartItemDto cartItem,decimal ProductPrice,int ProductId){
            return new CartItem{
                Quantity=cartItem.Quantity,
                Price=ProductPrice,
                ProductId=ProductId
               
              
            

            };


        }
        
    }
}