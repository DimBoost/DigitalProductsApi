namespace DigitalProductsApi.src.Application.DTOs.Products
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
