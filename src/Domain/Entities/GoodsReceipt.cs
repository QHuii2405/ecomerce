using System.Text.Json.Serialization;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class GoodsReceipt : BaseEntity
{
    public string ReceiptNumber { get; set; } = string.Empty;
    public Guid CreatedByUserId { get; set; }
    public Guid? ApprovedByUserId { get; set; }
    public ReceiptStatus Status { get; set; } = ReceiptStatus.Pending;
    public string? Note { get; set; }
    public decimal TotalAmount { get; set; }

    [JsonIgnore]
    public virtual User? CreatedByUser { get; set; }
    [JsonIgnore]
    public virtual User? ApprovedByUser { get; set; }
    
    public virtual ICollection<GoodsReceiptDetail> Details { get; set; } = new List<GoodsReceiptDetail>();
}
