using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Exceptions;
using InvoicesManager.Core.Interfaces.Repositories;
using InvoicesManager.Core.Invoices.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InvoicesManager.Core.Invoices.Queries.GetInvoice
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, InvoiceModel>
    {
        private readonly IInvoiceRepository __invoiceRepository;

        public GetInvoiceQueryHandler(IInvoiceRepository invoiceRepository)
        {
            __invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceModel> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var invoice = await __invoiceRepository.GetByIdAsync(request.Id);

            if (invoice == null)
            {
                throw new NotFoundException(nameof(Invoice), request.Id);
            }


            return new InvoiceModel()
            {
                Name = invoice.Name
            };
    
        }
    }
}
