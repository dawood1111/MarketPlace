using api.DTO;
using api.Model;

namespace api.Mapper
{
    public static class ShippingAddressMapper
    {
        public static ShippingAddress ToShippingAddress(this ShippingAddressDto shippingAddressDto){
            return new ShippingAddress{
                 FirstName=shippingAddressDto.FirstName,
                 LastName=shippingAddressDto.LastName,  
                 Country=shippingAddressDto.Country,
                 StreetAddress=shippingAddressDto.StreetAddress,
                 PhoneNumber=shippingAddressDto.PhoneNumber,
            };
        }
    }
}