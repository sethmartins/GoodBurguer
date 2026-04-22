using FluentValidation;
namespace GoodBurger.Application.Pedidos.Commands.CreatePedido;



public class CreatePedidoValidator : AbstractValidator<CreatePedidoCommand>
{
    public CreatePedidoValidator()
    {
        RuleFor(x => x.ItemIds)
            .NotNull()
            .NotEmpty()
            .WithMessage("O pedido deve conter itens");

        
    }
}
