using GoodBurger.Application.Abstractions;
using GoodBurger.Infrastructure.Context;
using GoodBurger.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoodBurger.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseInMemoryDatabase("GoodHamburgerDb"));
        
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        return services;
    }
}
