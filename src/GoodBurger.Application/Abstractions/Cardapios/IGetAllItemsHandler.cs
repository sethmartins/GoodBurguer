using GoodBurger.Application.Cardapio.Queries.GetAllItems;
using GoodBurger.Domain.Models;


namespace GoodBurger.Application.Abstractions.Cardapios;

public interface IGetAllItemsHandler
{
    Task<IEnumerable<Item>> HandleGetAllItems(GetAllItemsQuery query);
}
