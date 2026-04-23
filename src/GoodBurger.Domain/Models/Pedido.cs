using GoodBurger.Domain.Enums;
using GoodBurger.Domain.Exceptions;
using GoodBurger.Domain.ValueObjects;

namespace GoodBurger.Domain.Models;

public sealed class Pedido
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    private readonly List<ItemPedido> _itens = new();
    public IReadOnlyCollection<ItemPedido> Itens => _itens;
    public decimal Subtotal { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal PercentualDesconto { get; private set; }
    public decimal Total { get; private set; }
    public bool Fechado { get; private set; }

    public void AdicionarItem(Item item)
    {
        //if (Fechado)
        //    throw new DomainException("Pedido já fechado");

        if (_itens.Any(i => i.Tipo == item.Tipo))
            throw new DomainException($"Já existe item do tipo {item.Tipo}");

        _itens.Add(new ItemPedido(item));
    }
    public void AtualizarItens(List<Item> items)
    {
        //if (Fechado)
        //    throw new DomainException("Não é possível alterar um pedido fechado");

        _itens.Clear();

        foreach (var item in items)
            AdicionarItem(item);
    }
    public void FecharPedido()
    {
        if (!_itens.Any())
            throw new DomainException("Pedido deve ter itens");

        if (!_itens.Any(i => i.Tipo == TipoItem.Sanduiche))
            throw new DomainException("Pedido deve ter ao menos um sanduíche");

        Subtotal = _itens.Sum(i => i.Preco);

        PercentualDesconto = CalcularPercentualDesconto();

        Desconto = Subtotal * PercentualDesconto;

        Total = Subtotal - Desconto;

        Fechado = true;
    }
    private decimal CalcularPercentualDesconto()
    {
        var temSanduiche = _itens.Any(i => i.Tipo == TipoItem.Sanduiche);
        var temBatata = _itens.Any(i => i.Tipo == TipoItem.Batata);
        var temRefri = _itens.Any(i => i.Tipo == TipoItem.Refrigerante);

        if (temSanduiche && temBatata && temRefri)
            return 0.20m;

        if (temSanduiche && temRefri)
            return 0.15m;

        if (temSanduiche && temBatata)
            return 0.10m;

        return 0;
    }

}