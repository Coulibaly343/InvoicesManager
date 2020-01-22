using AutoMapper;
using InvoicesManager.Core.Interfaces.Repositories;
using InvoicesManager.Core.Invoices.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InvoicesManager.Core.Invoices.Queries.GetAllInvoices
{
    public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDto>>
    {
        private readonly IInvoiceRepository __invoiceRepository;
        private readonly IMapper _mapper;

        public GetAllInvoicesQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            __invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await __invoiceRepository.GetAllWithProductsByUserIdAsync(request.UserId);

            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

    }
}
