using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IProductService
{
    Task<(IEnumerable<Product> Products, string Source)> GetAllProductsAsync();
    Task<Guid> CreateProductAsync(CreateProductRequest request);
    Task UpdateProductAsync(Guid id, UpdateProductRequest request);
    Task DeleteProductAsync(Guid id);
}
