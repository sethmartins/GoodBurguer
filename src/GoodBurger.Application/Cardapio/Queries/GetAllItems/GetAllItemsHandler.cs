using GoodBurger.Application.Abstractions;
using GoodBurger.Domain.Models;

namespace GoodBurger.Application.Cardapio.Queries.GetAllItems;

public sealed class GetAllItemsHandler
{
    private readonly IItemRepository _repo;

    public GetAllItemsHandler(IItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Item>> HandleGetAllItems(GetAllItemsQuery query)
    {
        return await _repo.GetAllAsync();
    }
}