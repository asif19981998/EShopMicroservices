using Mapster;

namespace Basket.Api.Basket.DeleteBasket;

//public record DeleteBasketRequest(string UserName)
public record DeleteBasketReponse(bool IsSuccess);
public class DeleteBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{UserName}", async (string userName, ISender sender) =>
        {
          
            var result = await sender.Send(new DeleteBasketCommand(userName));

            var response = result.Adapt<DeleteBasketReponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketReponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete a shopping cart")
        .WithDescription("Delete a shopping cart for a user");
    }
}
