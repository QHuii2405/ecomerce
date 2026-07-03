namespace Application.DTOs;

public class CreateReturnRequest
{
    public Guid OrderId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public List<string>? ImageUrls { get; set; }
}

public class ReturnRequestDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public List<string>? ImageUrls { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? AdminNote { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProcessReturnRequest
{
    public bool Approve { get; set; }
    public string? AdminNote { get; set; }
}
