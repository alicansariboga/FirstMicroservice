using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// OCELOT
builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();

app.Run();
