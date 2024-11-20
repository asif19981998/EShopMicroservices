namespace Catalog.Api.Products.GetProductByCategory;

public record GetByCategoryResponse(IEnumerable<Product> Products);
public class GetProductByCategoryEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/category/{category}", 
            async (ISender sender, string category) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(category));
            var response = new GetByCategoryResponse(result.Products);
            return Results.Ok(response);
        })
        .Produces<GetByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products by Category")
        .WithDescription("Get all products by category");
    }
}
