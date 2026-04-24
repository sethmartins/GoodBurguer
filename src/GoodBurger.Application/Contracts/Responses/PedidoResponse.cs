

using GoodBurger.Application.Abstractions;

namespace GoodBurger.Application.Contracts.Responses;

public record PedidoResponse(
    Guid Id,
    decimal Subtotal,
    decimal Desconto,
    decimal PercentualDesconto,
    decimal Total,
    List<ItemResponse> Itens
):IResponse;