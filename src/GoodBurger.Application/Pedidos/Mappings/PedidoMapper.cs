

using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Domain.Models;

namespace GoodBurger.Application.Pedidos.Mappings;

public static class PedidoMapper
{
    public static PedidoResponse ToResponse(Pedido pedido)
    {
        return new PedidoResponse(
            pedido.Id,
            pedido.Subtotal,
            pedido.Desconto,
            pedido.PercentualDesconto * 100,
            pedido.Total,
            pedido.Itens.Select(i =>
                new ItemResponse(
                    i.ItemId,
                    i.Nome,
                    i.Preco,
                    i.Tipo
                )).ToList()
        );
    }
}
