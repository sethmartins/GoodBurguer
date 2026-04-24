using GoodBurger.Domain.Models;


namespace GoodBurger.Application.Abstractions;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(int id);

    Task<IEnumerable<Item>> GetAllAsync();
    Task<IEnumerable<Item>> GetByIdsAsync(IEnumerable<int> ids);
}
