using Application.DTOs;

namespace Application.Interfaces;

public interface IPaymentGatewayService
{
    Task<PaymentInitResponse> CreatePaymentAsync(CreatePaymentRequest request);
    Task ConfirmPaymentAsync(Guid orderId, string provider, string transactionCode);
}
