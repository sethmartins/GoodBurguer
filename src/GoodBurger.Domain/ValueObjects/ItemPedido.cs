using GoodBurger.Domain.Enums;
using GoodBurger.Domain.Exceptions;

namespace GoodBurger.Domain.ValueObjects;

public sealed class ItemPedido
{
    public string Nome { get; }
    public decimal Preco { get; }
    public TipoItem Tipo { get; }

    public ItemPedido(string nome, decimal preco, TipoItem tipo)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome é obrigatório");

        if (preco <= 0)
            throw new DomainException("Preço deve ser maior que zero");

        Nome = nome;
        Preco = preco;
        Tipo = tipo;
    }
}
