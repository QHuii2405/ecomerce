using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class PaymentGatewayService : IPaymentGatewayService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public PaymentGatewayService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<PaymentInitResponse> CreatePaymentAsync(CreatePaymentRequest request)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
        if (order == null) throw new KeyNotFoundException("Không tìm thấy đơn hàng.");
        if (order.Status != "Pending") throw new InvalidOperationException("Đơn hàng không ở trạng thái chờ thanh toán.");

        var provider = NormalizeProvider(request.Provider);
        var transactionCode = $"{provider}{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
        var paymentUrl = BuildPaymentUrl(provider, order.Id, order.TotalAmount, transactionCode, request.ReturnUrl);
        var qrCodeUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=280x280&data={Uri.EscapeDataString(paymentUrl)}";

        var transaction = new PaymentTransaction
        {
            OrderId = order.Id,
            Provider = provider,
            TransactionCode = transactionCode,
            Amount = order.TotalAmount,
            Status = "Pending",
            PaymentUrl = paymentUrl,
            QrCodeUrl = qrCodeUrl,
            RawResponse = "{}"
        };

        await _unitOfWork.Repository<PaymentTransaction>().AddAsync(transaction);
        await _unitOfWork.SaveChangesAsync();

        return new PaymentInitResponse
        {
            OrderId = order.Id,
            TransactionId = transaction.Id,
            Provider = provider,
            PaymentUrl = paymentUrl,
            QrCodeUrl = qrCodeUrl,
            Amount = order.TotalAmount,
            Status = transaction.Status
        };
    }

    public async Task ConfirmPaymentAsync(Guid orderId, string provider, string transactionCode)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) throw new KeyNotFoundException("Không tìm thấy đơn hàng.");

            var normalizedProvider = NormalizeProvider(provider);
            var transactions = await _unitOfWork.Repository<PaymentTransaction>().FindAsync(t =>
                t.OrderId == orderId && t.Provider == normalizedProvider && t.TransactionCode == transactionCode && !t.IsDeleted);
            var transaction = transactions.OrderByDescending(t => t.CreatedAt).FirstOrDefault();
            if (transaction == null) throw new KeyNotFoundException("Không tìm thấy giao dịch thanh toán.");

            if (transaction.Status != "Paid")
            {
                transaction.Status = "Paid";
                transaction.PaidAt = DateTime.UtcNow;
                transaction.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Repository<PaymentTransaction>().Update(transaction);
            }

            order.PaymentStatus = "Paid";
            order.Status = "Confirmed";
            order.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Orders.Update(order);

            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    private string BuildPaymentUrl(string provider, Guid orderId, decimal amount, string transactionCode, string returnUrl)
    {
        if (provider == "MoMo")
        {
            var endpoint = _configuration["MoMo:Endpoint"] ?? "https://test-payment.momo.vn/v2/gateway/pay";
            return $"{endpoint}?orderId={orderId}&amount={amount:0}&requestId={transactionCode}&redirectUrl={Uri.EscapeDataString(returnUrl)}";
        }

        var payOsEndpoint = _configuration["PayOS:Endpoint"] ?? "https://pay.payos.vn/web";
        return $"{payOsEndpoint}?orderCode={transactionCode}&amount={amount:0}&returnUrl={Uri.EscapeDataString(returnUrl)}";
    }

    private static string NormalizeProvider(string provider)
    {
        return provider.Trim().Equals("PayOS", StringComparison.OrdinalIgnoreCase) ? "PayOS" : "MoMo";
    }
}
