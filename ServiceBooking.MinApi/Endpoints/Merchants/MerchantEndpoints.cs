using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceBooking.ServiceLayer.UserService;
using ServiceBooking.DTO.UserDto;

namespace ServiceBooking.MinApi.Endpoints;

public static partial class EndpointExtensions
{
    public static WebApplication MapMerchantPost(this WebApplication app)
    {
        app.MapPost("api/merchants", Results<Created<MerchantDto>, BadRequest> (
            [FromBody] UserInputDTO input,
            [FromServices] IMerchantService merchantService) =>
        {
            bool result = merchantService.SignUp(input, out MerchantDto? model, out string error);
            return result && model != null ? TypedResults.Created($"api/merchant/", model) : TypedResults.BadRequest();
        })
        .WithName("MerchantSignUp")
        .WithOpenApi()
        .Produces<MerchantDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        return app;
    }
}
