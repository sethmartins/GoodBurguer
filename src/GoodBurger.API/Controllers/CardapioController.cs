using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Features.Cardapio.Queries.GetAllItems;
using GoodBurger.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GoodBurger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardapioController(
        IHandler<GetAllItemsQuery, Result<IEnumerable<ItemResponse>>> mediator) : ControllerBase
{
    /// <summary>
    /// Lista todos os itens do cardápio
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result   = await mediator.HandleAsync(new GetAllItemsQuery(), cancellationToken);

        if(result.IsSuccess)
        {
            return Ok(result.Value);
        }


        return BadRequest(result.Error);
        
    }
}

