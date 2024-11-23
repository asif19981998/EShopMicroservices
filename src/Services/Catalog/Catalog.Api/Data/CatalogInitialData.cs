using Marten.Schema;

namespace Catalog.Api.Data;
public class CatalogInitialData : IInitialData
{ 
	public async Task Populate(IDocumentStore store, CancellationToken cancellation)
	{
		using var session = store.LightweightSession();	

		if (await session.Query<Product>().AnyAsync())
		{
			return;
		}

		session.Store<Product>(GetPreconfiguredProducts());
		await session.SaveChangesAsync();
	}

	private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
	{
		
		new Product()
		{
			Id = Guid.NewGuid(),
			Name = "Product 1",
			Description = "Description",
			ImageFile = "product-1.png",
			Price = 100,
			Category = new List<string> { "Category 1", "Category 2" }
		},
		new Product()
		{
			Id = Guid.NewGuid(),
			Name = "Product 2",
			Description = "Description",
			ImageFile = "product-2.png",
			Price = 1000,
			Category = new List<string> { "Category 1", "Category 2" }
		},
		new Product()
		{
			Id = Guid.NewGuid(),
			Name = "Product 3",
			Description = "Description",
			ImageFile = "product-3.png",
			Price = 1000,
			Category = new List<string> { "Category 1", "Category 2" }
		}
		
	};
}
