

namespace ServiceBooking.DTO.UserDto;

public class MerchantDto : UserDTOBase
{
    public string[] Businesses { get; set; } = [];
    public string Role { get { return ROLE; } }
    private const string ROLE = "Merchant";
}
