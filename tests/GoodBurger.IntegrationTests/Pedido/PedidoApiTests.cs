using FluentAssertions;
using GoodBurger.Application.Contracts.Responses;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace GoodBurger.IntegrationTests.Pedido;

public sealed class PedidoApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PedidoApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Deve_Criar_Pedido_Com_Sucesso()
    {
        var request = new
        {
            itemIds = new[] { 1, 2, 3 }
        };

        var response = await _client.PostAsJsonAsync("/api/pedido", request);
        var raw = await response.Content.ReadAsStringAsync();
        Console.WriteLine(raw);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
    [Fact]
    public async Task Deve_Buscar_Pedido_Por_Id()
    {
        var id = await CriarPedidoAsync();

        var response = await _client.GetAsync($"/api/pedido/{id}");

        response.IsSuccessStatusCode.Should().BeTrue();

        var pedido = await response.Content.ReadFromJsonAsync<PedidoResponse>();

        pedido.Should().NotBeNull();
        pedido!.Id.Should().Be(id);
    }

    [Fact]
    public async Task Deve_Retornar_404_Se_Pedido_Nao_Existir()
    {
        var response = await _client.GetAsync($"/api/pedido/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Deve_Listar_Todos_Pedidos()
    {
        await CriarPedidoAsync();
        await CriarPedidoAsync();

        var response = await _client.GetAsync("/api/pedido");

        response.IsSuccessStatusCode.Should().BeTrue();

        var pedidos = await response.Content.ReadFromJsonAsync<List<PedidoResponse>>();

        pedidos.Should().NotBeEmpty();
        pedidos!.Count.Should().BeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task Deve_Atualizar_Pedido()
    {
        var id = await CriarPedidoAsync();

        var update = new { itemIds = new[] { 1, 3 } };

        var response = await _client.PutAsJsonAsync($"/api/pedido/{id}", update);

        response.IsSuccessStatusCode.Should().BeTrue();

        var pedido = await response.Content.ReadFromJsonAsync<PedidoResponse>();

        pedido!.Itens.Count.Should().Be(2);
    }

    [Fact]
    public async Task Deve_Remover_Pedido()
    {
        var id = await CriarPedidoAsync();

        var delete = await _client.DeleteAsync($"/api/pedido/{id}");

        delete.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var get = await _client.GetAsync($"/api/pedido/{id}");

        get.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    private async Task<Guid> CriarPedidoAsync()
    {
        var request = new { itemIds = new[] { 1, 2, 3 } };

        var response = await _client.PostAsJsonAsync("/api/pedido", request);

        var content = await response.Content
            .ReadFromJsonAsync<PedidoResponse>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return content!.Id;
    }
}