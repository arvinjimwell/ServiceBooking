using Microsoft.EntityFrameworkCore;
using ServiceBooking.DataLayer;
using ServiceBooking.DataLayer.EntityModels;

namespace ServiceBooking.ServiceLayer;

public static partial class Tools
{
    public static Merchant? FindMerchantByUserNameNoTracking(this ServiceBookingContext context, string email)
    {
        return context.Merchants.Include(m => m.ContactInformation)
            .Include(m => m.Businesses)
            .Include(m => m.Credential)
            .AsNoTracking()
            .FirstOrDefault(m => m.Credential.UserName.Equals(email));
    }
}
