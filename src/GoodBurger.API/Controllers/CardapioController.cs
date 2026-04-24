using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Cardapio.Queries.GetAllItems;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GoodBurger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardapioController(IMediator mediator) : ControllerBase
{
   

    /// <summary>
    /// Lista todos os itens do cardápio
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await mediator.HandleGetAllItems(new GetAllItemsQuery());

        var response = items
            .Select(i => new ItemResponse(
                Nome: i.Nome,
                Preco: i.Preco,
                Tipo: i.Tipo,
                Id: i.Id)).ToList();
        return Ok(response);
    }
}

