using FluentValidation;
namespace GoodBurger.Application.Features.Pedidos.Commands.CreatePedido;
public sealed class CreatePedidoValidator : AbstractValidator<CreatePedidoCommand>
{
    public CreatePedidoValidator()
    {
        RuleFor(x => x.ItemIds)
            .NotNull()
            .NotEmpty()
            .WithMessage("O pedido deve conter itens");

        
    }
}
