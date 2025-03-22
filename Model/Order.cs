namespace api.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int ShippingAddressId { get; set; }
        public ShippingAddress shippingAddress { get; set; }
        public String OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public String  UserId { get; set; }
        public User user { get; set; }
        public Decimal TotalPrice { get; set; }
        public List<OrderItems> orderItems { get; set; }=new List<OrderItems>();

    }
}