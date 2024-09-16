
namespace ServiceBooking.DTO.UserDto;

public class ClientDto : UserDTOBase
{
    private const string ROLE = "Client";

    public string Role { get { return ROLE; } }
    public string[] Bookings { get; set; } = []; 
}
