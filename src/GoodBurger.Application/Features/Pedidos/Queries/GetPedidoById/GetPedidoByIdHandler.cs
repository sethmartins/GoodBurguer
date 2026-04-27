using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Domain.Abstractions;
using GoodBurger.Domain.Abstractions.Errors;


namespace GoodBurger.Application.Features.Pedidos.Queries.GetPedidoById;
public sealed class GetPedidoByIdHandler : IHandler<GetPedidoByIdQuery, Result<PedidoResponse?>>
{
    private readonly IPedidoRepository _repo;

    public GetPedidoByIdHandler(IPedidoRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<PedidoResponse?>> HandleAsync(GetPedidoByIdQuery query, CancellationToken cancellationToken)
    {
        var pedido =  await _repo.GetByIdAsync(query.Id,cancellationToken);
        if (pedido ==null  )
            return Result.Failure<PedidoResponse?>(new Error("404","Pedido não encontrado"));
        var response = new PedidoResponse(
           pedido.Id,
           pedido.Subtotal,
           pedido.Desconto,
           pedido.PercentualDesconto * 100,
           pedido.Total,
           pedido.Itens.Select(i =>
               new ItemPedidoResponse(i.Id, i.ItemId, i.Nome, i.Preco, i.Tipo)).ToList()
        );
        return Result.Success<PedidoResponse>(response);
    }

}
