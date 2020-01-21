using System;
using System.Collections.Generic;

namespace InvoicesManager.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public string Name { get; set; }
        public string SaleDate { get; set; }
        public int UserId { get; set; }
        public User CreatedBy { get; set; }
        public string UserEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public ICollection<Product> Products { get; set; }

        public Invoice(
            string name,
            string saleDate,
            string userEmail,
            string receiverEmail)
        {
            Name = name;
            SaleDate = saleDate;
            UserEmail = userEmail;
            ReceiverEmail = receiverEmail;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
