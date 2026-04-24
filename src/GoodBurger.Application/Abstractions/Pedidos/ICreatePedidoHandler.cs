using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Pedidos.Commands.CreatePedido;

namespace GoodBurger.Application.Abstractions.Pedidos;

public interface ICreatePedidoHandler
{
    public Task<PedidoResponse> HandleCreatePedido(CreatePedidoCommand command);
}
