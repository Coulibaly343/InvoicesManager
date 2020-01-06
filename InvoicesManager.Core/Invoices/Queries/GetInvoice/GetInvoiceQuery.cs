using InvoicesManager.Core.Invoices.Models;
using MediatR;

namespace InvoicesManager.Core.Invoices.Queries.GetInvoice
{
    public class GetInvoiceQuery : IRequest<InvoiceModel>
    {
        public int Id { get; set; }
    }
}
