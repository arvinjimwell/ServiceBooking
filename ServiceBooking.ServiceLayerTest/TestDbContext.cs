using Bogus;
using Microsoft.EntityFrameworkCore;
using ServiceBooking.DataLayer;
using ServiceBooking.DataLayer.EntityModels;

namespace ServiceBooking.ServiceLayerTest;

internal static class TestDbContext
{
    public static ServiceBookingContext CreateContext(string name)
    {
        var options = new DbContextOptionsBuilder<ServiceBookingContext>()
            .UseInMemoryDatabase(name)
            .Options;

        return new ServiceBookingContext(options);
    }

    public static void SeedDatabase(this ServiceBookingContext dbContext)
    {
        var fakeMerchant = FakeMerchant();
        List<Merchant> merchants = fakeMerchant.Generate(10);
        dbContext.AddRange(merchants);
        dbContext.SaveChanges();
    }

    private static Faker<Merchant> FakeMerchant()
    {
        var fakeBusiness = FakeBusiness();
        return new Faker<Merchant>()
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
            .RuleFor(u => u.Credential, (f, u) => FakeCredential(u).Generate())
            .RuleFor(u => u.ContactInformation, (f, u) => FakeContactInformation(u.Credential.UserName).Generate())
            .RuleFor(u => u.Businesses, (f, u) => fakeBusiness.Generate(Random.Shared.Next(1, 4)));
    }

    private static Faker<UserCredential> FakeCredential<T>(T user) where T : UserBase
    {
        return new Faker<UserCredential>()
            .RuleFor(u => u.Password, (f, u) => "password")
            .RuleFor(u => u.UserName, (f, u) => f.Internet.Email(user.FirstName + user.LastName));
    }

    private static Faker<Business> FakeBusiness()
    {
        var fakeService = FakeService();
        var fakeContactInformation = FakeContactInformation();
        return new Faker<Business>()
            .RuleFor(u => u.Name, (f, u) => f.Company.CompanyName())
            .RuleFor(u => u.ContactInformation, (f, u) => fakeContactInformation.Generate())
            .RuleFor(u => u.Services, (f, u) => fakeService.Generate(Random.Shared.Next(1, 4)));
    }

    private static Faker<Service> FakeService()
    {
        return new Faker<Service>()
            .RuleFor(u => u.Name, (f, u) => f.Commerce.Product())
            .RuleFor(u => u.Price, (f, u) => Decimal.Parse(f.Commerce.Price()))
            .RuleFor(u => u.Description, (f, u) => f.Lorem.Paragraph());
    }

    private static Faker<ContactInformation> FakeContactInformation(string email = "")
    {
        var fakeAddress = FakeAddress();

        return new Faker<ContactInformation>()
            .RuleFor(u => u.Email, (f, u) => string.IsNullOrWhiteSpace(email) ? f.Internet.Email() : email)
            .RuleFor(u => u.PhoneNumber, (f, u) => [f.Phone.PhoneNumber()])
            .RuleFor(u => u.Address, (f, u) => fakeAddress.Generate());
    }

    private static Faker<Address> FakeAddress()
    {
        return new Faker<Address>()
            .RuleFor(u => u.StreetNumber, (f, u) => f.Address.StreetAddress())
            .RuleFor(u => u.StreetName, (f, u) => f.Address.StreetName())
            .RuleFor(u => u.State, (f, u) => f.Address.State())
            .RuleFor(u => u.City, (f, u) => f.Address.City())
            .RuleFor(u => u.PostalCode, (f, u) => f.Address.ZipCode());
    }
}
