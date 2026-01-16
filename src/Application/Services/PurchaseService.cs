using DigitalProductsApi.src.Application.Interfaces;
using DigitalProductsApi.src.Domain.Entities;
using DigitalProductsApi.src.Domain.Exceptions;
using DigitalProductsApi.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DigitalProductsApi.src.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly AppDbContext _context;

        public PurchaseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task PurchaseAsync(Guid productId, Guid userId)
        {
            var product = await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => new
                {
                    p.Id,
                    p.Price,
                    p.AvailableFrom,
                    p.AvailableUntil
                })
                .FirstOrDefaultAsync();

            if (product is null)
                //Custom Exception Class
                throw new NotFoundException(nameof(Product), productId);

            // Check product avaibility
            var now = DateTime.UtcNow;

            if((product.AvailableFrom.HasValue && product.AvailableFrom > now) ||
                (product.AvailableUntil.HasValue && product.AvailableUntil < now))
            {
                throw new InvalidCastException("Product is not available");
            }

            // Prevent double purchases
            var alreadyPurchased = await _context.Purchases
                .AnyAsync(p => p.ProductId == productId && p.UserId == userId);

            if (alreadyPurchased)
            {
                throw new InvalidOperationException("Product already purchased");
            }

            // Create purchase
            var purchase = new Purchase
            {
                ProductId = product.Id,
                UserId = userId,
                Price = product.Price,
                PurchaseDate = now
            };

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Purchase>> GetUserPurchasesAsync(Guid userId)
        {
            return await _context.Purchases
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.PurchaseDate)
                .ToListAsync();
        }

    }
}
