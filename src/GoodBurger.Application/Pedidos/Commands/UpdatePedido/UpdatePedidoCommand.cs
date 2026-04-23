
namespace GoodBurger.Application.Pedidos.Commands.UpdatePedido;

public record UpdatePedidoCommand(Guid Id, List<int> ItemIds);
