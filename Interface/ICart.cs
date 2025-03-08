using api.Model;

namespace api.Interface
{
    public interface ICart
    {
        public Task<Cart> GetIdAsync(int id);

         
    }
}