using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Pedidos.Mappings;
using GoodBurger.Domain.Exceptions;

namespace GoodBurger.Application.Pedidos.Commands.UpdatePedido;

public sealed class UpdatePedidoHandler
{
    private readonly IPedidoRepository _pedidoRepo;
    private readonly IItemRepository _itemRepo;

    public UpdatePedidoHandler(IPedidoRepository pedidoRepo, IItemRepository itemRepo)
    {
        _pedidoRepo = pedidoRepo;
        _itemRepo = itemRepo;
    }

    public async Task<PedidoResponse> HandleUpdatePedido(UpdatePedidoCommand command)
    {
        var pedido = await _pedidoRepo.GetByIdAsync(command.Id);

        if (pedido is null)
            throw new DomainException("Pedido não encontrado");

        var items = await _itemRepo.GetByIdsAsync(command.ItemIds);

        if (items.Count() != command.ItemIds.Count())
            throw new DomainException("Um ou mais itens não foram encontrados");

        pedido.AtualizarItens(items); 

        pedido.FecharPedido();

        await _pedidoRepo.UpdateAsync(pedido);

        return PedidoMapper.ToResponse(pedido);
    }
}