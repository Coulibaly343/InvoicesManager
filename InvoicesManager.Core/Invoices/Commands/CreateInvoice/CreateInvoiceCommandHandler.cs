using MediatR;
using System.Threading;
using System.Threading.Tasks;
using InvoicesManager.Core.Interfaces.Repositories;

namespace InvoicesManager.Core.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
