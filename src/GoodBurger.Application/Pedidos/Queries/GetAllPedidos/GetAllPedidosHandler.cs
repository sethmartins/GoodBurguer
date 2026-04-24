using GoodBurger.Application.Abstractions;
using GoodBurger.Domain.Models;

namespace GoodBurger.Application.Pedidos.Queries.GetAllPedidos;

public class GetAllPedidosHandler 
{
    private readonly IPedidoRepository _repo;

    public GetAllPedidosHandler(IPedidoRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Pedido>> HandleGetAllPedidos(GetAllPedidosQuery query)
    {
        return await _repo.GetAllAsync();
    }
}
