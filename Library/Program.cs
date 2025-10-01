using System.Reflection;
using Library;
using Library.Interfaces;
using Library.Models;
using Library.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<LibDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
builder.Services.AddControllers();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBookService, BookService>();
// builder.Services.AddScoped<>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "API with EF Core Integration"
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

//await DebugSeeding.RunSeedingWithDebugAsync(app.Services);

// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<LibDbContext>();
//     
//     await db.Database.EnsureCreatedAsync();
//
//     var seedPath = Path.Combine(AppContext.BaseDirectory, "JsonSeeder");
//
//     // ✅ run all seeders
//     await new CategorySeeder(Path.Combine(seedPath, "categories.json")).SeedAsync(db);
//     await new AuthorSeeder(Path.Combine(seedPath, "authors.json")).SeedAsync(db);
//     //await new PublisherSeeder(Path.Combine(seedPath, "publishers.json")).SeedAsync(db);
//     await new BookSeeder(Path.Combine(seedPath, "books.json")).SeedAsync(db);
//     await new UserSeeder(Path.Combine(seedPath, "users.json")).SeedAsync(db);
//     //await new ReviewSeeder(Path.Combine(seedPath, "reviews.json")).SeedAsync(db);
// }

using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

try
{
    LibDbContext context = serviceScope.ServiceProvider.GetRequiredService<LibDbContext>();
    DataInitializer.SeedData(context).Wait();
}
catch (Exception ex)
{
    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB.");
}

app.Run();
