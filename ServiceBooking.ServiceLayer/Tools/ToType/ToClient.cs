using ServiceBooking.DataLayer.EntityModels;
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer;

public static partial class Tools
{
    public static Client ToClient(this UserInputDTO input)
    {
        return new()
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            MiddleName = input.MiddleName,
            ContactInformation = new()
            {
                Email = input.Email,
                PhoneNumber = input.ContactNumber,
            },
            Credential = new()
            {
                UserName = input.Email,
                Password = input.Password
            },
            Bookings = []
        };
    }
}
