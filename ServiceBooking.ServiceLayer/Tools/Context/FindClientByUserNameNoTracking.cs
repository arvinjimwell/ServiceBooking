
using Microsoft.EntityFrameworkCore;
using ServiceBooking.DataLayer;
using ServiceBooking.DataLayer.EntityModels;

namespace ServiceBooking.ServiceLayer;

public static partial class Tools
{
    public static Client? FindClientByUserNameNoTracking(this ServiceBookingContext context, string username)
    {
        return context.Clients.Include(c => c.ContactInformation)
            .Include(c => c.Credential)
            .Include(c => c.Bookings)
            .FirstOrDefault(c => c.Credential.UserName.Equals(username));
    }
}
