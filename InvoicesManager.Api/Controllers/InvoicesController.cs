using InvoicesManager.Core.Invoices.Commands.CreateInvoice;
using InvoicesManager.Core.Invoices.Queries.GetInvoice;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoicesManager.Api.Controllers
{
    public class InvoicesController : ApiBaseController
    {
        /// <summary>
        /// Get invoice Id
        /// </summary>
        /// <param name="id">Id of the invoice to return</param>
        /// <returns>Invoice</returns>
        /// <response code="200">Invoice has been returned successfully.</response>
        /// <response code="404">Requested Invoice not found.</response>
        /// <response code="500">Getting Invoice failed.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int id)
        {
            var invoice = await Mediator.Send(new GetInvoiceQuery { Id = id });
            return Ok(invoice);
        }

        /// <summary>
        /// Create inovice
        /// </summary>
        /// <param name="command">Invoice object</param>
        /// <returns>Id </returns>
        /// <response code="201">Invoice has been created successfully.</response>
        /// <response code="400">Invoice hasn't been created successfully.</response>
        /// <response code="500">Creating invoice failed.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceId = await Mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = invoiceId }, invoiceId);
        }
    }
}
