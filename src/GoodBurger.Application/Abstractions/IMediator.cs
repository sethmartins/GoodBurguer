using GoodBurger.Application.Cardapio.Queries.GetAllItems;
using GoodBurger.Application.Contracts.Responses;
using GoodBurger.Application.Pedidos.Commands.CreatePedido;
using GoodBurger.Application.Pedidos.Commands.DeletePedido;
using GoodBurger.Application.Pedidos.Commands.UpdatePedido;
using GoodBurger.Application.Pedidos.Queries.GetAllPedidos;
using GoodBurger.Application.Pedidos.Queries.GetPedidoById;
using GoodBurger.Application.Utils;
using GoodBurger.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodBurger.Application.Abstractions;

public interface IMediator
{
    
    public Task<PedidoResponse> HandleCreatePedido(CreatePedidoCommand command);
    public Task HandleDeletePedido(DeletePedidoCommand command);
    public Task<PedidoResponse> HandleUpdatePedido(UpdatePedidoCommand command);
    public Task<IEnumerable<Pedido>> HandleGetAllPedidos(GetAllPedidosQuery query);
    public Task<Pedido?> HandleGetPedidoById(GetPedidoByIdQuery query);
    public Task<IEnumerable<Item>> HandleGetAllItems(GetAllItemsQuery query);
}



public interface ICommand 
{
    

}
public interface IResponse
{


}
public interface IQuery 
{


}