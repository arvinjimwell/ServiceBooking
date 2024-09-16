

namespace ServiceBooking.DataLayer.EntityModels;

public class ContactInformation
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public string[] PhoneNumber { get; set; } = [];
    public Address Address { get; set; } = null!;
}
