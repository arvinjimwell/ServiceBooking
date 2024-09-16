using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ServiceBooking.DataLayer;
using ServiceBooking.DTO.UserDto;
using ServiceBooking.ServiceLayer.Users;
using static ServiceBooking.ServiceLayerTest.TestDbContext;

namespace ServiceBooking.ServiceLayerTest;

public class MerchantSignUpTest
{
    private readonly ServiceBookingContext _context;
    private readonly IUserService _userService;

    public MerchantSignUpTest()
    {
        _context = CreateContext("MerchantSignUp");
        _userService = new UserService(_context);
    }

    [Fact]
    public void AbleToSignUp()
    {
        // Arrange
        UserInputDTO input = new()
        {
            Email = "test@email.com",
            FirstName = "John",
            LastName = "Doe",
            ContactNumber = ["+123-1234-123"],
            Password = "testpassword",
        };
        int expMerchantNumber = _context.Merchants.Count() + 1;
        int expClientNumber = _context.Clients.Count();

        // Act
        var result = _userService.MerchantSignUp(input, out MerchantDto? merchant, out string errors);
        int actMerchantNumber = _context.Merchants.Count();
        int actClientNumber = _context.Clients.Count();

        // Assert
        Assert.True(result);
        Assert.NotNull(merchant);
        Assert.Null(errors);
        Assert.Equal(expMerchantNumber, actMerchantNumber);
        Assert.Equal(expClientNumber, actClientNumber);
        Assert.True(merchant.GetType() == typeof(MerchantDto));
        Assert.True(merchant.Role == "Merchant");
    }

    [Fact]
    public void AccuracyTest()
    {
        // Arrange
        UserInputDTO input = new()
        {
            Email = "accuracytest@email.com",
            FirstName = "John",
            LastName = "Doe",
            ContactNumber = ["+123-1234-123", "321-123-4123", "09123456789"],
            Password = "testpassword",
        };

        int expMerchantNumber = _context.Merchants.Count() + 1;
        int expClientNumber = _context.Clients.Count();

        // Act
        var result = _userService.MerchantSignUp(input, out MerchantDto? merchant, out string errors);
        int actMerchantNumber = _context.Merchants.Count();
        int actClientNumber = _context.Clients.Count();

        // Assert
        Assert.True(result);
        Assert.NotNull(merchant);
        Assert.Null(errors);
        Assert.Equal(expMerchantNumber, actMerchantNumber);
        Assert.Equal(expClientNumber, actClientNumber);
        Assert.True(merchant.GetType() == typeof(MerchantDto));
        Assert.True(merchant.Role == "Merchant");
        Assert.Equal(input.Email, merchant.Email);
        Assert.Equal(input.FirstName, merchant.FirstName);
        Assert.Equal(input.LastName, merchant.LastName);
        Assert.Equal(input.MiddleName, merchant.MiddleName);
        foreach(string cn in input.ContactNumber)
        {
            Assert.Contains(cn, merchant.ContactNumber);
        }
    }

    /// <summary>
    ///     Will fail if this test run on its own since the database is empty.
    /// </summary>
    [Fact]
    public void IfEmailIsAlreadyRegistered_ShouldNotSignUp()
    {
        // Arrange
        UserInputDTO input = new()
        {
            Email = "test@email.com",
            FirstName = "John",
            LastName = "Doe",
            ContactNumber = ["+123-1234-123", "321-123-4123", "09123456789"],
            Password = "testpassword",
        };
        int expMerchantNumber = _context.Merchants.Count();
        int expClientNumber = _context.Clients.Count();
        string expError = "Email is already registered.";

        // Act
        var result = _userService.MerchantSignUp(input, out MerchantDto? merchant, out string errors);
        int actMerchantNumber = _context.Merchants.Count();
        int actClientNumber = _context.Clients.Count();

        // Assert
        Assert.Equal(expClientNumber, actClientNumber);
        Assert.Equal(expMerchantNumber, actMerchantNumber);
        Assert.NotNull(errors);
        Assert.Equal(expError, errors);
        Assert.Null(merchant);
        Assert.False(result);
    }
}