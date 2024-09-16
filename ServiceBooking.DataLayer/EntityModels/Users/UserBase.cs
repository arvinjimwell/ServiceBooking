
namespace ServiceBooking.DataLayer.EntityModels;

public abstract class UserBase
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public ContactInformation ContactInformation { get; set; } = null!;
    public UserCredential Credential { get; set; } = null!;
}
