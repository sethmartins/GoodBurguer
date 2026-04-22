using GoodBurger.Domain.Enums;

namespace GoodBurger.Domain.Models;

public sealed class Item
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
    public TipoItem Tipo { get; private set; }

    private Item() { }

    public Item(string nome, decimal preco, TipoItem tipo)
    {
        Nome = nome;
        Preco = preco;
        Tipo = tipo;
    }
}
