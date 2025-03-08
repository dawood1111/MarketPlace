namespace api.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public String UserId { get; set; }=string.Empty;
        public User user { get; set; }
        public List<CartItem> cartitem { get; set; }=new List<CartItem>();
    }
}