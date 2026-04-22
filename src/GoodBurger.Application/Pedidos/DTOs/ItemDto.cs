using GoodBurger.Domain.Enums;
namespace GoodBurger.Application.Pedidos.DTOs;

public record ItemDto(string Nome, decimal Preco, TipoItem Tipo);
