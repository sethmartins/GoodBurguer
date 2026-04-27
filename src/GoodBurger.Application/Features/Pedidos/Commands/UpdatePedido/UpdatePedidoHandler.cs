using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Features.Pedidos.Mappings;
using GoodBurger.Domain.Abstractions;
using GoodBurger.Domain.Abstractions.Errors;
using GoodBurger.Domain.Exceptions;

namespace GoodBurger.Application.Features.Pedidos.Commands.UpdatePedido;

public sealed class UpdatePedidoHandler(
    IPedidoRepository _pedidoRepo,
    IItemRepository _itemRepo) : IHandler<UpdatePedidoCommand, Result<PedidoResponse>>
{ 

    public async Task<Result<PedidoResponse>> HandleAsync(UpdatePedidoCommand command, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepo.GetByIdAsync(command.Id, cancellationToken);

        if (pedido is null)
            throw new DomainException("Pedido não encontrado");

        var items = await _itemRepo.GetByIdsAsync(command.ItemIds);
   

        if (items.Count() != command.ItemIds.Count())
            throw new DomainException("Um ou mais itens não foram encontrados");
      
        pedido.AtualizarItens(items); 

        pedido.FecharPedido();

        var updateResult = await _pedidoRepo.UpdateAsync(pedido, cancellationToken);
        if (updateResult == 0)
            return Result.Failure<PedidoResponse>(new Error("UPDATE_ERROR", "Erro ao atualizar o pedido")); 
       
        var pedidosResponse = PedidoMapper.ToResponse(pedido);

        return Result.Success<PedidoResponse>(pedidosResponse);
    }
}