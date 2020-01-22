using InvoicesManager.Core.Invoices.Models;
using MediatR;
using System.Collections.Generic;

namespace InvoicesManager.Core.Invoices.Queries.GetAllInvoices
{
    public class GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceDto>>
    {
        public int UserId { get; set; }
    }
}