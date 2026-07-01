using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

public class GoodsReceiptDetail : BaseEntity
{
    public Guid GoodsReceiptId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal ImportPrice { get; set; }

    [JsonIgnore]
    public virtual GoodsReceipt? GoodsReceipt { get; set; }
    [JsonIgnore]
    public virtual Product? Product { get; set; }
    [JsonIgnore]
    public virtual ProductVariant? ProductVariant { get; set; }
}
