using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Features.Cardapio.Queries.GetAllItems;
using GoodBurger.Application.Features.Pedidos.Commands.CreatePedido;
using GoodBurger.Application.Features.Pedidos.Commands.DeletePedido;
using GoodBurger.Application.Features.Pedidos.Commands.UpdatePedido;
using GoodBurger.Application.Features.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Application.Features.Pedidos.Queries.GetPedidoById;
using GoodBurger.Domain.Abstractions;


namespace GoodBurger.Application.Utils;

public class Mediator : IMediator
{
    private readonly IHandler<CreatePedidoCommand, Result<PedidoResponse>> _create;
    private readonly IHandler<GetPedidoByIdQuery, Result<PedidoResponse?>> _getPedidoByIdHandler;
    private readonly IHandler<GetAllPedidosQuery, Result<IEnumerable<PedidoResponse>>> _getAllHandler;
    private readonly IHandler<UpdatePedidoCommand, Result<PedidoResponse>> _updateHandler;
    private readonly IHandler<DeletePedidoCommand, Result<int>> _deleteHandler;
    private readonly IHandler<GetAllItemsQuery, Result<IEnumerable<ItemResponse>>> _handler;


    public Mediator(
        IHandler<CreatePedidoCommand, Result<PedidoResponse>> createPedidoHandler,
        IHandler<GetPedidoByIdQuery, Result<PedidoResponse?>> getPedidoByIdHandler,
        IHandler<GetAllPedidosQuery, Result<IEnumerable<PedidoResponse>>> getAllPedidosHandler,
        IHandler<UpdatePedidoCommand, Result<PedidoResponse>> updatePedidoHandler,
        IHandler<DeletePedidoCommand, Result<int>> deletePedidoHandler,
        IHandler<GetAllItemsQuery, Result<IEnumerable<ItemResponse>>> getAllItemsHandler)
    {
        _create = createPedidoHandler;
        _getPedidoByIdHandler = getPedidoByIdHandler;
        _getAllHandler = getAllPedidosHandler;
        _updateHandler = updatePedidoHandler;
        _deleteHandler = deletePedidoHandler;
        _handler = getAllItemsHandler;
    }
    public Mediator()
    {
        
    }

    public async Task<PedidoResponse> HandleCreatePedido(CreatePedidoCommand command, CancellationToken cancellationToken)
    {
        var result = await _create.HandleAsync(command, cancellationToken);
        return result.Value;
    }

    public async Task<int> HandleDeletePedido(DeletePedidoCommand command, CancellationToken cancellationToken)
    {
        var result = await _deleteHandler.HandleAsync(command, cancellationToken);
        return result.Value;
    }

    public async Task<PedidoResponse> HandleUpdatePedido(UpdatePedidoCommand command, CancellationToken cancellationToken)
    {
        var result = await _updateHandler.HandleAsync(command, cancellationToken);
        return result.Value;
    }

    public async Task<Result<IEnumerable<PedidoResponse>>> HandleGetAllPedidos(GetAllPedidosQuery query, CancellationToken cancellationToken)
    {
        return await _getAllHandler.HandleAsync(query, cancellationToken);
    }

    public async Task<Result<PedidoResponse?>> HandleGetPedidoById(GetPedidoByIdQuery query, CancellationToken cancellationToken)
    {
        return await _getPedidoByIdHandler.HandleAsync(query, cancellationToken);
    }

    public async Task<Result<IEnumerable<ItemResponse>>> HandleGetAllItems(GetAllItemsQuery query, CancellationToken cancellationToken)
    {
        return await _handler.HandleAsync(query, cancellationToken);
    }
}