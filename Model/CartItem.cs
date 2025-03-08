namespace api.Model
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product product { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public Cart Carts { get; set; }
        public int Quantity { get; set; }

    }
}