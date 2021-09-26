using FluentValidation;

namespace Transferencia.Application.Commands.Transfer
{
    public class TransferCommandValidation : AbstractValidator<TransferCommand>
    {
        public TransferCommandValidation()
        {
            RuleFor(x => x.AccountDestination)
                .NotEmpty();

            RuleFor(x => x.AccountOrigin)
                .NotEmpty();

            RuleFor(x => x.Value)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.AccountOrigin)
                .NotEqual(x => x.AccountDestination)
                .WithMessage("Origin account cannot be equal to destination account.");
        }
    }
}
