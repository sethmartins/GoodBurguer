using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Pedidos.Commands.DeletePedido;

namespace GoodBurger.Application.Abstractions.Pedidos;

public interface IDeletePedidoHandler
{
    Task HandleDeletePedido(DeletePedidoCommand command);
}
