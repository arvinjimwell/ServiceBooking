using Microsoft.EntityFrameworkCore;
using ServiceBooking.DataLayer.EntityModels;
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.ServiceLayer.UserService;

public abstract partial class UserServiceBase
{
    /// <summary>
    ///  This function wrap the logic behind user signup.
    /// </summary>
    /// <typeparam name="TResult">DTO class of the user</typeparam>
    /// <param name="input">The details to map to create the user</param>
    /// <param name="result">The DTO class containing all the input data of user</param>
    /// <param name="errors">The errors message</param>
    /// <returns>Return true if the sign up sucessful, else false.</returns>
    /// <exception cref="Exception"></exception>
    private bool SignUpLogic<TResult>(UserInputDTO input, out TResult? result, out string errors) 
        where TResult : UserDTOBase
    {
        result = null;
        errors = null!;

        if(IsEmailTaken(input.Email))
        {
            errors = "Email is already registered.";
            return false;
        }

        if(!MapInputAndSaveToDatabase<TResult>(input))
        {
            errors = "Failed to save the user to the database.";
            return false;
        }

        result = GetUser<TResult>(input.Email);
        return true;
    }

    private bool IsEmailTaken(string email)
    {
        List<string> registeredEmails = [.. _dbContext.Users.Select(u => u.Credential.UserName)];
        return registeredEmails.Contains(email);
    }

    /// <summary>
    /// Create a new instance of TValue and save it to the database.
    /// </summary>
    /// <typeparam name="TValue">Class of the User in the database</typeparam>
    /// <param name="input">User data that need to be map</param>
    /// <returns>Returns true if the new instance successfully saved in the database, else return false.</returns>
    private bool MapInputAndSaveToDatabase<TValue>(UserInputDTO input) where TValue : UserDTOBase
    {
        object modelToSave = ToUserType<TValue>(input);
        _dbContext.Add(modelToSave);
        return _dbContext.SaveAndCheckIfSuccessful();
    }

    /// <summary>
    /// Create a new instance of TValue
    /// </summary>
    /// <typeparam name="TValue">The DTO class of the User Class</typeparam>
    /// <param name="input">User data that need to map to the User class</param>
    /// <returns>Return an object to saved in the database</returns>
    /// <exception cref="Exception"></exception>
    private static object ToUserType<TValue>(UserInputDTO input) where TValue : UserDTOBase => 
    typeof(TValue) switch
    {
        Type t when t == typeof(ClientDto) => input.ToClient(),
        Type t when t == typeof(MerchantDto) => input.ToMerchant(),
        _ => throw new Exception("Undefined user type.")
    };
    
    /// <summary>
    /// Get the user in the database.
    /// </summary>
    /// <typeparam name="TResult">The DTO that needed to be cast</typeparam>
    /// <param name="username">Username or Email of the user</param>
    /// <returns>Return a new instance of the TResult</returns>
    /// <exception cref="Exception"></exception>
    private TResult? GetUser<TResult>(string username) where TResult : UserDTOBase
    {
        var model = GetUserByType<TResult>(username) ?? throw new Exception("Unable to find the user.");
        return CastToTypeDto<TResult>(model);
    }

    /// <summary>
    /// Get the User in the database based on the TResult type.
    /// </summary>
    /// <typeparam name="TResult">The DTO class of the User being query</typeparam>
    /// <param name="username">The email or username of the user</param>
    /// <returns>Return an object if the query is successful, else null.</returns>
    /// <exception cref="Exception"></exception>
    private object? GetUserByType<TResult>(string username) where TResult : UserDTOBase =>
    typeof(TResult) switch
    {
        Type t when t == typeof(ClientDto) => _dbContext.FindClientByUserNameNoTracking(username),
        Type t when t == typeof(MerchantDto) => _dbContext.FindMerchantByUserNameNoTracking(username),
        _ => throw new Exception("Undefined user type.")
    };

    /// <summary>
    /// Cast the object to the a UserType.
    /// </summary>
    /// <typeparam name="TResult">The target class</typeparam>
    /// <param name="value">The object that needed to be cast</param>
    /// <returns>Return a instance of TResult.</returns>
    /// <exception cref="Exception"></exception>
    private static TResult? CastToTypeDto<TResult>(object value) where TResult : UserDTOBase =>
    typeof(TResult) switch
    {
        Type t when t == typeof(ClientDto) => (value as Client)?.ToUserDto<Client, ClientDto>() as TResult,
        Type t when t == typeof(MerchantDto) => (value as Merchant)?.ToUserDto<Merchant, MerchantDto>() as TResult,
        _ => throw new Exception("Undefined user type.")
    };
}
