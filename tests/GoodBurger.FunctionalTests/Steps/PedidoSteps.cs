using FluentAssertions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.FunctionalTests.Hooks;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;


namespace GoodBurger.FunctionalTests.Steps;

[Binding]
public sealed class PedidoSteps
{
    private HttpResponseMessage _response;

    [When(@"eu envio um pedido com sanduíche, batata e refrigerante")]
    public async Task WhenEnvioPedido()
    {
        var request = new { itemIds = new[] { 1, 2, 3 } };

        _response = await TestHooks.Client
            .PostAsJsonAsync("/api/pedido", request);
    }

    [Then(@"o desconto deve ser 20%")]
    public async Task ThenValidaDesconto()
    {
        var raw = await _response.Content.ReadAsStringAsync();
        Console.WriteLine(raw);
        var content = await _response.Content
            .ReadFromJsonAsync<PedidoResponse>();

        content.PercentualDesconto.Should().Be(20);
    }
}