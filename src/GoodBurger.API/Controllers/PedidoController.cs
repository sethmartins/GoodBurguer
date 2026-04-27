using GoodBurger.Application.Abstractions;
using GoodBurger.Application.Contracts.Requests;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Pedidos.Commands.CreatePedido;
using GoodBurger.Application.Pedidos.Commands.DeletePedido;
using GoodBurger.Application.Pedidos.Commands.UpdatePedido;
using GoodBurger.Application.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Application.Pedidos.Queries.GetPedidoById;
using GoodBurger.Domain.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace GoodBurger.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public sealed class PedidoController(IMediator mediator) : ControllerBase
{

    [HttpPost]   
    public async Task<IActionResult> Create( 
        [FromServices] IHandler<CreatePedidoCommand, Result<PedidoResponse>> handler,
        CreatePedidoRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new CreatePedidoCommand(request.ItemIds);

        var pedido = await handler.HandleAsync(command, cancellationToken);

        if (pedido.IsSuccess)
        { 
            return Ok(pedido.Value);
        }

        return BadRequest(pedido.Error);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        [FromServices] IHandler<GetPedidoByIdQuery, Result<PedidoResponse?>> handler, 
        Guid id, 
        CancellationToken cancellationToken)
    {
        var pedido = await handler.HandleAsync(new GetPedidoByIdQuery(id), cancellationToken);

        if (pedido.IsSuccess)
        {
            return Ok(pedido.Value);
        }

       return NotFound(pedido.IsFailure);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromServices] IHandler<GetAllPedidosQuery, Result<IEnumerable<PedidoResponse>>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetAllPedidosQuery(), cancellationToken);
        
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromServices] IHandler<UpdatePedidoCommand, Result<PedidoResponse>> handler,
        Guid id, UpdatePedidoRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePedidoCommand(id, request.ItemIds);

        var response = await handler.HandleAsync(command, cancellationToken);

        if (response.IsSuccess)
        {
            return Ok(response.Value);
        }

        return BadRequest(response.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
    {
        // Para mostrar o uso diretor do mediator sem depender do handler específico, podemos criar um comando diretamente aqui e enviá-lo para o mediator.
        var response = await mediator.HandleDeletePedido(new DeletePedidoCommand(id),cancellationToken);
        if(response > 0)
        {
            return NoContent();
        }

        return BadRequest(response);
    }
}
