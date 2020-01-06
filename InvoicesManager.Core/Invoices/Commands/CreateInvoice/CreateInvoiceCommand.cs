using MediatR;

namespace InvoicesManager.Core.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
