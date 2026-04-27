using GoodBurger.Application.Abstractions;
using GoodBurger.Domain.Models;
using GoodBurger.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodBurger.Infrastructure.Repository;

public sealed class PedidoRepository(AppDbContext context) : IPedidoRepository
{
    public async Task<int?> AddAsync(Pedido pedido, CancellationToken cancellationToken)
    {
        context.Pedidos.Add(pedido);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Pedido?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default)
    {         
        var pedido = await context.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (pedido == null)
            return null;        

        return pedido;
    } 
        
    public async Task<IEnumerable<Pedido?>> GetAllAsync( CancellationToken cancellationToken = default)
    {
        return await context.Pedidos.Include(p => p.Itens).ToListAsync(cancellationToken);       
            
    }

    public async Task<int?> UpdateAsync(Pedido pedido, CancellationToken cancellationToken = default)
    {
        context.Pedidos.Update(pedido);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<int?> DeleteAsync(Pedido pedido, CancellationToken cancellationToken = default)
    {
        context.Pedidos.Remove(pedido);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result;
    }
}
