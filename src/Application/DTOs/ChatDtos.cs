namespace Application.DTOs;

public class ChatRequest
{
    public string Message { get; set; } = string.Empty;
}

public class ChatResponse
{
    public string Reply { get; set; } = string.Empty;
    public List<ProductResponse> SuggestedProducts { get; set; } = new();
}
