﻿using InvoicesManager.Core.Invoices.Commands.CreateInvoice;
using InvoicesManager.Core.Invoices.Commands.DeleteInvoice;
using InvoicesManager.Core.Invoices.Commands.UpdateInvoice;
using InvoicesManager.Core.Invoices.Queries.GetAllInvoices;
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
        /// Get invoice Id
        /// </summary>
        /// <param name="id">Id of the user </param>
        /// <returns>Invoice</returns>
        /// <response code="200">Invoice has been returned successfully.</response>
        /// <response code="404">Requested Invoice not found.</response>
        /// <response code="500">Getting Invoice failed.</response>
        [HttpGet("all/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll(int userId)
        {
            var invoice = await Mediator.Send(new GetAllInvoicesQuery { UserId = userId });
            return Ok(invoice);
        }

        /// <summary>
        /// Create inovice
        /// </summary>
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

        /// <summary>
        /// Update inovice
        /// </summary>
        /// <response code="204">Invoice has been updated successfully.</response>
        /// <response code="400">Invoice hasn't been updated successfully.</response>
        /// <response code="404">Requested Invoice not found.</response>
        /// <response code="500">Update Invoice failed.</response>
        [HttpPut("{invoiceId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateInvoice([FromBody] UpdateInvoiceCommand command, int invoiceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            command.Id = invoiceId;
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete invoice by id
        /// </summary>
        /// <param name="invoiceId">Id of the invoice to delete.</param>
        /// <response code="204">Invoice deleted.</response>
        /// <response code="404">Requested Invoice not found.</response>
        /// <response code="500">Delete Invoice failed.</response>
        [HttpDelete("{invoiceId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteInvoice(int invoiceId)
        {
            await Mediator.Send(new DeleteInvoiceCommand { Id = invoiceId });
            return NoContent();
        }

    }
}
