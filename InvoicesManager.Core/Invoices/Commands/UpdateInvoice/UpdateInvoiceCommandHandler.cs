using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Exceptions;
using InvoicesManager.Core.Interfaces.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InvoicesManager.Core.Invoices.Commands.UpdateInvoice
{
    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;


        public UpdateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IProductRepository productRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice =
                await _invoiceRepository.GetWithProductsByIdAsync(request.Id);
            if (invoice is null)
                throw new NotFoundException(nameof(Invoice), request.Id);

            invoice.Update(request.Name, request.SaleDate, request.UserEmail, request.ReceiverEmail);

            foreach (var productDto in request.Products)
            {
                invoice.Products
                    .SingleOrDefault(x => x.Id == productDto.Id)
                    .Update(
                    productDto.Name,
                    productDto.Quantity,
                    productDto.Price,
                    productDto.IsPayed);
            }

            await _invoiceRepository.UpdateAsync(invoice);
            return Unit.Value;
        }
    
    }
}
