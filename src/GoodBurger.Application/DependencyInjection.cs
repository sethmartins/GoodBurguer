

using FluentValidation;
using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Cardapio.Queries.GetAllItems;
using GoodBurger.Application.Pedidos.Commands.CreatePedido;
using GoodBurger.Application.Pedidos.Commands.DeletePedido;
using GoodBurger.Application.Pedidos.Commands.UpdatePedido;
using GoodBurger.Application.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Application.Pedidos.Queries.GetPedidoById;
using GoodBurger.Application.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace GoodBurger.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreatePedidoHandler>();
        services.AddScoped<GetPedidoByIdHandler>();
        services.AddScoped<GetAllItemsHandler>();
        services.AddScoped<GetAllPedidosHandler>();
        services.AddScoped<UpdatePedidoHandler>();
        services.AddScoped<DeletePedidoHandler>();
        services.AddScoped<IMediator, Mediator>();
        services.AddValidatorsFromAssemblyContaining<CreatePedidoValidator>();
        return services;
    }
}
