namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);
public class DeleteProductEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id}", async (ISender sender, Guid id) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));

            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
        .Produces<DeleteProductResult>(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Product")
        .WithDescription("Delete a product by id");
    }
}

