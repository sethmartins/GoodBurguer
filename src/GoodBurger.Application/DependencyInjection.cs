using FluentValidation;
using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Features.Cardapio.Queries.GetAllItems;
using GoodBurger.Application.Features.Pedidos.Commands.CreatePedido;
using GoodBurger.Application.Features.Pedidos.Commands.DeletePedido;
using GoodBurger.Application.Features.Pedidos.Commands.UpdatePedido;
using GoodBurger.Application.Features.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Application.Features.Pedidos.Queries.GetPedidoById;
using GoodBurger.Application.Utils;
using GoodBurger.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GoodBurger.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IHandler<CreatePedidoCommand, Result<PedidoResponse>>, CreatePedidoHandler>();
        services.AddScoped<IHandler<GetPedidoByIdQuery, Result<PedidoResponse?>>, GetPedidoByIdHandler>();
        services.AddScoped<IHandler<GetAllItemsQuery, Result<IEnumerable<ItemResponse>>>  , GetAllItemsHandler>();
        services.AddScoped<IHandler<GetAllPedidosQuery, Result<IEnumerable<PedidoResponse>>> , GetAllPedidosHandler>();
        services.AddScoped<IHandler<UpdatePedidoCommand, Result<PedidoResponse>>, UpdatePedidoHandler>();
        services.AddScoped<IHandler<DeletePedidoCommand, Result<int>>, DeletePedidoHandler>();
        services.AddScoped<IMediator, Mediator>();

        services.AddValidatorsFromAssemblyContaining<CreatePedidoValidator>();
        return services;
    }
}
