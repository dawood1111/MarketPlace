using api.Model;

namespace api.Interface
{
    public interface ICart
    {
        public Task<Cart> GetIdAsync(int id); 
        public Task<List<Cart>> GetAllAsync(string FindByEmail);

         
    }
}