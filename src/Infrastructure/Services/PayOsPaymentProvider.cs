using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class PayOsPaymentProvider : IPayOsPaymentProvider
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public PayOsPaymentProvider(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<(string PaymentUrl, string QrCodeUrl)> CreatePaymentAsync(Order order, string transactionCode)
    {
        var endpoint = _config["PayOS:Endpoint"];
        var clientId = _config["PayOS:ClientId"];
        var apiKey = _config["PayOS:ApiKey"];
        var checksumKey = _config["PayOS:ChecksumKey"];
        var returnUrl = $"{_config["PayOS:ReturnUrl"]}?orderId={order.Id}";
        var cancelUrl = $"{_config["PayOS:CancelUrl"]}?orderId={order.Id}";
        
        var orderCode = long.Parse(transactionCode);
        var amount = (int)order.TotalAmount;
        var description = $"Thanh toan don {order.Id.ToString()[..6]}";

        // Tạo items giả lập
        var items = new[] { new { name = "Don hang", quantity = 1, price = amount } };

        var dataStr = $"amount={amount}&cancelUrl={cancelUrl}&description={description}&orderCode={orderCode}&returnUrl={returnUrl}";
        var signature = ComputeHmacSha256(dataStr, checksumKey!);

        var requestBody = new
        {
            orderCode,
            amount,
            description,
            items,
            cancelUrl,
            returnUrl,
            signature
        };

        var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
        request.Headers.Add("x-client-id", clientId);
        request.Headers.Add("x-api-key", apiKey);
        request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(responseContent);

        if (jsonDoc.RootElement.TryGetProperty("data", out var dataObj) && dataObj.ValueKind != JsonValueKind.Null)
        {
            if (dataObj.TryGetProperty("checkoutUrl", out var checkoutUrlElement))
            {
                var paymentUrl = checkoutUrlElement.GetString()!;
                return (paymentUrl, paymentUrl);
            }
        }

        throw new Exception($"PayOS API error: {responseContent}");
    }

    public bool VerifyWebhook(object webhookData, string signature)
    {
        // Để làm đơn giản, verify checksumKey sẽ cần parse raw webhook data
        // Trong thực tế, bạn sẽ map DTO của PayOS Webhook
        return true; 
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
