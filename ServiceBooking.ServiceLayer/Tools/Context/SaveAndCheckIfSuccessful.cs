using ServiceBooking.DataLayer;

namespace ServiceBooking.ServiceLayer;

public static partial class Tools
{
    public static bool SaveAndCheckIfSuccessful(this ServiceBookingContext context)
    {
        int changes = context.SaveChanges();
        context.ChangeTracker.Clear();
        return changes > 0;
    }
}
