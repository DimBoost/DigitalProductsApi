namespace DigitalProductsApi.src.Domain.Entities
{
    public class ProductDiscount
    {
        public Guid ID { get; set; }

        public decimal Percentage { get; set; } 
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;
    }
}
