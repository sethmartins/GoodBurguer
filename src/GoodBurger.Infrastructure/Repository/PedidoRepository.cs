using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Domain.Models;
using GoodBurger.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace GoodBurger.Infrastructure.Repository;

public sealed class PedidoRepository(AppDbContext context) : IPedidoRepository
{
    public async Task AddAsync(Pedido pedido)
    {
        context.Pedidos.Add(pedido);
        await context.SaveChangesAsync();
    }

    public async Task<Pedido?> GetByIdAsync(Guid id) 
        => await context.Pedidos.FindAsync(id);
    public async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await context.Pedidos
            .Include(p => p.Itens)            
            .ToListAsync();
    }

    public async Task UpdateAsync(Pedido pedido)
    {
        context.Pedidos.Update(pedido);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Pedido pedido)
    {
        context.Pedidos.Remove(pedido);
        await context.SaveChangesAsync();
    }
}
