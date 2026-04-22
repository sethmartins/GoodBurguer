namespace GoodBurger.API.Contracts.Responses;

public record PedidoListResponse(
    Guid Id,
    decimal Subtotal,
    decimal Desconto,
    decimal PercentualDesconto,
    decimal Total,
    int QuantidadeItens
);
