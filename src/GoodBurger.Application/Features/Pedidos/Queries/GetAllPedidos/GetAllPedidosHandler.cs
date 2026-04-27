using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Domain.Abstractions;
using GoodBurger.Domain.Abstractions.Errors;


namespace GoodBurger.Application.Features.Pedidos.Queries.GetAllPedidos;

public class GetAllPedidosHandler(IPedidoRepository repo) : IHandler<GetAllPedidosQuery, Result<IEnumerable<PedidoResponse>>>
{
     public async Task<Result<IEnumerable<PedidoResponse>>> HandleAsync(GetAllPedidosQuery command,CancellationToken cancellationToken)
    {
        var result = await repo.GetAllAsync(cancellationToken);
    

       var response  = result?
           .Select(p => new PedidoResponse(
               p.Id,
               p.Subtotal,
               p.Desconto,
               p.PercentualDesconto *100,
               p.Total,
               p.Itens
                   .Select(i => new ItemPedidoResponse(
                       i.Id,
                       i.ItemId,
                       i.Nome,
                       i.Preco,
                       i.Tipo)
                   )
                   .ToList()
            )).ToList();

        return Result.Success<IEnumerable<PedidoResponse>>(response);
    }


}
