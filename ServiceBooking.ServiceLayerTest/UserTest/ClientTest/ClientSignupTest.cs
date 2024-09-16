using ServiceBooking.DataLayer;
using ServiceBooking.DTO.UserDto;
using ServiceBooking.ServiceLayer.UserService;
using static ServiceBooking.ServiceLayerTest.TestDbContext;


namespace ServiceBooking.ServiceLayerTest.UserTest.ClientTest;

public class ClientSignupTest
{
    private readonly ServiceBookingContext _context;
    private readonly IClientService _clientService;

    public ClientSignupTest()
    {
        _context = CreateContext("ClientSignUp");
        _clientService = new ClientService(_context);
    }

    [Fact]
    public void AbleToSignUp()
    {
        // Arrange
        UserInputDTO input = new()
        {
            Email = "testclient@email.com",
            FirstName = "John",
            LastName = "Doe",
            ContactNumber = ["+123-1234-123"],
            Password = "testpassword",
        };
        int expClientNum = _context.Clients.Count() + 1;
        int expMercNum = _context.Merchants.Count();

        // Act
        var result = _clientService.SignUp(input, out ClientDto? client, out string errors);
        int actClientNum = _context.Clients.Count();
        int actMercNum = _context.Merchants.Count();

        // Assert
        Assert.True(result);
        Assert.NotNull(client);
        Assert.Null(errors);
        Assert.Equal(expClientNum, actClientNum);
        Assert.Equal(expMercNum, actMercNum);
        Assert.True(client.GetType() == typeof(ClientDto));
        Assert.Equal("Client", client.Role);
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

        int expClientNum = _context.Clients.Count() + 1;
        int expMercNum = _context.Merchants.Count();

        // Act
        var result = _clientService.SignUp(input, out ClientDto? client, out string errors);
        int actClientNum = _context.Clients.Count();
        int actMercNum = _context.Merchants.Count();

        // Assert
        Assert.True(result);
        Assert.NotNull(client);
        Assert.Null(errors);
        Assert.Equal(expClientNum, actClientNum);
        Assert.Equal(expMercNum, actMercNum);
        Assert.True(client.GetType() == typeof(ClientDto));
        Assert.Equal("Client", client.Role);
        Assert.Equal(input.Email, client.Email);
        Assert.Equal(input.FirstName, client.FirstName);
        Assert.Equal(input.LastName, client.LastName);
        Assert.Equal(input.MiddleName, client.MiddleName);
        foreach(string cn in input.ContactNumber)
        {
            Assert.Contains(cn, client.ContactNumber);
        }
    }

    [Fact]
    public void IfEmailIsAlreadyRegistered_ShouldNotSignUp()
    {
        // Arrange
        UserInputDTO input = new()
        {
            Email = "already@email.com",
            FirstName = "John",
            LastName = "Doe",
            ContactNumber = ["+123-1234-123", "321-123-4123", "09123456789"],
            Password = "testpassword",
        };
        int expMerchNum = _context.Merchants.Count();
        int expClientNum = _context.Clients.Count() + 1;
        string expError = "Email is already registered.";

        // Act
        _ = _clientService.SignUp(input, out ClientDto? _, out string _);
        var result = _clientService.SignUp(input, out ClientDto? client, out string errors);
        int actMercNum = _context.Merchants.Count();
        int actClientNum = _context.Clients.Count();

        // Assert
        Assert.Equal(expClientNum, actClientNum);
        Assert.Equal(expMerchNum, actMercNum);
        Assert.NotNull(errors);
        Assert.Equal(expError, errors);
        Assert.Null(client);
        Assert.False(result);
    }
}
