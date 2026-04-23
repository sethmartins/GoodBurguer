using GoodBurger.Application.Contracts.Responses;

namespace GoodBurger.Web.Services;

public sealed class ItemService
{
    private readonly HttpClient _http;

    public ItemService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ItemResponse>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<ItemResponse>>("/api/cardapio");
    }
}
