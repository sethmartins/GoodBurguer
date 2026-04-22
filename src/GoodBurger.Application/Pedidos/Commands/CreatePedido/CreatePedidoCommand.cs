using GoodBurger.Application.Pedidos.DTOs;


namespace GoodBurger.Application.Pedidos.Commands.CreatePedido;

public record CreatePedidoCommand(List<int> ItemIds);
