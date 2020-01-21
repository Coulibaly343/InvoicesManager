namespace InvoicesManager.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public double Price { get; set; }
        public bool IsPayed { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
