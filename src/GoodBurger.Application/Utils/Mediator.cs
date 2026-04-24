using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Abstractions.Cardapios;
using GoodBurger.Application.Abstractions.Pedidos;
using GoodBurger.Application.Cardapio.Queries.GetAllItems;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Pedidos.Commands.CreatePedido;
using GoodBurger.Application.Pedidos.Commands.DeletePedido;
using GoodBurger.Application.Pedidos.Commands.UpdatePedido;
using GoodBurger.Application.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Application.Pedidos.Queries.GetPedidoById;
using GoodBurger.Domain.Models;

namespace GoodBurger.Application.Utils;

public class Mediator : IMediator
{
    private readonly ICreatePedidoHandler _create;
    private readonly IGetPedidoByIdHandler _get;
    private readonly IGetAllPedidosHandler _getAllHandler;
    private readonly IUpdatePedidoHandler _updateHandler;
    private readonly IDeletePedidoHandler _deleteHandler;
    private readonly IGetAllItemsHandler _handler;


    public Mediator(
        ICreatePedidoHandler createPedidoHandler,
        IGetPedidoByIdHandler getPedidoByIdHandler,
        IGetAllPedidosHandler getAllPedidosHandler,
        IUpdatePedidoHandler updatePedidoHandler,
        IDeletePedidoHandler deletePedidoHandler,
        IGetAllItemsHandler getAllItemsHandler)
    {
        _create = createPedidoHandler;
        _get = getPedidoByIdHandler;
        _getAllHandler = getAllPedidosHandler;
        _updateHandler = updatePedidoHandler;
        _deleteHandler = deletePedidoHandler;
        _handler = getAllItemsHandler;
    }
    public Mediator()
    {
        
    }

    public Task<PedidoResponse> HandleCreatePedido(CreatePedidoCommand command)
    {
        return _create.HandleCreatePedido(command);
    }

    public Task HandleDeletePedido(DeletePedidoCommand command)
    {
        return _deleteHandler.HandleDeletePedido(command);
    }

    public Task<PedidoResponse> HandleUpdatePedido(UpdatePedidoCommand command)
    {
        return _updateHandler.HandleUpdatePedido(command);
    }

    public Task<IEnumerable<Pedido>> HandleGetAllPedidos(GetAllPedidosQuery query)
    {
        return _getAllHandler.HandleGetAllPedidos(query);
    }

    public Task<Pedido?> HandleGetPedidoById(GetPedidoByIdQuery query)
    {
        return _get.HandleGetPedidoById(query);
    }

    public Task<IEnumerable<Item>> HandleGetAllItems(GetAllItemsQuery query)
    {
        return _handler.HandleGetAllItems(query);
    }
}