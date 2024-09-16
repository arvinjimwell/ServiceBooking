
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer.Users;

public interface IUserService
{
    bool MerchantSignUp(UserInputDTO input, out MerchantDto? result, out string errorMessage);
    bool ClientSignUp(UserInputDTO input, out ClientDto? result, out string errorMessage);
}
