using GoodBurger.Application.Abstractions;
using GoodBurger.Domain.Models;

namespace GoodBurger.Application.Pedidos.Queries.GetPedidoById;
public sealed class GetPedidoByIdHandler
{
    private readonly IPedidoRepository _repo;

    public GetPedidoByIdHandler(IPedidoRepository repo)
    {
        _repo = repo;
    }

    public async Task<Pedido?> HandleGetPedidoById(GetPedidoByIdQuery query)
    {
        return await _repo.GetByIdAsync(query.Id);
    }
}
