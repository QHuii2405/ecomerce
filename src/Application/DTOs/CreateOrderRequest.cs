namespace Application.DTOs;

public class CreateOrderRequest
{
    public List<OrderItemRequest> Items { get; set; } = new();
    public string? Note { get; set; }

    // Thông tin giao hàng
    public string ShippingAddress { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public string RecipientPhone { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = "COD"; // COD, MoMo, VietQR
}

public class OrderItemRequest
{
    public Guid ProductId { get; set; }
    public Guid? ProductVariantId { get; set; }
    public int Quantity { get; set; }
}