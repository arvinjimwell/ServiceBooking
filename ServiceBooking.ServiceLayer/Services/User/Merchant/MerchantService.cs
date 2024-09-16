
using ServiceBooking.DataLayer;
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer.UserService;

public sealed class MerchantService(ServiceBookingContext dbContext) : UserServiceBase(dbContext), IMerchantService
{
    public bool SignUp(UserInputDTO input, out MerchantDto? result, out string errorMessage)
    {
        return SignUp<MerchantDto>(input, out result, out errorMessage);
    }
}
