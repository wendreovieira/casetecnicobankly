using Transferencia.Domain.Core.Message;

namespace Transferencia.Application.Commands.Transfer
{
    public sealed class TransferCommand : Command<TransferCommandResult>
    {
        public string AccountOrigin { get; set; }
        public string AccountDestination { get; set; }
        public float Value { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new TransferCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
