
using Microsoft.AspNetCore.Components;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsReponse(IEnumerable<Product> products);
public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("products",async ([AsParameters] GetProductsRequest request, ISender sender) =>
       {
            var query = request.Adapt<GetProductsQuery>();

            var result = await sender.Send(query);

            var response = new GetProductsReponse(result.Products);

            return Results.Ok(response);
       })
       .WithName("GetProducts")
       .Produces<GetProductsReponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Get Products")
       .WithDescription("Get all products");
    }
}

