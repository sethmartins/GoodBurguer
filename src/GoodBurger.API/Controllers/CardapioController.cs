using GoodBurger.Application.Cardapio.Queries.GetAllItems;
using GoodBurger.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GoodBurger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardapioController : ControllerBase
{
    private readonly GetAllItemsHandler _handler;

    public CardapioController(GetAllItemsHandler handler)
    {
        _handler = handler;
    }

    /// <summary>
    /// Lista todos os itens do cardápio
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await _handler.Handle(new GetAllItemsQuery());

        var response = items
            .Select(i => new ItemResponse(
                Nome: i.Nome,
                Preco: i.Preco,
                Tipo: i.Tipo,
                Id: i.Id)).ToList();
        return Ok(response);
    }
}

public record ItemResponse(int Id, string Nome, decimal Preco, TipoItem Tipo);