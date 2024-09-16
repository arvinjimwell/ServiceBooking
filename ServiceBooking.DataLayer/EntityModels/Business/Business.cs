
namespace ServiceBooking.DataLayer.EntityModels;

public class Business
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ContactInformation ContactInformation { get; set; } = null!;
    public ICollection<Service> Services { get; set; } = [];
    public Guid MerchantId { get; set; }
    public Merchant Merchant { get; set; } = null!;
}
