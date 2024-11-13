using FirstMicroservice.Categories.WebApi.Context;
using FirstMicroservice.Categories.WebApi.Dto;
using FirstMicroservice.Categories.WebApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

var app = builder.Build();

app.MapGet("/categories/getall", async (ApplicationDbContext context, CancellationToken cancellationToken) =>
{
    var categories = await context.Categories.ToListAsync(cancellationToken);
    return categories;
});

app.MapPost("/categories/create", async (CreateCategoryDto request,  ApplicationDbContext context, CancellationToken cancellationToken) =>
{
    bool isNameExist = await context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken);
    if(isNameExist)
    {
        return Results.BadRequest(new { Message = "Category name already exists." });
    }
    Category category = new()
    {
        Name = request.Name,
    };
    await context.Categories.AddAsync(category, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Results.Ok(new { Message = "Category create is successfully." });
});

// autometically create database and apply migrations
using (var scoped = app.Services.CreateScope())
{
    var srv = scoped.ServiceProvider;
    var context = srv.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
}

app.Run();
