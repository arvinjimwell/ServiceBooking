
namespace ServiceBooking.DataLayer.EntityModels;

public class BookServices
{
    public Guid Id { get; set; }
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
