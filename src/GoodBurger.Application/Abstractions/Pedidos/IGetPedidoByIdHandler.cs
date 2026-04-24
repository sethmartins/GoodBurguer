using GoodBurger.Application.Pedidos.Queries.GetPedidoById;
using GoodBurger.Domain.Models;


namespace GoodBurger.Application.Abstractions.Pedidos;

public interface IGetPedidoByIdHandler
{
    Task<Pedido?> HandleGetPedidoById(GetPedidoByIdQuery query);
}
