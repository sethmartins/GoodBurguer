using GoodBurger.API.Middleware;
using GoodBurger.Application;
using GoodBurger.Domain.Enums;
using GoodBurger.Domain.Models;
using GoodBurger.Infrastructure;
using GoodBurger.Infrastructure.Context;
using Microsoft.OpenApi;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GoodBurger API",
        Version = "v1",
        Description = "API para gerenciamento de pedidos"
    });
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!ctx.Items.Any()) 
    {
        ctx.Items.AddRange(
            new Item("X Burger", 5, TipoItem.Sanduiche),
            new Item("X Egg", 4.5m, TipoItem.Sanduiche),
            new Item("X Bacon", 7, TipoItem.Sanduiche), 
            new Item("Batata", 2, TipoItem.Batata),
            new Item("Refrigerante", 2.5m, TipoItem.Refrigerante)
        );

        ctx.SaveChanges();
    }
}
app.UseSwagger();



app.MapScalarApiReference();
app.MapGet("/", context =>
{
    context.Response.Redirect("/scalar");
    return Task.CompletedTask;
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<CorrelationMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();

