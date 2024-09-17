
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ServiceBooking.DataLayer.EntityModels;

namespace ServiceBooking.DataLayer;

public class ServiceBookingContext : DbContext
{
    public ServiceBookingContext() { }
    public ServiceBookingContext(DbContextOptions<ServiceBookingContext> options) : base(options) { }

    public DbSet<UserBase> Users { get; set; }
    public DbSet<Merchant> Merchants { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<BookServices> BookServices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            SqlConnectionStringBuilder builder = new() {
                DataSource = "StudentJimwell",
                InitialCatalog = "ServiceBooking",
                IntegratedSecurity = true,
                MultipleActiveResultSets = true,
                TrustServerCertificate = true,
                ConnectTimeout = 30
            };
            optionsBuilder.UseSqlServer(builder.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserBase>(model =>
        {
            model.HasKey(m => m.Id);
        });

        modelBuilder.Entity<Merchant>(model =>
        {
            model.HasMany(m => m.Businesses)
                .WithOne(b => b.Merchant)
                .HasForeignKey(b => b.MerchantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Business>(model =>
        {
            model.HasKey(m => m.Id);

            model.HasMany(m => m.Services)
                .WithOne(s => s.Business)
                .HasForeignKey(s => s.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Service>(model =>
        {
            model.Property(m => m.Price)
                .HasPrecision(18, 2);
        });

        modelBuilder.Entity<BookServices>(model =>
        {
            model.HasKey(m => m.Id);

            model.HasOne(m => m.Service)
                .WithMany(s => s.Bookings)
                .OnDelete(DeleteBehavior.NoAction);

            model.HasOne(m => m.Client)
                .WithMany(c => c.Bookings)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
