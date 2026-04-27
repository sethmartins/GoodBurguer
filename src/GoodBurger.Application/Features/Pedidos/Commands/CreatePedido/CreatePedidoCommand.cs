using GoodBurger.Application.Abstractions;

namespace GoodBurger.Application.Features.Pedidos.Commands.CreatePedido;

public record CreatePedidoCommand(IEnumerable<int> ItemIds);
