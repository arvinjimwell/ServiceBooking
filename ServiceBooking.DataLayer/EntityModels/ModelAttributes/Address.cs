
namespace ServiceBooking.DataLayer.EntityModels;

public class Address
{
    public int Id { get; set; }
    public string? StreetNumber { get; set; }
    public string? StreetName { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
}
