using MediatR;

namespace InvoicesManager.Core.Invoices.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommand : IRequest
    {
        public int Id { get; set; }
    }
}
