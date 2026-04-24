
namespace GoodBurger.Application.Pedidos.Commands.UpdatePedido;

public record UpdatePedidoCommand(Guid Id, IEnumerable<int> ItemIds);
