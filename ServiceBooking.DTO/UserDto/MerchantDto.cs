

namespace ServiceBooking.DTO.UserDto;

public class MerchantDto : UserDTOBase
{
    private const string ROLE = "Merchant";

    public string Role { get { return ROLE; } }
    public string[] Businesses { get; set; } = [];
}
