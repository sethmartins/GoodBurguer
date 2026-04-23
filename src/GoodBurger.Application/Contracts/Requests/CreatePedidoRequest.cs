using GoodBurger.Application.Pedidos.DTOs;
using GoodBurger.Domain.Enums;

namespace GoodBurger.Application.Contracts.Requests;

public record CreatePedidoRequest
{
    public List<int> ItemIds { get; set; } = new();
}


