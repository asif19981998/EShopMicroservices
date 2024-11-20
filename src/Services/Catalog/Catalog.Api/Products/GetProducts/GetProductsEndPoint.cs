
namespace Catalog.Api.Products.GetProducts;

public record GetProductsReponse(IEnumerable<Product> products);
public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("products",async (ISender sender) =>
       {
          
                var result = await sender.Send(new GetProductsQuery());
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

