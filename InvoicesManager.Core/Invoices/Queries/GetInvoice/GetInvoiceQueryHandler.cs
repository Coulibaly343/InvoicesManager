using AutoMapper;
using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Exceptions;
using InvoicesManager.Core.Interfaces.Repositories;
using InvoicesManager.Core.Invoices.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InvoicesManager.Core.Invoices.Queries.GetInvoice
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, InvoiceDto>
    {
        private readonly IInvoiceRepository __invoiceRepository;
        private readonly IMapper _mapper;

        public GetInvoiceQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            __invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<InvoiceDto> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var invoice = await __invoiceRepository.GetWithProductsByIdAsync(request.Id);

            if (invoice == null)
            {
                throw new NotFoundException(nameof(Invoice), request.Id);
            }

            return _mapper.Map<InvoiceDto>(invoice);
        }
    }
}
