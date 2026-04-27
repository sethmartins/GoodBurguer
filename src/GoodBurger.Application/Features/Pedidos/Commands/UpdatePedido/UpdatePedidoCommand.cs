namespace GoodBurger.Application.Features.Pedidos.Commands.UpdatePedido;

public record UpdatePedidoCommand(Guid Id, IEnumerable<int> ItemIds);
