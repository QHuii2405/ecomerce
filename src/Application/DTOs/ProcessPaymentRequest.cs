namespace Application.DTOs;

public class ProcessPaymentRequest
{
    public Guid OrderId { get; set; }
    public bool SimulateSuccess { get; set; } // Giả lập thành công hoặc thất bại
}