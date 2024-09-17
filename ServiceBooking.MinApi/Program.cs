using ServiceBooking.MinApi.Endpoints;
using ServiceBooking.DataLayer;
using ServiceBooking.ServiceLayer.UserService;
using ServiceBooking.ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceBookingContext(null!);
builder.Services.AddScoped<IMerchantService, MerchantService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapMerchantPost();

app.Run();

