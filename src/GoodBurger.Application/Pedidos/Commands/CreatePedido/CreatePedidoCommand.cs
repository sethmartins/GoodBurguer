

using GoodBurger.Application.Abstractions;

namespace GoodBurger.Application.Pedidos.Commands.CreatePedido;

public record CreatePedidoCommand(IEnumerable<int> ItemIds) :ICommand;
