using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Domain.Abstractions;
using GoodBurger.Domain.Abstractions.Errors;
using GoodBurger.Domain.Exceptions;
using GoodBurger.Domain.Models;

namespace GoodBurger.Application.Features.Pedidos.Commands.CreatePedido;

public sealed class CreatePedidoHandler : IHandler<CreatePedidoCommand, Result<PedidoResponse>>
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

    public async Task<Result<PedidoResponse>> HandleAsync(CreatePedidoCommand command, CancellationToken cancellationToken)
    {
        var items = await _itemRepo.GetByIdsAsync(command.ItemIds);

        if (items.Count() != command.ItemIds.Count())
            throw new DomainException("Um ou mais itens não foram encontrados");

        var pedido = new Pedido();

        foreach (var item in items)
            pedido.AdicionarItem(new Item(item.Id, item.Nome, item.Preco, item.Tipo));

        pedido.FecharPedido();

        var addResult = await _pedidoRepo.AddAsync(pedido, cancellationToken);
        if (addResult == 0)
            return Result.Failure<PedidoResponse>(new Error("CREATE_ERROR", "Erro ao criar o pedido"));

        var pedidoResponse = new PedidoResponse(
            pedido.Id,
            pedido.Subtotal,
            pedido.Desconto,
            pedido.PercentualDesconto * 100,
            pedido.Total,
            pedido.Itens.Select(i =>
                new ItemPedidoResponse(
                    i.Id,
                    i.ItemId,
                    i.Nome, 
                    i.Preco,
                    i.Tipo)
                ).ToList<ItemPedidoResponse>()
                ?? new List<ItemPedidoResponse>() 
        ) ;
        return Result.Success<PedidoResponse>(pedidoResponse);
    }
}
        