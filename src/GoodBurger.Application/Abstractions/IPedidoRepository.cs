using GoodBurger.Domain.Models;

namespace GoodBurger.Application.Abstractions;

public interface IPedidoRepository
{
    Task AddAsync(Pedido pedido);
    Task<Pedido?> GetByIdAsync(Guid id);
    Task<List<Pedido>> GetAllAsync();
}