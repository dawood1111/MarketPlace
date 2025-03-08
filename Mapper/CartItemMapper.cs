using api.Controller;
using api.DTO;
using api.Model;

namespace api.Mapper
{
    public static class Mapper
    {
        public static CartItem ToCartItem(this CartItemDto cartItem,int productId){
            return new CartItem{
                Quantity=cartItem.Quantity,
                ProductId=productId,
              
            

            };


        }
        
    }
}