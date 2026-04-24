using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Cardapio.Queries.GetAllItems;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Pedidos.Commands.CreatePedido;
using GoodBurger.Application.Pedidos.Commands.DeletePedido;
using GoodBurger.Application.Pedidos.Commands.UpdatePedido;
using GoodBurger.Application.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Application.Pedidos.Queries.GetPedidoById;
using GoodBurger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodBurger.Application.Utils;

public class Mediator : IMediator
{
    private readonly CreatePedidoHandler _create;
    private readonly GetPedidoByIdHandler _get;
    private readonly GetAllPedidosHandler _getAllHandler;
    private readonly UpdatePedidoHandler _updateHandler;
    private readonly DeletePedidoHandler _deleteHandler;
    private readonly GetAllItemsHandler _handler;


    public Mediator(
        CreatePedidoHandler createPedidoHandler,
        GetPedidoByIdHandler getPedidoByIdHandler,
        GetAllPedidosHandler getAllPedidosHandler,
        UpdatePedidoHandler updatePedidoHandler,
        DeletePedidoHandler deletePedidoHandler,
        GetAllItemsHandler getAllItemsHandler)
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
//    //public Task<IResponse?> Handle(object? commandOrQuery,TypeCQRS type)
//    {
        
//        switch (type)
//        {
//            case TypeCQRS.CreatePedidoHandler:
//                return _create.HandleCreatePedido((CreatePedidoCommand)commandOrQuery!);
//            case TypeCQRS.GetPedidoByIdHandler:
//                return _get.HandleGetPedidoById((GetPedidoByIdQuery)commandOrQuery!);
//            case TypeCQRS.GetAllPedidosHandler:
//                return _getAllHandler.HandleGetAllPedidos((GetAllPedidosQuery)commandOrQuery!);
//            case TypeCQRS.UpdatePedidoHandler:
//                return _updateHandler.HandleUpdatePedido((UpdatePedidoCommand)commandOrQuery!);
//            case TypeCQRS.DeletePedidoHandler:
//                return _deleteHandler.HandleDeletePedido((DeletePedidoCommand)commandOrQuery!);
//            case TypeCQRS.GetAllItemsHandler:
//                return _handler.HandleGetAllItems((GetAllItemsQuery)commandOrQuery!);
//            default:
//                throw new InvalidOperationException("Tipo de comando ou consulta desconhecido.");
//    }
//}

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