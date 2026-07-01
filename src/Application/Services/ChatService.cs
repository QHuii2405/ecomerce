using System.Net.Http.Json;
using System.Text.Json;
using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class ChatService : IChatService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductService _productService;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ChatService(IUnitOfWork unitOfWork, IProductService productService, HttpClient httpClient, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _productService = productService;
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<ChatResponse> AskAsync(ChatRequest request)
    {
        var message = request.Message.Trim();
        if (string.IsNullOrWhiteSpace(message))
        {
            return new ChatResponse { Reply = "Bạn muốn tìm sản phẩm nào hoặc cần tư vấn ngân sách bao nhiêu?" };
        }

        var products = (await _unitOfWork.Products.GetProductsWithInventoryAsync()).ToList();
        var matchedProducts = products
            .Where(p => ProductMatches(p.Name, p.Description, p.Category?.Name, p.Brand, message))
            .Take(5)
            .ToList();

        if (matchedProducts.Count == 0)
        {
            matchedProducts = products
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.Inventory != null ? p.Inventory.AvailableQuantity : 0)
                .Take(5)
                .ToList();
        }

        var (_, source) = await _productService.GetAllProductsAsync();
        var allProductResponses = (await _productService.GetAllProductsAsync()).Products.ToList();
        var suggestedIds = matchedProducts.Select(p => p.Id).ToHashSet();
        var suggested = allProductResponses.Where(p => suggestedIds.Contains(p.Id)).ToList();

        var apiKey = _configuration["Gemini:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return new ChatResponse
            {
                Reply = BuildFallbackReply(message, suggested, source),
                SuggestedProducts = suggested
            };
        }

        var catalogContext = string.Join("\n", suggested.Select(p =>
            $"- {p.Name} | Hãng: {p.Brand} | Danh mục: {p.Category?.Name} | Giá: {p.Price:N0}đ | Tồn: {p.Inventory?.AvailableQuantity ?? 0} | Mô tả: {p.Description}"));

        var prompt = $"""
Bạn là chatbot tư vấn mua hàng cho iLuminaty Shop. Chỉ trả lời dựa trên dữ liệu sản phẩm dưới đây, nói tiếng Việt, ngắn gọn, thân thiện. Nếu thiếu dữ liệu, hãy nói rõ và đề xuất sản phẩm gần nhất.

Câu hỏi khách hàng: {message}

Dữ liệu sản phẩm liên quan:
{catalogContext}
""";

        try
        {
            var endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";
            var body = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[] { new { text = prompt } }
                    }
                }
            };

            var response = await _httpClient.PostAsJsonAsync(endpoint, body);
            response.EnsureSuccessStatusCode();
            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var reply = json.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return new ChatResponse
            {
                Reply = string.IsNullOrWhiteSpace(reply) ? BuildFallbackReply(message, suggested, source) : reply,
                SuggestedProducts = suggested
            };
        }
        catch
        {
            return new ChatResponse
            {
                Reply = BuildFallbackReply(message, suggested, source),
                SuggestedProducts = suggested
            };
        }
    }

    private static bool ProductMatches(string name, string description, string? category, string brand, string message)
    {
        var text = $"{name} {description} {category} {brand}".ToLowerInvariant();
        var terms = message.ToLowerInvariant()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(t => t.Length >= 3);

        return terms.Any(text.Contains);
    }

    private static string BuildFallbackReply(string message, List<ProductResponse> products, string source)
    {
        if (products.Count == 0)
        {
            return "Hiện mình chưa tìm thấy sản phẩm phù hợp. Bạn có thể cho biết thêm ngân sách, hãng hoặc nhu cầu sử dụng không?";
        }

        var lines = products.Take(3).Select(p => $"{p.Name} ({p.Brand}) giá {p.Price:N0}đ, còn {p.Inventory?.AvailableQuantity ?? 0} sản phẩm");
        return $"Mình tìm thấy vài gợi ý phù hợp từ {source}: {string.Join("; ", lines)}. Bạn muốn mình so sánh chi tiết mẫu nào không?";
    }
}
