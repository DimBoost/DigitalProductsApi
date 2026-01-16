using DigitalProductsApi.src.Domain.Entities;

namespace DigitalProductsApi.src.Application.Interfaces
{
    public interface IPurchaseService
    {
        Task PurchaseAsync(Guid productId, Guid userId);
        Task<IReadOnlyList<Purchase>> GetUserPurchasesAsync(Guid userId);
    }
}
