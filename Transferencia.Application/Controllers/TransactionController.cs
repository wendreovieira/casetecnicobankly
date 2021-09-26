using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Transferencia.Application.Commands.Transfer;
using Transferencia.Domain.Enums;
using Transferencia.Domain.Repositories;

namespace Transferencia.Application.Controllers
{
    [ApiController]
    [Route("api/fund-transfer")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(IMediator mediator, ITransactionRepository transactionRepository)
        {
            _mediator = mediator;
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<ActionResult<TransferCommandResult>> Post([FromBody] TransferCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        
        [HttpGet]
        [Route("{transactionId}")]
        public async Task<ActionResult> GetById(Guid transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);

            if (transaction == null) return NotFound();
            
            if (transaction.Status == ETransactionStatus.Error)
                return Ok(new 
                {
                    Status = transaction.Status,
                    Error = transaction.ErrorMessage
                });

            return Ok(new
            {
                Status = transaction.Status
            });
        }
    }
}
