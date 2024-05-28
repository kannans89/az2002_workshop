using _03ProductAPI.Data;
using _03ProductAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Products API for Az2002 workshiop",
        Description = "API for managing a list of products and their stock status.",
    });
});

builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseInMemoryDatabase("Productsdb");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithTags("For weatherInfo");


app.MapGet("/products", async (ProductDbContext dbcontext) =>
{
    return  await dbcontext.Products.ToListAsync();

}).WithTags("GetProductInfo");

app.MapGet("/products/{proudctid}", async (ProductDbContext dbContext, int proudctid) =>
{
    return await dbContext.Products.FindAsync(proudctid);
})
    .WithTags("GetProductInfo");


app.MapPost("/products", async (ProductDbContext dbContext, Product product) =>
{
    dbContext.Products.Add(product);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
})
    .WithTags("AddProducts");

app.MapPut("/products/{proudctid}", async (ProductDbContext dbContext, int proudctid, Product product) =>
{
    if (proudctid != product.Id)
    {
        return Results.BadRequest("Product ID mismatch");
    }

    dbContext.Entry(product).State = EntityState.Modified;
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
})
    .WithTags("UpdateProducts");



app.MapDelete("/products/{proudctid}", async (ProductDbContext db, int proudctid) =>
{
    if (await db.Products.FindAsync(proudctid) is Product product)
    {
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return Results.Ok(product);
    }

    return Results.NotFound();
})
    .WithTags("DeleteProducts");


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}




