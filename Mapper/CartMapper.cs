using api.DTO;
using api.Model;

namespace api.Mapper
{
    public static class CartMapper
    {
    public static Cart ToCart(this CartDto cartDto){
        return new Cart{
          UserId=cartDto.UserID
        };
    }
        
    }
}