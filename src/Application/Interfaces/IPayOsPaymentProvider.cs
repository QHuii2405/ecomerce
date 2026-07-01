using Domain.Entities;

namespace Application.Interfaces;

public interface IPayOsPaymentProvider
{
    Task<(string PaymentUrl, string QrCodeUrl)> CreatePaymentAsync(Order order, string transactionCode);
    bool VerifyWebhook(object webhookData, string signature);
}
