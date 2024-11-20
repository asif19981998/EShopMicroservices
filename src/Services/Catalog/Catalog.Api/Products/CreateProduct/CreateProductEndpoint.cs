namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                if (sender == null)
                {
                    return Results.Problem("Sender is null. Dependency injection might be misconfigured.");
                }

                var command = request.Adapt<CreateProductCommand>();

                try
                {
                    var result = await sender.Send(command);
                    var response = result.Adapt<CreateProductResponse>();
                    return Results.Created($"/products/{response.Id}", response);
                }
                catch (Exception ex)
                {
                    // Log the exception (you can use a logging framework here)
                    return Results.Problem($"An error occurred while processing the request: {ex.Message}");
                }
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Prduct")
            .WithDescription("Create a new product");
           
        }
    }
}
