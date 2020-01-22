using System;
using System.Collections.Generic;

namespace InvoicesManager.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public string Name { get; private set; }
        public string SaleDate { get; private set; }
        public int UserId { get; private set; }
        public User CreatedBy { get; private set; }
        public string UserEmail { get; private set; }
        public string ReceiverEmail { get; private set; }
        public ICollection<Product> Products { get; set; }

        public Invoice(
            string name,
            string saleDate,
            string userEmail,
            int userId,
            string receiverEmail)
        {
            Name = name;
            SaleDate = saleDate;
            UserEmail = userEmail;
            UserId = userId;
            ReceiverEmail = receiverEmail;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string saleDate, string userEmail, string receiverEmail)
        {
            Name = name;
            SaleDate = saleDate;
            UserEmail = userEmail;
            ReceiverEmail = receiverEmail;
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
