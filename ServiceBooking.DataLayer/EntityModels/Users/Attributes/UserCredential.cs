
namespace ServiceBooking.DataLayer.EntityModels;

public class UserCredential
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
