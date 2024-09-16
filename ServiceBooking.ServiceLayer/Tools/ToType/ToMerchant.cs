using ServiceBooking.DataLayer.EntityModels;
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer;

public static partial class Tools
{
    public static Merchant ToMerchant(this UserInputDTO input)
    {
        return new()
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            MiddleName = input.MiddleName,
            Credential = new()
            {
                UserName = input.Email,
                Password = input.Password,
            },
            ContactInformation = new()
            {
                Email = input.Email,
                PhoneNumber = input.ContactNumber,
            },
            Businesses = []
        };
    }
}
