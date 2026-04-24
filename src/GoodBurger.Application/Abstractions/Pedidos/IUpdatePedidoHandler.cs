using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Pedidos.Commands.UpdatePedido;

namespace GoodBurger.Application.Abstractions.Pedidos;

public interface IUpdatePedidoHandler
{
    Task<PedidoResponse> HandleUpdatePedido(UpdatePedidoCommand command);
}
