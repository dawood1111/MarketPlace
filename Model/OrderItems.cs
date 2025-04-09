namespace api.Model
{
    public class OrderItems
    {
        public int Id { get; set; }
        public Order order { get; set; }
        public int OrderId { get; set; }     
        public Decimal Price { get; set; }
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public int Quantity { get; set; }
        public Decimal TotalPrice{get;set;}
        
    }
}