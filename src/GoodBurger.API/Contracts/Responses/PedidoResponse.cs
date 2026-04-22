using GoodBurger.API.Controllers;
using GoodBurger.Application.Pedidos.DTOs;

namespace GoodBurger.API.Contracts.Responses;

public record PedidoResponse(
    Guid Id,
    decimal Subtotal,
    decimal Desconto,
    decimal PercentualDesconto,
    decimal Total,
    List<ItemResponse> Itens
);