using Application.DTOs.Inventory;
using Domain.Common;

namespace Application.Interfaces;

public interface IInventoryService
{
    Task<IEnumerable<GoodsReceiptDto>> GetGoodsReceiptsAsync();
    Task<GoodsReceiptDto> GetGoodsReceiptByIdAsync(Guid id);
    Task<GoodsReceiptDto> CreateGoodsReceiptAsync(Guid userId, CreateGoodsReceiptRequest request);
    Task<GoodsReceiptDto> UpdateGoodsReceiptAsync(Guid id, UpdateGoodsReceiptRequest request);
    Task ApproveGoodsReceiptAsync(Guid id, Guid adminId);
    Task RejectGoodsReceiptAsync(Guid id, Guid adminId);
    Task DeleteGoodsReceiptAsync(Guid id);
}
