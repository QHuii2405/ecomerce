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
    private readonly IMomoPaymentProvider _momoProvider;
    private readonly IPayOsPaymentProvider _payOsProvider;

    public PaymentGatewayService(IUnitOfWork unitOfWork, IConfiguration configuration, IMomoPaymentProvider momoProvider, IPayOsPaymentProvider payOsProvider)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _momoProvider = momoProvider;
        _payOsProvider = payOsProvider;
    }

    public async Task<PaymentInitResponse> CreatePaymentAsync(CreatePaymentRequest request)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
        if (order == null) throw new KeyNotFoundException("Không tìm thấy đơn hàng.");
        if (order.Status != "Pending") throw new InvalidOperationException("Đơn hàng không ở trạng thái chờ thanh toán.");

        var provider = NormalizeProvider(request.Provider);
        
        // PayOS requires orderCode to be a long/int, up to 9007199254740991. 
        // MoMo accepts strings. So we generate a numeric timestamp-based code for both for consistency,
        // or keep prefix for MoMo. Let's use numeric for PayOS.
        var transactionCode = provider == "PayOS" 
            ? DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() 
            : $"{provider}{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
        
        string paymentUrl;
        string qrCodeUrl;
        var isDemoModeStr = _configuration["PaymentSettings:IsDemoMode"];
        var isDemoMode = string.IsNullOrEmpty(isDemoModeStr) || isDemoModeStr.Equals("true", StringComparison.OrdinalIgnoreCase);

        if (isDemoMode)
        {
            // Demo mode: Return a local URL that indicates success and auto-confirm order
            paymentUrl = $"http://localhost:5173/payment-result?orderId={order.Id}&resultCode=0&code=00";
            qrCodeUrl = paymentUrl;

            order.PaymentStatus = "Paid";
            order.Status = "Confirmed";
            _unitOfWork.Orders.Update(order);
        }
        else
        {
            try 
            {
                if (provider == "MoMo")
                {
                    (paymentUrl, qrCodeUrl) = await _momoProvider.CreatePaymentAsync(order, transactionCode);
                }
                else 
                {
                    (paymentUrl, qrCodeUrl) = await _payOsProvider.CreatePaymentAsync(order, transactionCode);
                }
            }
            catch (Exception ex)
            {
                 throw new Exception($"Lỗi khi tạo giao dịch {provider}: {ex.Message}");
            }
        }

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

    public async Task ConfirmPaymentByTransactionCodeAsync(string provider, string transactionCode)
    {
        var normalizedProvider = NormalizeProvider(provider);
        var transactions = await _unitOfWork.Repository<PaymentTransaction>().FindAsync(t =>
            t.Provider == normalizedProvider && t.TransactionCode == transactionCode && !t.IsDeleted);
        var transaction = transactions.OrderByDescending(t => t.CreatedAt).FirstOrDefault();
        if (transaction == null) throw new KeyNotFoundException("Không tìm thấy giao dịch thanh toán.");

        await ConfirmPaymentAsync(transaction.OrderId, provider, transactionCode);
    }

    public async Task<bool> RefundPaymentAsync(Guid orderId)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
        if (order == null) throw new KeyNotFoundException("Không tìm thấy đơn hàng.");

        var transactions = await _unitOfWork.Repository<PaymentTransaction>().FindAsync(t => t.OrderId == orderId && t.Status == "Paid");
        var transaction = transactions.FirstOrDefault();
        
        if (transaction != null)
        {
            // Trong môi trường thực tế, chúng ta sẽ gọi API của MoMo hoặc PayOS để thực hiện Refund:
            // if (transaction.Provider == "MoMo") await _momoProvider.RefundAsync(transaction.TransactionCode, order.TotalAmount);
            // else await _payOsProvider.RefundAsync(transaction.TransactionCode, order.TotalAmount);
            
            // Mock Refund Success
            transaction.Status = "Refunded";
            transaction.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Repository<PaymentTransaction>().Update(transaction);
        }

        order.PaymentStatus = "Refunded";
        order.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.Orders.Update(order);
        
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    private static string NormalizeProvider(string provider)
    {
        return provider.Trim().Equals("PayOS", StringComparison.OrdinalIgnoreCase) ? "PayOS" : "MoMo";
    }
}
