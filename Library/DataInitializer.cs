using Newtonsoft.Json;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Models;

public class DataInitializer
{
    public static async Task SeedFromJsonAsync<T>(
        LibDbContext context,
        DbSet<T> dbSet,
        string projectRoot,
        string filename
    ) where T : class
    {
        if (await dbSet.AnyAsync()) return;
        
        var filePath = Path.Combine(projectRoot, "JsonSeeders", filename);
        filePath = Path.GetFullPath(filePath);
        
        if(!File.Exists(filePath))
            throw new FileNotFoundException($"Seed file not found: {filePath}");
        
        var json = await File.ReadAllTextAsync(filePath);
        var entities = JsonConvert.DeserializeObject<List<T>>(json);
        
        if(entities == null ||entities.Count == 0)
            throw new InvalidDataException($"No valid data found in seed file: {filePath}");
        
        await dbSet.AddRangeAsync(entities);
    }
    public static async Task SeedData(LibDbContext context)
    {
        
        // Get the output directory (e.g., bin/Debug/net8.0)
        var outputDirectory = AppContext.BaseDirectory;

        // Go up two levels to reach the project root (from bin/Debug/net8.0 -> project root)
        var projectRoot = Path.Combine(outputDirectory, "..", "..", "..");

        // await DataInitializer.SeedFromJsonAsync(context, context.Categories, projectRoot, "categories.json");
        // await DataInitializer.SeedFromJsonAsync(context, context.Books, projectRoot, "books.json");
        // await DataInitializer.SeedFromJsonAsync(context, context.Authors, projectRoot, "authors.json");
        // await DataInitializer.SeedFromJsonAsync(context, context.BookAuthors, projectRoot, "bookAuthors.json");
        // await DataInitializer.SeedFromJsonAsync(context, context.Inventories, projectRoot, "inventories.json");
        // await DataInitializer.SeedFromJsonAsync(context, context.Users, projectRoot, "users.json");
        // await DataInitializer.SeedFromJsonAsync(context, context.Loans, projectRoot, "loans.json");
        await DataInitializer.SeedFromJsonAsync(context, context.Reviews, projectRoot, "reviews.json");
        
        await context.SaveChangesAsync();
    }
}