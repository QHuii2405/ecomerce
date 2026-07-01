using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<(IEnumerable<ProductResponse> Products, string Source)> GetAllProductsAsync();
    Task<ProductResponse?> GetProductByIdAsync(Guid id);
    Task<Guid> CreateProductAsync(CreateProductRequest request);
    Task UpdateProductAsync(Guid id, UpdateProductRequest request);
    Task DeleteProductAsync(Guid id);
}
