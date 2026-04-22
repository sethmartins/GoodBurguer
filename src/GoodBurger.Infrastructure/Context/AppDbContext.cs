using GoodBurger.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodBurger.Infrastructure.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}

    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<Item> Items => Set<Item>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>(pedido =>
        {
            pedido.OwnsMany(p => p.Itens, item =>
            {
                item.WithOwner().HasForeignKey("PedidoId");

                item.Property<int>("Id"); // chave interna do EF
                item.HasKey("Id");

                item.Property(i => i.Nome).IsRequired();
                item.Property(i => i.Preco).IsRequired();
                item.Property(i => i.Tipo).IsRequired();
            });
        });
        modelBuilder.Entity<Item>(item =>
        {
            item.HasKey(i => i.Id);
        });
    }
}
