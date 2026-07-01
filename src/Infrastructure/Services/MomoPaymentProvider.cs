using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class MomoPaymentProvider : IMomoPaymentProvider
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public MomoPaymentProvider(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<(string PaymentUrl, string QrCodeUrl)> CreatePaymentAsync(Order order, string transactionCode)
    {
        var endpoint = _config["MoMo:Endpoint"];
        var partnerCode = _config["MoMo:PartnerCode"];
        var accessKey = _config["MoMo:AccessKey"];
        var secretKey = _config["MoMo:SecretKey"];
        var redirectUrl = $"{_config["MoMo:ReturnUrl"]}?orderId={order.Id}";
        var ipnUrl = _config["MoMo:NotifyUrl"];
        
        var requestType = "captureWallet";
        var amount = ((long)order.TotalAmount).ToString();
        var orderId = transactionCode;
        var requestId = transactionCode;
        var extraData = "";
        var orderInfo = $"Thanh toan don hang {order.Id}";

        var rawHash = $"accessKey={accessKey}&amount={amount}&extraData={extraData}&ipnUrl={ipnUrl}&orderId={orderId}&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType={requestType}";
        var signature = ComputeHmacSha256(rawHash, secretKey!);

        var requestBody = new
        {
            partnerCode,
            partnerName = "Test Store",
            storeId = "MomoTestStore",
            requestId,
            amount,
            orderId,
            orderInfo,
            redirectUrl,
            ipnUrl,
            lang = "vi",
            extraData,
            requestType,
            signature
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(responseContent);
        
        if (jsonDoc.RootElement.TryGetProperty("payUrl", out var payUrlElement))
        {
            var paymentUrl = payUrlElement.GetString()!;
            var qrCodeUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=280x280&data={Uri.EscapeDataString(paymentUrl)}";
            return (paymentUrl, qrCodeUrl);
        }

        throw new Exception($"MoMo API error: {responseContent}");
    }

    public bool VerifySignature(string partnerCode, string orderId, string requestId, string amount, string orderInfo, string orderType, string transId, string resultCode, string message, string payType, string responseTime, string extraData, string signature)
    {
        var accessKey = _config["MoMo:AccessKey"];
        var secretKey = _config["MoMo:SecretKey"];
        var rawHash = $"accessKey={accessKey}&amount={amount}&extraData={extraData}&message={message}&orderId={orderId}&orderInfo={orderInfo}&orderType={orderType}&partnerCode={partnerCode}&payType={payType}&requestId={requestId}&responseTime={responseTime}&resultCode={resultCode}&transId={transId}";
        
        var computedSignature = ComputeHmacSha256(rawHash, secretKey!);
        return computedSignature.Equals(signature, StringComparison.OrdinalIgnoreCase);
    }

    private static string ComputeHmacSha256(string message, string secretKey)
    {
        byte[] keyByte = Encoding.UTF8.GetBytes(secretKey);
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        using var hmacsha256 = new HMACSHA256(keyByte);
        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
        return BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
    }
}
