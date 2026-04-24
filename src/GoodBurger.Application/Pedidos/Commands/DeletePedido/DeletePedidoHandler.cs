using GoodBurger.Application.Abstractions;
using GoodBurger.Domain.Exceptions;

namespace GoodBurger.Application.Pedidos.Commands.DeletePedido;

public class DeletePedidoHandler
{
    private readonly IPedidoRepository _repo;

    public DeletePedidoHandler(IPedidoRepository repo)
    {
        _repo = repo;
    }

    public async Task HandleDeletePedido(DeletePedidoCommand command)
    {
        var pedido = await _repo.GetByIdAsync(command.Id);

        if (pedido is null)
            throw new DomainException("Pedido não encontrado");

        await _repo.DeleteAsync(pedido);
    }
}
