using FluentValidation;
using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Abstractions.Cardapios;
using GoodBurger.Application.Abstractions.Pedidos;
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
        services.AddScoped<ICreatePedidoHandler, CreatePedidoHandler>();
        services.AddScoped<IGetPedidoByIdHandler, GetPedidoByIdHandler>();
        services.AddScoped<IGetAllItemsHandler, GetAllItemsHandler>();
        services.AddScoped<IGetAllPedidosHandler, GetAllPedidosHandler>();
        services.AddScoped<IUpdatePedidoHandler, UpdatePedidoHandler>();
        services.AddScoped<IDeletePedidoHandler, DeletePedidoHandler>();
        services.AddScoped<IMediator, Mediator>();
        services.AddValidatorsFromAssemblyContaining<CreatePedidoValidator>();
        return services;
    }
}
