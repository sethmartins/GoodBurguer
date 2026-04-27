using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Domain.Abstractions;

namespace GoodBurger.Web.Services;

public sealed class ItemService
{
    private readonly HttpClient _http;

    public ItemService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<ItemResponse>> GetAll()
    {
        return await _http.GetFromJsonAsync<IEnumerable<ItemResponse>>("/api/cardapio");
    }
}
