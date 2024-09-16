using Azure.Core;
using ServiceBooking.DataLayer;
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer.Users;

public partial class UserService(ServiceBookingContext dbContext) : IUserService
{
    private readonly ServiceBookingContext _dbContext = dbContext;

    public bool MerchantSignUp(UserInputDTO input, out MerchantDto? result, out string errorMessage)
    {
        try
        {
            return UserSignUpLogic<MerchantDto>(input, out result, out errorMessage);
        }
        catch(Exception ex)
        {
            result = null;
            errorMessage = $"An error occured: {ex.Message}";
            return false;
        }
    }

    public bool ClientSignUp(UserInputDTO input, out ClientDto? result, out string errorMessage)
    {
        try
        {
            return UserSignUpLogic<ClientDto>(input, out result, out errorMessage);
        }
        catch(Exception ex)
        {
            result = null;
            errorMessage = $"An error occured: {ex.Message}";
            return false;
        }
    }

}
