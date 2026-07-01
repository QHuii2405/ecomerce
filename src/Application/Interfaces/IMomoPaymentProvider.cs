using Domain.Entities;

namespace Application.Interfaces;

public interface IMomoPaymentProvider
{
    Task<(string PaymentUrl, string QrCodeUrl)> CreatePaymentAsync(Order order, string transactionCode);
    bool VerifySignature(string partnerCode, string orderId, string requestId, string amount, string orderInfo, string orderType, string transId, string resultCode, string message, string payType, string responseTime, string extraData, string signature);
}
