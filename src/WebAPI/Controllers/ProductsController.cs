namespace WebAPI.Controllers;

using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? category,
        [FromQuery] string? brand,
        [FromQuery] string? search,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice)
    {
        var (products, source) = await _productService.GetAllProductsAsync(category, brand, search, minPrice, maxPrice);
        return Ok(new { Data = products, Source = source });
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetBrands([FromQuery] string? category)
    {
        var brands = await _productService.GetProductBrandsAsync(category);
        return Ok(new { Data = brands });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound(new { Message = "Khong tim thay san pham." });
        }

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        try
        {
            var productId = await _productService.CreateProductAsync(request);
            return Ok(new { Message = "Them san pham va khoi tao kho thanh cong!", ProductId = productId });
        }
        catch (Exception ex)
        {
            return BadRequest("Co loi xay ra khi tao san pham: " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequest request)
    {
        try
        {
            await _productService.UpdateProductAsync(id, request);
            return Ok(new { Message = "Cap nhat san pham thanh cong!" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Co loi xay ra khi cap nhat san pham: " + ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return Ok(new { Message = "Da xoa san pham." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Co loi xay ra khi xoa san pham: " + ex.Message });
        }
    }

    [HttpPost("{productId}/variants")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> AddVariant(Guid productId, CreateProductVariantRequest request)
    {
        try
        {
            var variantId = await _productService.AddVariantAsync(productId, request);
            return Ok(new { Message = "Thêm biến thể thành công!", VariantId = variantId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("{productId}/variants/{variantId}")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> UpdateVariant(Guid productId, Guid variantId, UpdateProductVariantRequest request)
    {
        try
        {
            await _productService.UpdateVariantAsync(productId, variantId, request);
            return Ok(new { Message = "Cập nhật biến thể thành công!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("{productId}/variants/{variantId}")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> DeleteVariant(Guid productId, Guid variantId)
    {
        try
        {
            await _productService.RemoveVariantAsync(productId, variantId);
            return Ok(new { Message = "Xóa biến thể thành công!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
