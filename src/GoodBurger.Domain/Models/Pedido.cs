using GoodBurger.Domain.Enums;
using GoodBurger.Domain.Exceptions;
using GoodBurger.Domain.ValueObjects;

namespace GoodBurger.Domain.Models;

public sealed class Pedido
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    private readonly List<ItemPedido> _itens = new();
    public IReadOnlyCollection<ItemPedido> Itens => _itens;

    public decimal Total { get; private set; }

    public void AdicionarItem(ItemPedido item)
    {
        if (_itens.Any(i => i.Tipo == item.Tipo))
            throw new DomainException($"Já existe um item do tipo {item.Tipo}");

        _itens.Add(item);
    }

    public void CalcularTotal()
    {
        if (!_itens.Any())
            throw new DomainException("Pedido deve ter ao menos um item");

        var subtotal = _itens.Sum(i => i.Preco);
        var desconto = CalcularDesconto();

        Total = subtotal - desconto;
    }

    private decimal CalcularDesconto()
    {
        var temSanduiche = _itens.Any(i => i.Tipo == TipoItem.Sanduiche);
        var temBatata = _itens.Any(i => i.Tipo == TipoItem.Batata);
        var temRefri = _itens.Any(i => i.Tipo == TipoItem.Refrigerante);

        if (temSanduiche && temBatata && temRefri)
            return _itens.Sum(i => i.Preco) * 0.20m;

        if (temSanduiche && temRefri)
            return _itens.Sum(i => i.Preco) * 0.15m;

        if (temSanduiche && temBatata)
            return _itens.Sum(i => i.Preco) * 0.10m;

        return 0;
    }
}
