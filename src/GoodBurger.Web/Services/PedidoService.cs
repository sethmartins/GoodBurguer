using GoodBurger.Application.Contracts.Responses;

namespace GoodBurger.Web.Services;

public sealed class PedidoService
{
    private readonly HttpClient _http;

    public PedidoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PedidoResponse>> GetAll()
        => await _http.GetFromJsonAsync<List<PedidoResponse>>("/api/Pedido");

    public async Task<PedidoResponse> GetById(Guid id)
        => await _http.GetFromJsonAsync<PedidoResponse>($"/api/Pedido/{id}");

    public async Task<PedidoResponse> Create(List<int> itemIds)
    {
        var response = await _http.PostAsJsonAsync("/api/Pedido", new { itemIds });
      
        return await response.Content.ReadFromJsonAsync<PedidoResponse>();
    }

    public async Task<PedidoResponse> Update(Guid id, List<int> itemIds)
    {
        var response = await _http.PutAsJsonAsync($"/api/Pedido/{id}", new { itemIds });
        return await response.Content.ReadFromJsonAsync<PedidoResponse>();
    }

    public async Task Delete(Guid id)
    {
        await _http.DeleteAsync($"/api/Pedido/{id}");
    }
}
