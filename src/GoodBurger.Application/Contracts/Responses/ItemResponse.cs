using GoodBurger.Application.Abstractions;
using GoodBurger.Domain.Enums;

namespace GoodBurger.Application.Contracts.Responses;

public record ItemResponse(
    int Id,  string Nome, decimal Preco, TipoItem Tipo);

public record ItemPedidoResponse(
    int Id, int ItemId, string Nome, decimal Preco, TipoItem Tipo);
