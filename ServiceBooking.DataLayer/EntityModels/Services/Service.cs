

namespace ServiceBooking.DataLayer.EntityModels;

public class Service
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ICollection<BookServices> Bookings { get; set; } = [];
    public Guid BusinessId { get; set; }
    public Business Business { get; set; } = null!;
}
