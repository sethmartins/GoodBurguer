using GoodBurger.API.Contracts.Requests;
using GoodBurger.API.Contracts.Responses;
using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Pedidos.Commands.CreatePedido;
using GoodBurger.Application.Pedidos.DTOs;
using GoodBurger.Application.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Application.Pedidos.Queries.GetPedidoById;
using GoodBurger.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace GoodBurger.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public sealed class PedidoController : ControllerBase
{
    private readonly CreatePedidoHandler _create;
    private readonly GetPedidoByIdHandler _get;
    private readonly GetAllPedidosHandler _getAllHandler;
    public PedidoController(
        CreatePedidoHandler create,
        GetPedidoByIdHandler get,
        GetAllPedidosHandler getAllHandler)
    {
        _create = create;
        _get = get;
        _getAllHandler = getAllHandler;
    }

    [HttpPost]
   
    public async Task<IActionResult> Create(CreatePedidoRequest request)
    {
        var command = new CreatePedidoCommand(request.ItemIds);

        var id = await _create.Handle(command);

        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var pedido = await _get.Handle(new GetPedidoByIdQuery(id));

        if (pedido is null)
            return NotFound();

        var response = new PedidoResponse(
            pedido.Id,
            pedido.Subtotal,
            pedido.Desconto,
            pedido.PercentualDesconto * 100,
            pedido.Total,
            pedido.Itens.Select(i =>
                new ItemResponse(i.ItemId,i.Nome, i.Preco, i.Tipo)).ToList()
);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pedidos = await _getAllHandler.Handle(new GetAllPedidosQuery());

        var response = pedidos.Select(p => new PedidoListResponse(
            p.Id,
            p.Subtotal,
            p.Desconto,
            p.PercentualDesconto * 100, // 👈 converte aqui
            p.Total,
            p.Itens.Count
        ));

        return Ok(response);
    }
}