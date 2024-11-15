using FirstMicroservice.Todos.WebApi.Context;
using FirstMicroservice.Todos.WebApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("TodosDb");
});

var app = builder.Build();

app.MapGet("/todos/create", (string work, ApplicationDbContext context) =>
{
    Todo todo = new Todo { Work = work };
    context.Add(todo);
    context.SaveChanges();

    return new { Message = "Todo create is successfully." };
});

app.MapGet("/todos/getall", (ApplicationDbContext context) =>
{
    var todos = context.Todos.ToList();
    return todos;
});

app.Run();