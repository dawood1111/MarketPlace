using System.ComponentModel.DataAnnotations;

namespace api.Model
{
    public class ShippingAddress
    {
        public int Id { get; set; }
       
        public String City { get; set; }
        public String Country { get; set; }
        public String StreetAddress { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }
        public Order order { get; set; }
          public User user { get; set; }
        public String UserId { get; set; }

    }
}