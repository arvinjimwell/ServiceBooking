
using ServiceBooking.DataLayer.EntityModels;
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer;

public static partial class Tools
{
    public static TResult? ToUserDto<TValue, TResult>(this TValue? value)
        where TValue : UserBase
        where TResult : UserDTOBase
    {
        return value switch
        {
            Client client => client.MapProperty<Client, ClientDto>() as TResult,
            Merchant merchant => merchant.MapProperty<Merchant, MerchantDto>() as TResult,
            _ => null
        };
    }

    private static TResult MapProperty<TValue, TResult>(this TValue value)
        where TValue : UserBase
        where TResult : UserDTOBase, new()
    {
        TResult model = new()
        {
            Id = value.Id.ToString(),
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
            Email = value.ContactInformation.Email,
            ContactNumber = value.ContactInformation.PhoneNumber,
        };

        if(value.GetType().Equals(typeof(Client)))
        {
            var clientDto = model as ClientDto;
            clientDto!.Bookings = [];
            return clientDto as TResult ?? new();
        }
        else if(value.GetType().Equals(typeof(Merchant)))
        {
            var merchantDto = model as MerchantDto;
            merchantDto!.Businesses = [];
            return merchantDto as TResult ?? new();
        }
        
        return new();
    }

}
