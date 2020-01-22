using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Products.Models;
using System.Collections.Generic;

namespace InvoicesManager.Core.Invoices.Models
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SaleDate { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
