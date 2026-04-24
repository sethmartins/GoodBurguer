
using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Requests;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Pedidos.Commands.CreatePedido;
using GoodBurger.Application.Pedidos.Commands.DeletePedido;
using GoodBurger.Application.Pedidos.Commands.UpdatePedido;
using GoodBurger.Application.Pedidos.DTOs;
using GoodBurger.Application.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Application.Pedidos.Queries.GetPedidoById;
using GoodBurger.Domain.Models;
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
    private readonly UpdatePedidoHandler _updateHandler;
    private readonly DeletePedidoHandler _deleteHandler;


    public PedidoController(
        CreatePedidoHandler create,
        GetPedidoByIdHandler get,
        GetAllPedidosHandler getAllHandler,
        UpdatePedidoHandler updateHandler,
        DeletePedidoHandler deleteHandler)
    {
        _create = create;
        _get = get;
        _getAllHandler = getAllHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    [HttpPost]
   
    public async Task<IActionResult> Create(CreatePedidoRequest request)
    {
        var command = new CreatePedidoCommand(request.ItemIds);

        var pedido = await _create.Handle(command);

        return Ok(new PedidoResponse(
                pedido.Id,
                pedido.Subtotal,
                pedido.Desconto,
                pedido.PercentualDesconto * 100,
                pedido.Total,
                pedido.Itens.Select(i =>
                    new ItemResponse(i.Id,i.Nome, i.Preco, i.Tipo)).ToList<ItemResponse>()
                    ?? new List<ItemResponse>()
));
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
            p.Itens.Count,
            p.Itens.Select(i =>
                new ItemResponse(i.ItemId, i.Nome, i.Preco, i.Tipo)).ToList())
        );
        

        return Ok(response);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdatePedidoRequest request)
    {
        var command = new UpdatePedidoCommand(id, request.ItemIds);

        var response = await _updateHandler.Handle(command);

        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deleteHandler.Handle(new DeletePedidoCommand(id));

        return NoContent();
    }
}