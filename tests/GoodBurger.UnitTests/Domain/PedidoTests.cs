

using FluentAssertions;
using GoodBurger.Domain.Enums;
using GoodBurger.Domain.Models;

namespace GoodBurger.UnitTests.Domain;

public sealed class PedidoTests
{
    [Fact]
    public void Deve_Calcular_Desconto_De_20_Porcento()
    {
        var pedido = new Pedido();

        pedido.AdicionarItem(new Item("X Burger", 5, TipoItem.Sanduiche));
        pedido.AdicionarItem(new Item("Batata", 2, TipoItem.Batata));
        pedido.AdicionarItem(new Item("Refri", 2.5m, TipoItem.Refrigerante));

        pedido.FecharPedido();

        pedido.Subtotal.Should().Be(9.5m);
        pedido.PercentualDesconto.Should().Be(0.2m);
        pedido.Total.Should().Be(7.6m);
    }
}
