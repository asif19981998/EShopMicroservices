namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdResponse(Product Product);
public class GetProductByIdEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = new GetProductByIdResponse(result.Product);
            return Results.Ok(response);
        })
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Product by Id")
        .WithDescription("Get a product by its id");
    }
}

