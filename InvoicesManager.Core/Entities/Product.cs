namespace InvoicesManager.Core.Entities
{
    public class Product : BaseEntity
    {

        public string Name { get; private set; }
        public string Quantity { get; private set; }
        public double Price { get; private set; }
        public bool IsPayed { get; private set; }
        public int InvoiceId { get; private set; }
        public Invoice Invoice { get; set; }

        public Product(string name, string quantity, double price, bool isPayed)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            IsPayed = isPayed;
        }

        public void Update(string name, string quantity, double price, bool isPayed)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            IsPayed = isPayed;
        }

    }
}



