using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
namespace Application.Services;

public class VoucherService : IVoucherService
{
    private readonly IUnitOfWork _unitOfWork;

    public VoucherService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<VoucherDto>> GetAllVouchersAsync()
    {
        var vouchers = await _unitOfWork.Repository<Voucher>().FindAsync(v => !v.IsDeleted);
        return vouchers.Select(v => new VoucherDto
        {
            Id = v.Id,
            Code = v.Code,
            DiscountType = v.DiscountType,
            DiscountValue = v.DiscountValue,
            MinOrderValue = v.MinOrderValue,
            MaxDiscountValue = v.MaxDiscountValue,
            ExpiryDate = v.ExpiryDate,
            UsageLimit = v.UsageLimit,
            UsedCount = v.UsedCount,
            IsActive = v.IsActive
        }).ToList();
    }

    public async Task<VoucherDto> CreateVoucherAsync(CreateVoucherRequest request)
    {
        var existing = await _unitOfWork.Repository<Voucher>().FindAsync(v => v.Code.ToUpper() == request.Code.ToUpper() && !v.IsDeleted);
        if (existing.Any())
        {
            throw new Exception("Mã giảm giá này đã tồn tại.");
        }

        var voucher = new Voucher
        {
            Code = request.Code.ToUpper(),
            DiscountType = request.DiscountType,
            DiscountValue = request.DiscountValue,
            MinOrderValue = request.MinOrderValue,
            MaxDiscountValue = request.MaxDiscountValue,
            ExpiryDate = request.ExpiryDate,
            UsageLimit = request.UsageLimit,
            UsedCount = 0,
            IsActive = true
        };

        await _unitOfWork.Repository<Voucher>().AddAsync(voucher);
        await _unitOfWork.SaveChangesAsync();

        return new VoucherDto
        {
            Id = voucher.Id,
            Code = voucher.Code,
            DiscountType = voucher.DiscountType,
            DiscountValue = voucher.DiscountValue,
            MinOrderValue = voucher.MinOrderValue,
            MaxDiscountValue = voucher.MaxDiscountValue,
            ExpiryDate = voucher.ExpiryDate,
            UsageLimit = voucher.UsageLimit,
            UsedCount = voucher.UsedCount,
            IsActive = voucher.IsActive
        };
    }

    public async Task<ApplyVoucherResponse> ValidateAndApplyVoucherAsync(ApplyVoucherRequest request)
    {
        var vouchers = await _unitOfWork.Repository<Voucher>().FindAsync(v => v.Code.ToUpper() == request.Code.ToUpper() && !v.IsDeleted);
        var voucher = vouchers.FirstOrDefault();

        if (voucher == null)
            throw new Exception("Mã giảm giá không tồn tại.");

        if (!voucher.IsActive)
            throw new Exception("Mã giảm giá đã bị vô hiệu hóa.");

        if (voucher.ExpiryDate < DateTime.UtcNow)
            throw new Exception("Mã giảm giá đã hết hạn.");

        if (voucher.UsedCount >= voucher.UsageLimit)
            throw new Exception("Mã giảm giá đã hết lượt sử dụng.");

        if (request.OrderTotal < voucher.MinOrderValue)
            throw new Exception($"Đơn hàng phải từ {voucher.MinOrderValue:N0}đ để áp dụng mã này.");

        decimal discount = 0;
        if (voucher.DiscountType == "Percentage")
        {
            discount = request.OrderTotal * (voucher.DiscountValue / 100);
            if (voucher.MaxDiscountValue.HasValue && discount > voucher.MaxDiscountValue.Value)
            {
                discount = voucher.MaxDiscountValue.Value;
            }
        }
        else
        {
            discount = voucher.DiscountValue;
        }

        // Prevent discount from exceeding order total
        if (discount > request.OrderTotal)
            discount = request.OrderTotal;

        return new ApplyVoucherResponse
        {
            VoucherId = voucher.Id,
            Code = voucher.Code,
            DiscountAmount = discount,
            NewTotal = request.OrderTotal - discount
        };
    }

    public async Task ToggleVoucherStatusAsync(Guid id)
    {
        var voucher = await _unitOfWork.Repository<Voucher>().GetByIdAsync(id);
        if (voucher == null) throw new KeyNotFoundException("Không tìm thấy mã giảm giá.");

        voucher.IsActive = !voucher.IsActive;
        voucher.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.Repository<Voucher>().Update(voucher);
        await _unitOfWork.SaveChangesAsync();
    }
}
