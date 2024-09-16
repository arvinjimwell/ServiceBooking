
namespace ServiceBooking.DTO.UserDto;

public abstract class UserDTOBase
{
    public string? Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string[] ContactNumber { get; set; } = [];
}
 