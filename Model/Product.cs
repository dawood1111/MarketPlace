using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }=string.Empty;
        [Column(TypeName ="decimal (10,2)")]
        [Required]
        public Decimal Price { get; set; }
        public String Description { get; set; }=string.Empty;
        public int Category_Id { get; set; }
        public Category Category1 { get; set; }
        public List<CartItem> cartItem { get; set; }=new List<CartItem>();
    }
}