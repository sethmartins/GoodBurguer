using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Features.Cardapio.Queries.GetAllItems;
using GoodBurger.Application.Features.Pedidos.Commands.CreatePedido;
using GoodBurger.Application.Features.Pedidos.Commands.DeletePedido;
using GoodBurger.Application.Features.Pedidos.Commands.UpdatePedido;
using GoodBurger.Application.Features.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Application.Features.Pedidos.Queries.GetPedidoById;
using GoodBurger.Domain.Abstractions;

namespace GoodBurger.Application.Abstractions;

public interface IMediator
{    
    public Task<PedidoResponse> HandleCreatePedido(CreatePedidoCommand command, CancellationToken cancellationToken);
    public Task<int> HandleDeletePedido(DeletePedidoCommand command, CancellationToken cancellationToken);
    public Task<PedidoResponse> HandleUpdatePedido(UpdatePedidoCommand command, CancellationToken cancellationToken);
    public Task<Result<IEnumerable<PedidoResponse>>> HandleGetAllPedidos(GetAllPedidosQuery query,CancellationToken cancellationToken);
    public Task<Result<PedidoResponse?>> HandleGetPedidoById(GetPedidoByIdQuery query, CancellationToken cancellationToken);
    public Task<Result<IEnumerable<ItemResponse>>> HandleGetAllItems(GetAllItemsQuery query, CancellationToken cancellationToken);
}