using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer.UserService;

public interface IClientService
{
    bool SignUp(UserInputDTO input, out ClientDto? result, out string errorMessages);
}
