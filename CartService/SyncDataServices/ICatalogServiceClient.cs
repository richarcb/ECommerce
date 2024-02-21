using CartService.Dtos;

namespace CartService.SyncDataServices
{
    public interface ICatalogServiceClient
    {
        Task<ProductDto> GetProductByIdAsync(int productId);
    }
}
