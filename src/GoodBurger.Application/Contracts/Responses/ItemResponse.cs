using GoodBurger.Domain.Enums;

namespace GoodBurger.Application.Contracts.Responses;

public record ItemResponse(int Id, string Nome, decimal Preco, TipoItem Tipo);
