using GoodBurger.Domain.Enums;
using GoodBurger.Domain.Models;
using GoodBurger.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoodBurger.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            ctx.Database.EnsureCreated();

            ctx.Items.AddRange(
                new Item("X Burger", 5, TipoItem.Sanduiche),
                new Item("Batata", 2, TipoItem.Batata),
                new Item("Refri", 2.5m, TipoItem.Refrigerante)
            );

            ctx.SaveChanges();
        });
    }
}
