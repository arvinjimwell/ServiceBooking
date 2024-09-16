using Azure.Core;
using ServiceBooking.DataLayer;
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer.UserService;

public abstract partial class UserServiceBase(ServiceBookingContext dbContext)
{
    protected readonly ServiceBookingContext _dbContext = dbContext;

    /// <summary>
    /// Wrap the sign up logic
    /// </summary>
    /// <typeparam name="TypeDto">The DTO class of the target user type.</typeparam>
    /// <param name="input">The data to be map.</param>
    /// <param name="result">The new instance of the TypeDto</param>
    /// <param name="errorMessage">The error message.</param>
    /// <returns>Return true if successfully signup, else false</returns>
    protected bool SignUp<TypeDto>(UserInputDTO input, out TypeDto? result, out string errorMessage) where TypeDto : UserDTOBase
    {
        try
        {
            return SignUpLogic<TypeDto>(input, out result, out errorMessage);
        }
        catch(Exception ex)
        {
            result = null;
            errorMessage = $"An error occured: {ex.Message}";
            return false;
        }
    }
}
