using api.Data;
using api.Interface;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CartItemRepos : ICartItem
    {
        private readonly ApplicationDBContext _context;
        public CartItemRepos(ApplicationDBContext context)
        {
            _context=context;
        }
        public async Task<CartItem> CareteCartItem(CartItem cart)
        {
        await _context.CartItems.AddAsync(cart);
          await _context.SaveChangesAsync();
          return cart;

           
        }

        public async  Task<CartItem> GetIdAsync(int id)
        {
          return await _context.CartItems.FirstOrDefaultAsync(ci=>ci.CartId==id);
        }
    }
}