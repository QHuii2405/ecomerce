using Application.DTOs.Inventory;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services;

public class InventoryService : IInventoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public InventoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GoodsReceiptDto>> GetGoodsReceiptsAsync()
    {
        // Default pagination hardcoded for simplicity right now
        var receipts = await _unitOfWork.GoodsReceipts.GetReceiptsWithDetailsAsync(1, 100);
        return receipts.Select(MapToDto).ToList();
    }

    public async Task<GoodsReceiptDto> GetGoodsReceiptByIdAsync(Guid id)
    {
        var receipt = await _unitOfWork.GoodsReceipts.GetReceiptWithDetailsByIdAsync(id);
        if (receipt == null)
            throw new Exception("GoodsReceipt not found.");

        return MapToDto(receipt);
    }

    public async Task<GoodsReceiptDto> CreateGoodsReceiptAsync(Guid userId, CreateGoodsReceiptRequest request)
    {
        var receiptNumber = $"GR-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

        var receipt = new GoodsReceipt
        {
            Id = Guid.NewGuid(),
            ReceiptNumber = receiptNumber,
            CreatedByUserId = userId,
            Status = ReceiptStatus.Pending,
            Note = request.Note,
            TotalAmount = request.Details.Sum(d => d.Quantity * d.ImportPrice),
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        foreach (var reqDetail in request.Details)
        {
            var detail = new GoodsReceiptDetail
            {
                Id = Guid.NewGuid(),
                GoodsReceiptId = receipt.Id,
                ProductId = reqDetail.ProductId,
                ProductVariantId = reqDetail.ProductVariantId,
                Quantity = reqDetail.Quantity,
                ImportPrice = reqDetail.ImportPrice,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            receipt.Details.Add(detail);
        }

        await _unitOfWork.GoodsReceipts.AddAsync(receipt);
        await _unitOfWork.SaveChangesAsync();

        return await GetGoodsReceiptByIdAsync(receipt.Id);
    }

    public async Task<GoodsReceiptDto> UpdateGoodsReceiptAsync(Guid id, UpdateGoodsReceiptRequest request)
    {
        var receipt = await _unitOfWork.GoodsReceipts.GetReceiptWithDetailsByIdAsync(id);
        if (receipt == null) throw new Exception("GoodsReceipt not found.");
        if (receipt.Status != ReceiptStatus.Pending) throw new Exception("Only pending receipts can be updated.");

        receipt.Note = request.Note;
        
        // Remove existing details
        var detailRepo = _unitOfWork.Repository<GoodsReceiptDetail>();
        foreach(var oldDetail in receipt.Details)
        {
             detailRepo.Delete(oldDetail);
        }
        receipt.Details.Clear();

        foreach (var reqDetail in request.Details)
        {
            var detail = new GoodsReceiptDetail
            {
                Id = Guid.NewGuid(),
                GoodsReceiptId = receipt.Id,
                ProductId = reqDetail.ProductId,
                ProductVariantId = reqDetail.ProductVariantId,
                Quantity = reqDetail.Quantity,
                ImportPrice = reqDetail.ImportPrice,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            receipt.Details.Add(detail);
        }

        receipt.TotalAmount = receipt.Details.Sum(d => d.Quantity * d.ImportPrice);
        receipt.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.GoodsReceipts.Update(receipt);
        await _unitOfWork.SaveChangesAsync();

        return await GetGoodsReceiptByIdAsync(receipt.Id);
    }

    public async Task ApproveGoodsReceiptAsync(Guid id, Guid adminId)
    {
        var receipt = await _unitOfWork.GoodsReceipts.GetReceiptWithDetailsByIdAsync(id);

        if (receipt == null) throw new Exception("GoodsReceipt not found.");
        if (receipt.Status != ReceiptStatus.Pending) throw new Exception("Receipt is not in Pending status.");

        receipt.Status = ReceiptStatus.Approved;
        receipt.ApprovedByUserId = adminId;
        receipt.UpdatedAt = DateTime.UtcNow;

        // Update inventory logic
        foreach (var detail in receipt.Details)
        {
            if (detail.ProductVariantId.HasValue)
            {
                var variant = await _unitOfWork.Repository<ProductVariant>().GetByIdAsync(detail.ProductVariantId.Value);
                if (variant != null)
                {
                    variant.StockQuantity += detail.Quantity;
                    _unitOfWork.Repository<ProductVariant>().Update(variant);
                }
            }
            else
            {
                var inventory = await _unitOfWork.Inventories.GetByProductIdAsync(detail.ProductId);
                if (inventory != null)
                {
                    inventory.StockQuantity += detail.Quantity;
                    _unitOfWork.Inventories.Update(inventory);
                }
            }
        }

        _unitOfWork.GoodsReceipts.Update(receipt);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RejectGoodsReceiptAsync(Guid id, Guid adminId)
    {
        var receipt = await _unitOfWork.GoodsReceipts.GetByIdAsync(id);

        if (receipt == null) throw new Exception("GoodsReceipt not found.");
        if (receipt.Status != ReceiptStatus.Pending) throw new Exception("Receipt is not in Pending status.");

        receipt.Status = ReceiptStatus.Rejected;
        receipt.ApprovedByUserId = adminId;
        receipt.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.GoodsReceipts.Update(receipt);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteGoodsReceiptAsync(Guid id)
    {
        var receipt = await _unitOfWork.GoodsReceipts.GetByIdAsync(id);

        if (receipt == null) throw new Exception("GoodsReceipt not found.");
        if (receipt.Status == ReceiptStatus.Approved) throw new Exception("Cannot delete approved receipt.");

        receipt.IsDeleted = true;
        receipt.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.GoodsReceipts.Update(receipt);
        await _unitOfWork.SaveChangesAsync();
    }

    private GoodsReceiptDto MapToDto(GoodsReceipt receipt)
    {
        return new GoodsReceiptDto
        {
            Id = receipt.Id,
            ReceiptNumber = receipt.ReceiptNumber,
            CreatedByUserId = receipt.CreatedByUserId,
            CreatedByName = receipt.CreatedByUser?.FullName ?? "Unknown",
            ApprovedByUserId = receipt.ApprovedByUserId,
            ApprovedByName = receipt.ApprovedByUser?.FullName,
            Status = receipt.Status.ToString(),
            Note = receipt.Note,
            TotalAmount = receipt.TotalAmount,
            CreatedAt = receipt.CreatedAt,
            Details = receipt.Details.Select(d => new GoodsReceiptDetailDto
            {
                Id = d.Id,
                ProductId = d.ProductId,
                ProductName = d.Product?.Name ?? "",
                ProductVariantId = d.ProductVariantId,
                VariantName = d.ProductVariant?.Name,
                Quantity = d.Quantity,
                ImportPrice = d.ImportPrice
            }).ToList()
        };
    }
}
