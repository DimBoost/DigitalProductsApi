using DigitalProductsApi.src.Infrastructure.Identity;

namespace DigitalProductsApi.src.Domain.Entities
{
    public class Purchase
    {
        public Guid ID { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        // Save price at time of purchase
        public decimal Price { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
    }
}
