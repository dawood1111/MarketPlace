using api.Model;

namespace api.Interface
{
    public interface ICartItem
    {
         public Task<CartItem> CareteCartItem(CartItem cart);
         public Task<CartItem> GetIdAsync(int id);
    }
}