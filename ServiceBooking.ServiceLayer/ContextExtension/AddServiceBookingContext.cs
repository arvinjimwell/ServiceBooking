using ServiceBooking.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;

namespace ServiceBooking.ServiceLayer;

public static class ServiceBookingServiceExtension
{
    public static IServiceCollection AddServiceBookingContext(this IServiceCollection services, string connectionString)
    {
        #if DEBUG
        if(string.IsNullOrWhiteSpace(connectionString))
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "StudentJimwell",
                InitialCatalog = "ServiceBooking",
                Encrypt = true,
                IntegratedSecurity = true,
                TrustServerCertificate = true,
                MultipleActiveResultSets = true,
                ConnectTimeout = 30
            };

            connectionString = builder.ConnectionString;
        }
        #endif

        services.AddDbContext<ServiceBookingContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

        return services;
    }
}
