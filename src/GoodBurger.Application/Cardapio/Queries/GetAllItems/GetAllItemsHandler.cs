using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Abstractions.Cardapios;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Domain.Abstractions;
using GoodBurger.Domain.Abstractions.Errors;
using GoodBurger.Domain.Models;
using System.Collections;

namespace GoodBurger.Application.Cardapio.Queries.GetAllItems;

public sealed class GetAllItemsHandler(IItemRepository repo) : IHandler<GetAllItemsQuery, Result<IEnumerable<ItemResponse>>>
{
    public async Task<Result<IEnumerable<ItemResponse>>> HandleAsync(GetAllItemsQuery command, CancellationToken cancellationToken)
    {
        var items = await repo.GetAllAsync();

        if (items == null )
            return Result
                .Failure<IEnumerable<ItemResponse>>(new Error("404","Nenhum item encontrado"));
        
        var itemsResponse = items?
            .Select(i => new ItemResponse(i.Id, i.Nome, i.Preco, i.Tipo))
            .ToList();
        
        return Result.Success<IEnumerable<ItemResponse>>(itemsResponse);
    }  
}