
using Microsoft.EntityFrameworkCore;
using ServiceBooking.DataLayer;
using ServiceBooking.DataLayer.EntityModels;

namespace ServiceBooking.ServiceLayer;

public partial class Tools
{
    /// <summary>
    /// Get user from the database.
    /// </summary>
    /// <typeparam name="T">Type of user</typeparam>
    /// <param name="dbContext"></param>
    /// <param name="userName">Target username or email</param>
    /// <returns>Returns new instance of T if found in the database, else null</returns>
    public static T? GetUserNoTracking<T>(this ServiceBookingContext dbContext, string userName) where T : UserBase =>
        typeof(T) switch
        {
            Type t when t == typeof(Merchant) => dbContext.GetMerchantNoTracking(userName) as T,
            Type t when t == typeof(Client) => dbContext.GetClientNoTracking(userName) as T,
            _ => null
        };

    private static Merchant? GetMerchantNoTracking(this ServiceBookingContext context, string email) =>
        context.Merchants.Include(m => m.ContactInformation)
            .Include(m => m.Businesses)
            .Include(m => m.Credential)
            .AsNoTracking()
            .FirstOrDefault(m => m.Credential.UserName.Equals(email));

    private static Client? GetClientNoTracking(this ServiceBookingContext context, string username) =>
        context.Clients.Include(c => c.ContactInformation)
            .Include(c => c.Credential)
            .Include(c => c.Bookings)
            .FirstOrDefault(c => c.Credential.UserName.Equals(username));
}
