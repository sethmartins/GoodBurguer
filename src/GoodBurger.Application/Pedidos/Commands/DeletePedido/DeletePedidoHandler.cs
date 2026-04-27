using GoodBurger.Application.Abstractions;
using GoodBurger.Domain.Abstractions;
using GoodBurger.Domain.Abstractions.Errors;
using GoodBurger.Domain.Exceptions;

namespace GoodBurger.Application.Pedidos.Commands.DeletePedido;

public class DeletePedidoHandler : IHandler<DeletePedidoCommand, Result<int>>
{
    private readonly IPedidoRepository _repo;

    public DeletePedidoHandler(IPedidoRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<int>> HandleAsync(DeletePedidoCommand command,CancellationToken cancellationToken)
    {
        var pedido = await _repo.GetByIdAsync(command.Id, cancellationToken);

        if (pedido is null)
            throw new DomainException("Pedido não encontrado");

        var deleteResult = await _repo.DeleteAsync(pedido, cancellationToken);

   
        return  deleteResult.Value > 0 ? 
            Result.Success<int>(deleteResult.Value) 
            : Result.Failure<int>(new Error("DELETE_ERROR","Failed to delete pedido"));
    }
}
