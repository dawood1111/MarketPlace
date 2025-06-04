using api.Data;
using api.Interface;
using api.Model;
using Microsoft.AspNetCore.Http.HttpResults;
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

      
       public async  Task<List<Cart>> GetAllAsync(string FindByEmail)
        {
             return await _context.Carts.Include(c=>c.cartitem).Where(c=>c.UserId==FindByEmail).ToListAsync();       

              }

    }
}