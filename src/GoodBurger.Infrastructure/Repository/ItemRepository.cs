using GoodBurger.Application.Abstractions;
using GoodBurger.Domain.Models;
using GoodBurger.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodBurger.Infrastructure.Repository;

public sealed class ItemRepository(AppDbContext _ctx) : IItemRepository
{
    public async Task<Item?> GetByIdAsync(int id) 
        => await _ctx.Items.FindAsync(id);
    public async Task<List<Item>> GetAllAsync()
        =>  await _ctx.Items.AsNoTracking().ToListAsync();
    public async Task<List<Item>> GetByIdsAsync(List<int> ids)
        => await _ctx.Items.AsNoTracking()
            .Where(i => ids.Contains(i.Id))
            .ToListAsync();
    
}
