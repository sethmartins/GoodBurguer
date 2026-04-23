using GoodBurger.Domain.Models;


namespace GoodBurger.Application.Abstractions;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(int id);

    Task<List<Item>> GetAllAsync();
    Task<List<Item>> GetByIdsAsync(List<int> ids);
}
