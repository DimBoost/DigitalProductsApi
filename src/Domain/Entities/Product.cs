namespace DigitalProductsApi.src.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableUntil { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
