
namespace ServiceBooking.DTO.UserDto;

public class ClientDto : UserDTOBase
{
    public string[] Bookings = [];
    public const string Role = "Client";
}
