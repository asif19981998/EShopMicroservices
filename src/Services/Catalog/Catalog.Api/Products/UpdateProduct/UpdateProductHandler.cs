﻿

using System.Security;

namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name,List<string> Category,string ImageFile,string Description, decimal Price)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator:AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
            .Length(2,150).WithMessage("Name must be between 2 and 150 characters");
        
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        
    }
}

internal class UpdateProductCommandHandler
    (IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id);
        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}