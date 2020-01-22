using MediatR;
using System.Threading;
using System.Threading.Tasks;
using InvoicesManager.Core.Interfaces.Repositories;
using InvoicesManager.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using InvoicesManager.Core.Exceptions;

namespace InvoicesManager.Core.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public CreateInvoiceCommandHandler(
            IInvoiceRepository invoiceRepository,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                throw new NotFoundException(nameof(User), request.UserId);
            var invoice = new Invoice(request.Name, request.SaleDate, request.UserEmail, user.Id, request.ReceiverEmail);

            var products = new List<Product>();

            if (request.Products != null)
            {
                invoice.Products = request.Products
                    .Select(x => new Product(
                        x.Name,
                        x.Quantity,
                        x.Price,
                        x.IsPayed))
                    .ToList();
            }

            await _invoiceRepository.AddAsync(invoice);

            return invoice.Id;
        }

    }
}
