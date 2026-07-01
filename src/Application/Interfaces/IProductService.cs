using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<(IEnumerable<ProductResponse> Products, string Source)> GetAllProductsAsync(
        string? category = null,
        string? brand = null,
        string? search = null,
        decimal? minPrice = null,
        decimal? maxPrice = null);
    Task<IEnumerable<string>> GetProductBrandsAsync(string? category = null);
    Task<ProductResponse?> GetProductByIdAsync(Guid id);
    Task<Guid> CreateProductAsync(CreateProductRequest request);
    Task UpdateProductAsync(Guid id, UpdateProductRequest request);
    Task DeleteProductAsync(Guid id);

    // Variant Management
    Task<Guid> AddVariantAsync(Guid productId, CreateProductVariantRequest request);
    Task UpdateVariantAsync(Guid productId, Guid variantId, UpdateProductVariantRequest request);
    Task RemoveVariantAsync(Guid productId, Guid variantId);
}
