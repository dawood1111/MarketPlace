namespace api.Model
{
    public class Category
    {
        public int Id { get; set; }
        public String Name { get; set; }=string.Empty;
        public List<Product> product { get; set; }=new List<Product>();
    }
}