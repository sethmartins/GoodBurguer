using FluentValidation;
using GoodBurger.Application.Abstractions;
using GoodBurger.Domain.Models;
using GoodBurger.Domain.ValueObjects;

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

    public async Task<Guid> Handle(CreatePedidoCommand command)
    {
        var pedido = new Pedido();

        foreach (var itemId in command.ItemIds)
        {
            var item = await _itemRepo.GetByIdAsync(itemId);

            if (item is null)
                throw new Exception($"Item {itemId} não encontrado");

            pedido.AdicionarItem(item);
        }

        pedido.FecharPedido();

        await _pedidoRepo.AddAsync(pedido);

        return pedido.Id;
    }
}