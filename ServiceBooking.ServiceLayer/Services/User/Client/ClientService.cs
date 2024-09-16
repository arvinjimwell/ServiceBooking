

using ServiceBooking.DataLayer;
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer.UserService;

public sealed class ClientService(ServiceBookingContext dbContext) : UserServiceBase(dbContext), IClientService
{
    public bool SignUp(UserInputDTO input, out ClientDto? result, out string errorMessages)
    {
        return SignUp<ClientDto>(input, out result, out errorMessages);
    }
}
