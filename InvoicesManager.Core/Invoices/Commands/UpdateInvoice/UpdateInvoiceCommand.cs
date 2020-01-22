using InvoicesManager.Core.Invoices.Models;
using MediatR;

namespace InvoicesManager.Core.Invoices.Commands.UpdateInvoice
{
    public class UpdateInvoiceCommand : InvoiceDto, IRequest
    {
    }
}
