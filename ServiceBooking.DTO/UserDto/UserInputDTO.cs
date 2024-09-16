

namespace ServiceBooking.DTO.UserDto;

public class UserInputDTO : UserDTOBase
{
    public string Password { get; set; } = string.Empty;
    public string? OldPassword { get; set; }
}
