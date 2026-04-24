using GoodBurger.Application.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Domain.Models;

namespace GoodBurger.Application.Abstractions.Pedidos;

public interface IGetAllPedidosHandler
{
    Task<IEnumerable<Pedido>> HandleGetAllPedidos(GetAllPedidosQuery query);
}
