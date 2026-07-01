using Application.DTOs;

namespace Application.Interfaces;

public interface IVoucherService
{
    Task<List<VoucherDto>> GetAllVouchersAsync();
    Task<VoucherDto> CreateVoucherAsync(CreateVoucherRequest request);
    Task<ApplyVoucherResponse> ValidateAndApplyVoucherAsync(ApplyVoucherRequest request);
    Task ToggleVoucherStatusAsync(Guid id);
}
