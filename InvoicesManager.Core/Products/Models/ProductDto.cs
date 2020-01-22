namespace InvoicesManager.Core.Products.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public double Price { get; set; }
        public bool IsPayed { get; set; }
    }
}
