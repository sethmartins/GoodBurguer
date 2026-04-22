using GoodBurger.Domain.Enums;
using GoodBurger.Domain.Exceptions;
using GoodBurger.Domain.Models;

namespace GoodBurger.Domain.ValueObjects;

public sealed class ItemPedido
{
    public int ItemId { get; private set; }
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
    public TipoItem Tipo { get; private set; }

    private ItemPedido() { }

    public ItemPedido(Item item)
    {
        if (string.IsNullOrWhiteSpace(item.Nome))
            throw new DomainException("Nome é obrigatório");

        if (item.Preco <= 0)
            throw new DomainException("Preço deve ser maior que zero");

        ItemId = item.Id;
        Nome = item.Nome;
        Preco = item.Preco;
        Tipo = item.Tipo;
    }
   
}
