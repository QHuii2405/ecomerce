namespace Application.DTOs.Inventory;

public class GoodsReceiptDetailRequest
{
    public Guid ProductId { get; set; }
    public Guid? ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal ImportPrice { get; set; }
}

public class CreateGoodsReceiptRequest
{
    public string? Note { get; set; }
    public List<GoodsReceiptDetailRequest> Details { get; set; } = new();
}

public class UpdateGoodsReceiptRequest
{
    public string? Note { get; set; }
    public List<GoodsReceiptDetailRequest> Details { get; set; } = new();
}
