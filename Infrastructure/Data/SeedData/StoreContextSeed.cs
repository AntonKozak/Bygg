using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data.SeedData;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Categories.Any())
        {
            var categoriesData = File.ReadAllText("../Infrastructure/Data/SeedData/Category.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
            context.Categories.AddRange(categories);
        }

        if (!context.ProductBrands.Any())
        {
            var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/ProductBrand.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            context.ProductBrands.AddRange(brands);
        }

        if (!context.ProductTypes.Any())
        {
            var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/ProductType.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            context.ProductTypes.AddRange(types);
        }

        if (!context.Products.Any())
        {
            var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/Products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            context.Products.AddRange(products);
        }

        if (context.ChangeTracker.HasChanges())
        {
            await context.SaveChangesAsync();
        }
    }
}
