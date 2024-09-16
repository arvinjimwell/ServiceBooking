

using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer.UserService;

public interface IMerchantService
{
    bool SignUp(UserInputDTO input, out MerchantDto? result, out string errorMessage);
}
