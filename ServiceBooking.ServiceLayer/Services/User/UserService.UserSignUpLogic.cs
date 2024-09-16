using Microsoft.EntityFrameworkCore;
using ServiceBooking.DataLayer.EntityModels;
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer.Users;

public partial class UserService
{
    private bool UserSignUpLogic<TResult>(UserInputDTO input, out TResult? result, out string errors) where TResult : UserDTOBase
    {
        List<string> registeredEmails = _dbContext.Users.Select(u => u.Credential.UserName).ToList();
        if(registeredEmails.Contains(input.Email))
        {
            result = null;
            errors = "Email is already registered.";
            return false;
        }

        object modelToSave = ConvertInputToUserType<TResult>(input);
        _dbContext.Add(modelToSave);
        if(!_dbContext.SaveAndCheckIfSuccessful())
        {
            result = null;
            errors = "Failed to save the user to the database.";
            return false;
        }

        var model = GetUserByType<TResult>(input.Email);
        if(model is null) { throw new Exception("Unable to find the user."); }

        result = ConvertToTypeDto<TResult>(model);
        errors = null!;
        return true;
    }

    private object ConvertInputToUserType<TValue>(UserInputDTO input) where TValue : UserDTOBase
    {
        return typeof(TValue) switch
        {
            Type t when t == typeof(ClientDto) => input.ToClient(),
            Type t when t == typeof(MerchantDto) => input.ToMerchant(),
            _ => throw new Exception("Undefined user type.")
        };
    }

    private object? GetUserByType<TResult>(string username) where TResult : UserDTOBase
    {
        return typeof(TResult) switch
        {
            Type t when t == typeof(ClientDto) => _dbContext.FindClientByUserNameNoTracking(username),
            Type t when t == typeof(MerchantDto) => _dbContext.FindMerchantByUserNameNoTracking(username),
            _ => throw new Exception("Undefined user type.")
        };
    }

    private TResult? ConvertToTypeDto<TResult>(object value) where TResult : UserDTOBase
    {
        return typeof(TResult) switch
        {
            Type t when t == typeof(ClientDto) => (value as Client)?.ToUserDto<Client, ClientDto>() as TResult,
            Type t when t == typeof(MerchantDto) => (value as Merchant)?.ToUserDto<Merchant, MerchantDto>() as TResult,
            _ => throw new Exception("Undefined user type.")
        };
    }
}
