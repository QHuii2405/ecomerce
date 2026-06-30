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
    public async Task<IActionResult> GetAll()
    {
        var (products, source) = await _productService.GetAllProductsAsync();
        return Ok(new { Data = products, Source = source });
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
}
