namespace WebAPI.Controllers;

using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return Ok(categories.Where(c => !c.IsDeleted));
        }
        catch (Exception ex)
        {
            return BadRequest("Loi khi lay danh sach danh muc: " + ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> Create(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { Message = "Tao danh muc thanh cong!", CategoryId = category.Id });
        }
        catch (Exception ex)
        {
            return BadRequest("Loi khi tao danh muc: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null || category.IsDeleted)
            {
                return NotFound(new { Message = "Khong tim thay danh muc." });
            }

            category.IsDeleted = true;
            category.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { Message = "Da xoa danh muc." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Loi khi xoa danh muc: " + ex.Message });
        }
    }
}
