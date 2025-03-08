using api.Data;
using api.Interface;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CartRepo : ICart
    {
        private readonly ApplicationDBContext _context;
        public CartRepo(ApplicationDBContext context)
        {
            _context=context;
        }
       
         

        

        public async Task<Cart> GetIdAsync(int id)
        {
           return await _context.Carts.FirstOrDefaultAsync(c=>c.Id==id);
        }
    }
}