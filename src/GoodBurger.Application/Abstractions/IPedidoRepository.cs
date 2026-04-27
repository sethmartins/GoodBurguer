using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Domain.Models;

namespace GoodBurger.Application.Abstractions;

public interface IPedidoRepository
{
    Task<int?> AddAsync(Pedido pedido, CancellationToken cancellationToken);
    Task<Pedido?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Pedido?>> GetAllAsync(CancellationToken cancellationToken);
    Task<int?> UpdateAsync(Pedido pedido, CancellationToken cancellationToken);
    Task<int?> DeleteAsync(Pedido pedido,CancellationToken cancellationToken);
}