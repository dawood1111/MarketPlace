using api.Model;

namespace api.DTO
{
    public class ShippingAddressDto
    {
        public String City { get; set; }
        public String Country { get; set; }
        public String StreetAddress { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }
       public String UserId { get; set; }
    }
}