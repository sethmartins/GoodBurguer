using GoodBurger.Application.Abstractions;

namespace GoodBurger.Application.Contracts.Responses;

public record PedidoListResponse(
    Guid Id,
    decimal Subtotal,
    decimal Desconto,
    decimal PercentualDesconto,
    decimal Total,
    int QuantidadeItens,
    List<ItemResponse> Itens
):IResponse;
