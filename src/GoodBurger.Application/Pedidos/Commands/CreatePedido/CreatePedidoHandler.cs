using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Domain.Exceptions;
using GoodBurger.Domain.Models;

namespace GoodBurger.Application.Pedidos.Commands.CreatePedido;

public sealed class CreatePedidoHandler 
{
    private readonly IPedidoRepository _pedidoRepo;
    private readonly IItemRepository _itemRepo;

    public CreatePedidoHandler(
        IPedidoRepository pedidoRepo,
        IItemRepository itemRepo)
    {
        _pedidoRepo = pedidoRepo;
        _itemRepo = itemRepo;
    }

    public async Task<PedidoResponse> HandleCreatePedido(CreatePedidoCommand command)
    {
        var items = await _itemRepo.GetByIdsAsync(command.ItemIds);

        if (items.Count() != command.ItemIds.Count())
            throw new DomainException("Um ou mais itens não foram encontrados");

        var pedido = new Pedido();

        foreach (var item in items)
            pedido.AdicionarItem(item);

        pedido.FecharPedido();

        await _pedidoRepo.AddAsync(pedido);

        return new PedidoResponse(
            pedido.Id,
            pedido.Subtotal,
            pedido.Desconto,
            pedido.PercentualDesconto,
            pedido.Total,
            pedido.Itens.Select(i =>
                new ItemResponse(
                    i.ItemId,
                    i.Nome, 
                    i.Preco,
                    i.Tipo)
                ).ToList<ItemResponse>()
                ?? new List<ItemResponse>() 
        ) ;
    }
}