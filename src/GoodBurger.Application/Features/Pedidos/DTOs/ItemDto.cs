using GoodBurger.Domain.Enums;
namespace GoodBurger.Application.Features.Pedidos.DTOs;
public record ItemDto(string Nome, decimal Preco, TipoItem Tipo);
